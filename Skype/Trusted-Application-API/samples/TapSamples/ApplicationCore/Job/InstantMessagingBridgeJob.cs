using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.Rtc.Internal.Platform.ResourceContract;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public class InstantMessagingBridgeJob : PlatformServiceListeningJobBase
    {
        private InstantMessagingBridgeJobInput m_handleIncomingMessageInput;

        private IConversation m_confConversation;

        private IConversation m_p2pConversation;

        public InstantMessagingBridgeJob(string jobid, string instanceid, AzureBasedApplicationBase azureApplication, InstantMessagingBridgeJobInput input)
            : base(jobid, instanceid, azureApplication, input, JobType.InstantMessagingBridge)
        {
            m_handleIncomingMessageInput = base.JobInput as InstantMessagingBridgeJobInput;
            if (m_handleIncomingMessageInput == null)
            {
                throw new ArgumentNullException("Failed to get job input as InstantMessagingBridgeJobInput!");
            }
        }

        protected override void StartCore()
        {
            //start communication listen to incoming messages
            /*
             This job kick off mode does not apply to multiple azure deployment instance case.
             in multiple deployment instance case, it is possible that the job request land on one instance while the actually invite land on another instance
             for multiple instance case, the event handler Instance_HandleIncomingInstantMessagingCall should always be there once service started
             * */
            AzureApplication.ApplicationEndpoint.HandleIncomingInstantMessagingCall += Instance_HandleIncomingInstantMessagingCall;
        }

        protected override void StopCore()
        {
            //stop communication listen to incoming messages
            AzureApplication.ApplicationEndpoint.HandleIncomingInstantMessagingCall -= Instance_HandleIncomingInstantMessagingCall;
        }

        /// <summary>
        /// HandleIncomingInstantMessagingCall
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Instance_HandleIncomingInstantMessagingCall(object sender, IncomingInviteEventArgs<IMessagingInvitation> e)
        {
            //Start async since we do not want to block the event handler thread
            StartInstantMessagingBridgeFlowAsync(e).ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    //Clean up only in fault case, if success, leave it in established state and rely on client chat to terminate the call
                    this.CleanUpConversations();
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "InstantMessagingBridgeFlow failed with exception. Job id {0} Instance id {1}", this.JobId, this.InstanceId);
                    }
                }
                else
                {
                    Logger.Instance.Information("InstantMessagingBridgeFlow completed, Job id {0} Instance id {1}", this.JobId, this.InstanceId);
                }
            }
            );
        }

        private void OnIncomingMessageReceived(object sender, IncomingMessageEventArgs args)
        {
            string msg = string.Empty;
            TextHtmlMessage HtmlMessage = args.HtmlMessage ?? null;
            TextPlainMessage PlainMessage = args.PlainMessage ?? null;

            if (HtmlMessage != null)
            {
                msg = System.Text.Encoding.UTF8.GetString(HtmlMessage.Message);
            }
            if (PlainMessage != null)
            {
                msg = System.Text.Encoding.UTF8.GetString(PlainMessage.Message);
            }
            Logger.Instance.Information("[HandleIncomingMessageJob] Get incoming message from " + args.FromParticipantName + " : " + msg);
        }

        private async Task StartInstantMessagingBridgeFlowAsync(IncomingInviteEventArgs<IMessagingInvitation> e)
        {
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] StartInstantMessagingBridgeFlow: LoggingContext: {0}", LoggingContext));

            CallbackContext callbackcontext = new CallbackContext { InstanceId = this.InstanceId, JobId = this.JobId };
            string callbackContextJsonString = JsonConvert.SerializeObject(callbackcontext);
            string CallbackUrl = string.Format(CultureInfo.InvariantCulture, AzureApplication.CallbackUriFormat, HttpUtility.UrlEncode(callbackContextJsonString));

            string meetingUrl = string.Empty;

            //There will be two conversation legs for the saas app
            m_p2pConversation = null;
            m_confConversation = null;

            #region Step 1 Start adhoc meeting
            //Step1:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step 1: Start adhoc meeting: LoggingContext: {0}", LoggingContext));

            IOnlineMeetingInvitation onlineMeetingInvite = await e.NewInvite.StartAdhocMeetingAsync(m_handleIncomingMessageInput.Subject, CallbackUrl, LoggingContext).ConfigureAwait(false);

            if (string.IsNullOrEmpty(onlineMeetingInvite.MeetingUrl))
            {
                throw new PlatformserviceApplicationException("Do not get valid MeetingUrl on onlineMeetingInvitation resource after startAdhocMeeting!");
            }
            meetingUrl = onlineMeetingInvite.MeetingUrl;

            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Get meeting uri: {0} LoggingContext: {1}", onlineMeetingInvite.MeetingUrl, LoggingContext));

            //wait on embedded onlinemeetingInvitation to complete, so that we can have valid related conversation
            await onlineMeetingInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);
            //this is conference conversation leg
            m_confConversation = onlineMeetingInvite.RelatedConversation;
            if (m_confConversation == null)
            {
                throw new PlatformserviceApplicationException("onlineMeetingInvite.RelatedConversation is null? this is propably app code bug!");
            }
            #endregion

            #region Step 2 add Messaging modality on conference conversation
            //Step2:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step2: add Messaging modality on conference conversation: LoggingContext: {0}", LoggingContext));
            IMessagingCall confMessaging = m_confConversation.MessagingCall;
            if (confMessaging == null)
            {
                throw new PlatformserviceApplicationException("[InstantMessagingBridgeFlow] No valid Messaging resource on conference conversation");
            }
            //Hook up the event handler on "MessagingModality" of the conference leg and make sure what ever message anon user , or agent input , the app can all know and note down
            confMessaging.IncomingMessageReceived += OnIncomingMessageReceived;
            m_confConversation.HandleParticipantChange += this.OnParticipantChange;
            IMessagingInvitation messageInvitation = await confMessaging.EstablishAsync(LoggingContext).ConfigureAwait(false);
            await messageInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);//messageInvitation cannot be null here
            #endregion

            #region Step 3 Start AcceptAndBridge
            //Step3:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step3:  Start AcceptAndBridge: LoggingContext: {0}", LoggingContext));
            await e.NewInvite.AcceptAndBridgeAsync(LoggingContext, meetingUrl, m_handleIncomingMessageInput.Subject).ConfigureAwait(false);
            await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);
            m_p2pConversation = e.NewInvite.RelatedConversation;

            //This is to clean the conf conversation leg when the p2p conversation is removed
            m_p2pConversation.HandleResourceRemoved += (o, args) =>
            {
                m_p2pConversation.HandleResourceRemoved = null;
                this.OnClientChatDisconnected();
            };

            IMessagingCall p2pMessaging = m_p2pConversation.MessagingCall;
            if (p2pMessaging == null || p2pMessaging.State != Rtc.Internal.Platform.ResourceContract.CallState.Connected)
            {
                Logger.Instance.Error(string.Format("[InstantMessagingBridgeFlow] p2pMessaging is null or not in connected state: LoggingContext: {0}", LoggingContext));
                throw new PlatformserviceApplicationException("[InstantMessagingBridgeFlow] p2pMessaging is null or not in connected state");
            }
            #endregion

            #region Step 4 Send welcome message
            //Step4:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step4:  Send welcome message: LoggingContext: {0}", LoggingContext));
            await p2pMessaging.SendMessageAsync(m_handleIncomingMessageInput.WelcomeMessage, LoggingContext).ConfigureAwait(false);
            #endregion

            #region Step 5 Add bridged participant to enable agent send message to client chat
            //Step5:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step5: Add bridged participant to enable agent send message to client chat. LoggingContext: {0}", LoggingContext));
            IConversationBridge conversationBridge = m_p2pConversation.ConversationBridge;
            if (conversationBridge == null)
            {
                Logger.Instance.Error(string.Format("[InstantMessagingBridgeFlow] conversationBridge == null after accept and bridge. LoggingContext: {0}", LoggingContext));
                throw new PlatformserviceApplicationException("[InstantMessagingBridgeFlow]  conversationBridge == null after accept and bridge");
            }
            await conversationBridge.AddBridgedParticipantAsync(LoggingContext, m_handleIncomingMessageInput.InvitedTargetDisplayName, m_handleIncomingMessageInput.InviteTargetUri, false).ConfigureAwait(false);
            #endregion

            #region Step 6 Start addParticipant to conference
            //Step 6:
            Logger.Instance.Information(string.Format("[HandleIncomingMessageJob] Step6: Start addParticipant to conference: LoggingContext: {0}", LoggingContext));
            IParticipantInvitation participantInvitation = await m_confConversation.AddParticipantAsync(m_handleIncomingMessageInput.InviteTargetUri, LoggingContext).ConfigureAwait(false);
            await participantInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);
            #endregion
        }

        private void OnClientChatDisconnected()
        {
            if (m_confConversation != null && m_confConversation.State == Rtc.Internal.Platform.ResourceContract.ConversationState.Conferenced)
            {
                m_confConversation.DeleteAsync(LoggingContext).Observe<Exception>();
            }
        }

        private void OnParticipantChange(object sender, ParticipantChangeEventArgs args)
        {
            if (args.AddedParticipants != null)
            {
                args.AddedParticipants[0].HandleParticipantModalityChange += OnParticipantModalityChange;
                Logger.Instance.Information("Participant " + args.AddedParticipants[0].Name + " added");
            }

            if (args.UpdatedParticipants != null)
            {
                Logger.Instance.Information("Participant " + args.UpdatedParticipants[0].Name + " updated");
            }

            if (args.RemovedParticipants != null)
            {
                Logger.Instance.Information("Participant " + args.RemovedParticipants[0].Name + " removed");
                args.RemovedParticipants[0].HandleParticipantModalityChange -= OnParticipantModalityChange;
            }
        }

        private void OnParticipantModalityChange(object sender, ParticipantModalityChangeEventArgs args)
        {
            if (args.AddedModalities != null)
            {
                if (args.AddedModalities[0] is IParticipantMessaging)
                {
                    IParticipantMessaging added = args.AddedModalities[0] as IParticipantMessaging;
                    Logger.Instance.Information("Participant Messaging added in " + ((IParticipant)added.Parent).Name);
                }
            }

            if (args.RemovedModalities != null)
            {
                if (args.RemovedModalities[0] is IParticipantMessaging)
                {
                    IParticipantMessaging removed = args.RemovedModalities[0] as IParticipantMessaging;
                    Logger.Instance.Information("Participant Messaging removed in " + ((IParticipant)removed.Parent).Name);
                }
            }
        }

        private void CleanUpConversations()
        {
            if (m_confConversation != null && m_confConversation.State == ConversationState.Conferenced)
            {
                m_confConversation.DeleteAsync(LoggingContext).Observe<Exception>();
            }

            if (m_p2pConversation != null && m_p2pConversation.State == ConversationState.Connected)
            {
                m_p2pConversation.DeleteAsync(LoggingContext).Observe<Exception>();
            }
        }
    }
}
