using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public class HuntGroupJob : PlatformServiceListeningJobBase
    {
        private HuntGroupJobInput m_jobInput;

        private IConversation m_pstnCallConversation;

        private List<IConversation> m_outboundAVConversations;

        private object m_syncRoot = new object();

        private int m_outboundCallTransferLock;

        private TaskCompletionSource<string> m_toneReceived;

        public HuntGroupJob(string jobid, string instanceid, AzureBasedApplicationBase azureApplication, HuntGroupJobInput input)
            : base(jobid, instanceid, azureApplication, input, JobType.HuntGroup)
        {
            m_jobInput = input;
            if (m_jobInput == null)
            {
                throw new ArgumentNullException("Failed to get job input as HuntGroupJobInput!");
            }

            m_toneReceived = new TaskCompletionSource<string>();
        }

        protected override void StartCore()
        {
            //start communication listen to incoming audioVideo calls

            /*
             This job kick off mode does not apply to multiple azure deployment instance case.
             in multiple deployment instance case, it is possible that the job request land on one instance while the actually invite land on another instance
             for multiple instance case, the event handler Instance_HandleIncomingAudioVideoCall should always be there once service started
             * */
            AzureApplication.ApplicationEndpoint.HandleIncomingAudioVideoCall += Instance_HandleIncomingAudioVideoCall;
        }

        protected override void StopCore()
        {
            //stop communication listen to incoming messages
            AzureApplication.ApplicationEndpoint.HandleIncomingAudioVideoCall -= Instance_HandleIncomingAudioVideoCall;
        }

        /// <summary>
        /// Instance_HandleIncomingAudioVideoCall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_HandleIncomingAudioVideoCall(object sender, IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            //Start async since we do not want to block the event handler thread
            StartHuntGroupFlowAsync(e).ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "StartHuntGroupFlow failed with exception. Job id {0} Instance id {1}", this.JobId, this.InstanceId);
                    }
                }
                else
                {
                    Logger.Instance.Information("StartHuntGroupFlow completed, Job id {0} Instance id {1}", this.JobId, this.InstanceId);
                }
                this.CleanUpConversations();
            }
            );
        }

        private async Task StartHuntGroupFlowAsync(IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            Logger.Instance.Information(string.Format("[StartHuntGroupFlow] StartHuntGroupFlow: LoggingContext: {0}", LoggingContext));

            m_pstnCallConversation = null;
            m_outboundAVConversations = new List<IConversation>();

            // Step1: accept incoming call
            Logger.Instance.Information(string.Format("[StartHuntGroupFlow] Step 1: Accept incoming av call: LoggingContext: {0}", LoggingContext));
            await e.NewInvite.AcceptAsync(LoggingContext).ConfigureAwait(false);
            await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);

            // if everything is fine, you will be able to get the related conversation
            m_pstnCallConversation = e.NewInvite.RelatedConversation;
            m_pstnCallConversation.HandleResourceRemoved += HandlePSTNCallConversationRemoved;

            // Step 2 : wait AV flow connected and play Promt
            IAudioVideoCall pstnAv = m_pstnCallConversation.AudioVideoCall;
            IAudioVideoFlow pstnFlow = await pstnAv.WaitForAVFlowConnected().ConfigureAwait(false);
            pstnFlow.ToneReceivedEvent += HandleToneReceived;

            // Step 3 : play prompt

            // We support two modes in this job.
            //  A : InviteTargetUris are provided in the configuration; we will send invites to all of them and transfer the incoming call
            //      to whoever accepts it.
            //  B : InviteTargetUris not provided in the configuration; we will provide the user with a list of agents and let the user
            //      pick the target transfer using DTMF tones.
            bool skipDTMF = m_jobInput.InviteTargetUris != null && m_jobInput.InviteTargetUris.Length > 0;

            string wavFile = skipDTMF ? "HuntGroupA.wav" : "HuntGroupB.wav";
            var resourceUri = new Uri(string.Format(this.AzureApplication.ResourceUriFormat, wavFile));
            try
            {
                await pstnFlow.PlayPromptAsync(resourceUri, LoggingContext).ConfigureAwait(false);
            }
            catch (CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("[HuntGroupJob] PlayPrompt api is not available!", ex);
            }
            catch (RemotePlatformServiceException ex)
            {
                ClientModel.ErrorInformation error = ex.ErrorInformation;
                if (error != null && error.Code == ErrorCode.Informational && error.Subcode == ErrorSubcode.CallTerminated)
                {
                    Logger.Instance.Information("[HuntGroupJob] Call terminated while playing prompt.");
                }
                else
                {
                    throw;
                }
            }

            string callContext = pstnAv.CallContext;

            if (string.IsNullOrEmpty(callContext))
            {
                throw new PlatformserviceApplicationException("No valid callcontext in audioVideo resource ");
            }

            //Step 4 : Make out bound call to agents and do transfer
            ICommunication communication = m_pstnCallConversation.Parent as ICommunication;

            bool transferFlowSuccess = false;

            if (skipDTMF)
            {
                List<Task> TasksWithAgent = new List<Task>();
                foreach (string to in m_jobInput.InviteTargetUris)
                {
                    Task a = this.StartAgentCallAndTransferFlowAsync(communication, to, callContext).ContinueWith
                        (
                          pTask =>
                          {
                              if (pTask.IsFaulted)
                              {
                                  Logger.Instance.Warning("[HuntGroupJob] Transfer flow failed." + pTask.Exception);
                              }
                              else
                              {
                                  Logger.Instance.Information("[HuntGroupJob] Transfer flow complete.");
                                  transferFlowSuccess = true;
                              }
                          }
                        );
                    TasksWithAgent.Add(a);
                }

                await Task.WhenAll(TasksWithAgent.ToArray()).ConfigureAwait(false);
            }
            else //Upgraded version, with DTMF recognize
            {
                //wait tone
                string target = await m_toneReceived.Task.ConfigureAwait(false);
                try
                {
                    await this.StartAgentCallAndTransferFlowAsync(communication, target, callContext).ConfigureAwait(false);
                    transferFlowSuccess = true;
                }
                catch (CapabilityNotAvailableException ex)
                {
                    Logger.Instance.Warning("[HuntGroupJob] Transfer flow failed.", ex);
                }
                catch (RemotePlatformServiceException ex)
                {
                    Logger.Instance.Warning("[HuntGroupJob] Transfer flow failed.", ex);
                }
            }

            m_outboundCallTransferLock = 0;

            if (transferFlowSuccess)
            {
                Logger.Instance.Information("TransferFlow success");
            }
            else
            {
                Logger.Instance.Error("TransferFlow Failed, see above trace for error info");
            }
        }

        private void CleanUpConversations()
        {
            lock (m_syncRoot)
            {
                foreach (IConversation c in m_outboundAVConversations)
                {
                    c.DeleteAsync(LoggingContext).Observe<Exception>();
                }
            }
        }

        private async Task<IAudioVideoInvitation> EstablishCallWithAgentAsync(ICommunication communication, string agent)
        {
            CallbackContext callbackcontext = new CallbackContext { InstanceId = this.InstanceId, JobId = this.JobId };
            string callbackContextJsonString = JsonConvert.SerializeObject(callbackcontext);
            string CallbackUrl = string.Format(CultureInfo.InvariantCulture, AzureApplication.CallbackUriFormat, HttpUtility.UrlEncode(callbackContextJsonString));

            Logger.Instance.Information("Making outbound call to " + agent);

            IAudioVideoInvitation invite = await communication.StartAudioAsync("customer call", agent, CallbackUrl, LoggingContext).ConfigureAwait(false);
            await invite.WaitForInviteCompleteAsync().ConfigureAwait(false);
            IConversation c = invite.RelatedConversation;
            if (c.AudioVideoCall != null)
            {
                await c.AudioVideoCall.WaitForAVFlowConnected().ConfigureAwait(false);
            }
            return invite;
        }

        private async Task StartAgentCallAndTransferFlowAsync(ICommunication communication, string agent, string callContext)
        {
            IAudioVideoInvitation invite = await EstablishCallWithAgentAsync(communication, agent).ConfigureAwait(false);

            lock (m_syncRoot)
            {
                m_outboundAVConversations.Add(invite.RelatedConversation);
            }

            int result = Interlocked.Exchange(ref m_outboundCallTransferLock, 1);
            if (result == 0)
            {
                //Step 4: do transfer
                Logger.Instance.Information("[HuntGroupJob] Transferring call to " + agent);

                IAudioVideoCall av = invite.RelatedConversation.AudioVideoCall;

                ITransfer t = await av.TransferAsync(null, callContext, LoggingContext).ConfigureAwait(false);
                await t.WaitForTransferCompleteAsync().TimeoutAfterAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
                Logger.Instance.Information("[HuntGroupJob] Transfer completed successfully!");
            }
            else
            {
                // The call is already accepted and transfered by some one else
                Logger.Instance.Information("[HuntGroupJob] The call is already accepted and transfered by some one else; cancelling the transfer for " + agent);

                await invite.RelatedConversation.DeleteAsync(LoggingContext).ConfigureAwait(false);
            }
        }

        private void HandlePSTNCallConversationRemoved(object sender, PlatformResourceEventArgs args)
        {
            Logger.Instance.Information("Incoming pstn call conversation is removed");
        }

        private void HandleToneReceived(object sender, ToneReceivedEventArgs args)
        {
            switch (args.Tone)
            {
                case Rtc.Internal.Platform.ResourceContract.ToneValue.Tone1:
                    {
                        m_toneReceived.TrySetResult("sip:danewman@microsoft.com");
                        break;
                    }
                case Rtc.Internal.Platform.ResourceContract.ToneValue.Tone2:
                    {
                        m_toneReceived.TrySetResult("sip:ganeshsr@microsoft.com");
                        break;
                    }
                case Rtc.Internal.Platform.ResourceContract.ToneValue.Tone3:
                    {
                        m_toneReceived.TrySetResult("sip:jiche@microsoft.com");
                        break;
                    }
                case Rtc.Internal.Platform.ResourceContract.ToneValue.Tone4:
                    {
                        m_toneReceived.TrySetResult("sip:sankum@microsoft.com");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
