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

namespace CallTransferSample
{
    public class CallCenterJob
    {
        private string[] m_inviteTargetUris { get; set; }

        private Uri m_callbackUri;
             
        private IncomingInviteEventArgs<IAudioVideoInvitation> m_incomingInvitation;

        private IConversation m_pstnCallConversation;

        private List<IConversation> m_outboundAVConversations;

        private object m_syncRoot = new object();

        private int m_outboundCallTransferLock;

        private IApplication m_application;

        private string m_jobId;

        private LoggingContext m_loggingContext;


        public CallCenterJob(IncomingInviteEventArgs<IAudioVideoInvitation> incomingInvitation, IApplication application, string[] inviteTargets, string callbackUri)
        {
            m_incomingInvitation = incomingInvitation;
            m_jobId = Guid.NewGuid().ToString();
            m_inviteTargetUris = inviteTargets;
            if (m_inviteTargetUris == null || m_inviteTargetUris.Length == 0)
            {
                throw new ArgumentNullException("Failed to get job input as CallCenterJobInput!");
            }
            m_callbackUri = new Uri(callbackUri);
            m_application = application;
            m_loggingContext = new LoggingContext(m_jobId, string.Empty);
        }

        public void Start()
        {
            //Start async since we do not want to block the event handler thread
            StartCallCenterFlowAsync(m_incomingInvitation).ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "StartHuntGroupFlow failed with exception. Job id {0} ", m_jobId);
                    }
                }
                else
                {
                    Logger.Instance.Information("StartCallCenterFlowAsync completed, Job id {0}", m_jobId);
                }
                this.CleanUpConversations();
            }
            );
        }

        private async Task StartCallCenterFlowAsync(IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            Logger.Instance.Information(string.Format("[StartCallCenterFlowAsync] StartCallCenterFlowAsync: LoggingContext: {0}", m_loggingContext));

            m_pstnCallConversation = null;
            m_outboundAVConversations = new List<IConversation>();

            // Step1: accept incoming call
            Logger.Instance.Information(string.Format("[StartCallCenterFlowAsync] Step 1: Accept incoming av call: LoggingContext: {0}", m_loggingContext));
            await e.NewInvite.AcceptAsync(m_loggingContext).ConfigureAwait(false);
            await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);

            // if everything is fine, you will be able to get the related conversation
            m_pstnCallConversation = e.NewInvite.RelatedConversation;
            m_pstnCallConversation.HandleResourceRemoved += HandlePSTNCallConversationRemoved;

            // Step 2 : wait AV flow connected and play Promt
            IAudioVideoCall pstnAv = m_pstnCallConversation.AudioVideoCall;
            IAudioVideoFlow pstnFlow = await pstnAv.WaitForAVFlowConnected().ConfigureAwait(false);

            // Step 3 : play prompt

           
            // InviteTargetUris are provided in the configuration; we will send invites to all of them and transfer the incoming call
            //      to whoever accepts it.
       
            string wavFile =  "CallTransferSample.wav" ;
            var resourceUri = new Uri(string.Format("{0}://{1}/resources/{2}", m_callbackUri.Scheme, m_callbackUri.Host, wavFile));
            try
            {
                await pstnFlow.PlayPromptAsync(resourceUri, m_loggingContext).ConfigureAwait(false);
            }
            catch (CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("[CallCenterJob] PlayPrompt api is not available!", ex);
            }
            catch (RemotePlatformServiceException ex)
            {
                Microsoft.SfB.PlatformService.SDK.ClientModel.ErrorInformation error = ex.ErrorInformation;
                if (error != null && error.Code == ErrorCode.Informational && error.Subcode == ErrorSubcode.CallTerminated)
                {
                    Logger.Instance.Information("[CallCenterJob] Call terminated while playing prompt.");
                }
                else
                {
                    throw;
                }
            }

            string callContext = pstnAv.CallContext;

            if (string.IsNullOrEmpty(callContext))
            {
                throw new Exception("No valid callcontext in audioVideo resource ");
            }

            //Step 4 : Make out bound call to agents and do transfer
            ICommunication communication = m_pstnCallConversation.Parent as ICommunication;

            bool transferFlowSuccess = false;
            
            List<Task> TasksWithAgent = new List<Task>();
            foreach (string to in m_inviteTargetUris)
            {
                Task a = this.StartAgentCallAndTransferFlowAsync(communication, to, callContext).ContinueWith
                    (
                        pTask =>
                        {
                            if (pTask.IsFaulted)
                            {
                                Logger.Instance.Warning("[CallCenterJob] Transfer flow failed." + pTask.Exception);
                            }
                            else
                            {
                                Logger.Instance.Information("[CallCenterJob] Transfer flow complete.");
                                transferFlowSuccess = true;
                            }
                        }
                    );
                TasksWithAgent.Add(a);
            }

            await Task.WhenAll(TasksWithAgent.ToArray()).ConfigureAwait(false);

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
                    c.DeleteAsync(m_loggingContext).Observe<Exception>();
                }
            }
        }

        private async Task<IAudioVideoInvitation> EstablishCallWithAgentAsync(ICommunication communication, string agent)
        {
            CallbackContext callbackcontext = new CallbackContext { JobId = m_jobId };
          
            string callbackContextJsonString = JsonConvert.SerializeObject(callbackcontext);
            string CallbackUrl = string.Format("{0}?callbackContext={1}", m_callbackUri.ToString() ,HttpUtility.UrlEncode(callbackContextJsonString));

            Logger.Instance.Information("Making outbound call to " + agent);
            IAudioVideoInvitation invite = await communication.StartAudioAsync("customer call", new SipUri(agent), CallbackUrl, m_loggingContext).ConfigureAwait(false);
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
                Logger.Instance.Information("[CallCenterJob] Transferring call to " + agent);

                IAudioVideoCall av = invite.RelatedConversation.AudioVideoCall;

                ITransfer t = await av.TransferAsync(callContext, m_loggingContext).ConfigureAwait(false);
                await t.WaitForTransferCompleteAsync().TimeoutAfterAsync(TimeSpan.FromSeconds(30)).ConfigureAwait(false);
                Logger.Instance.Information("[CallCenterJob] Transfer completed successfully!");
            }
            else
            {
                // The call is already accepted and transfered by some one else
                Logger.Instance.Information("[CallCenterJob] The call is already accepted and transfered by some one else; cancelling the transfer for " + agent);

                await invite.RelatedConversation.DeleteAsync(m_loggingContext).ConfigureAwait(false);
            }
        }

        private void HandlePSTNCallConversationRemoved(object sender, PlatformResourceEventArgs args)
        {
            Logger.Instance.Information("Incoming pstn call conversation is removed");
        }
    }
}
