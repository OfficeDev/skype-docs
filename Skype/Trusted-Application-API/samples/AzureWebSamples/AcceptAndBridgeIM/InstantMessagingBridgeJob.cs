using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.Rtc.Internal.Platform.ResourceContract;

namespace AcceptAndBridgeIM
{
    public class InstantMessagingBridgeJob 
    {
        private InstantMessagingBridgeJobInput m_handleIncomingMessageInput;

        private IConversation m_confConversation;

        private IConversation m_p2pConversation;
        private string JobId;
        private string InstanceId;

        protected LoggingContext m_loggingContext { get; private set; }


        public InstantMessagingBridgeJob(string jobid, string instanceid,InstantMessagingBridgeJobInput input)
        {
            this.JobId = jobid;
            this.InstanceId = instanceid;
            m_loggingContext = new LoggingContext(JobId, InstanceId);

            m_handleIncomingMessageInput = input;
            if (m_handleIncomingMessageInput == null)
            {
                throw new ArgumentNullException("Failed to get job input as InstantMessagingBridgeJobInput!");
            }
        }

        public void Start()
        {
            //start communication listen to incoming messages
            /*
             This job kick off mode does not apply to multiple azure deployment instance case.
             in multiple deployment instance case, it is possible that the job request land on one instance while the actually invite land on another instance
             for multiple instance case, the event handler Instance_HandleIncomingInstantMessagingCall should always be there once service started
             * */
            WebApiApplication.ApplicationEndpoint.HandleIncomingInstantMessagingCall += Instance_HandleIncomingInstantMessagingCall;
            Logger.Instance.Information("HandleIncomingInstantMessagingCall started, Job id {0} Instance id {1}", this.JobId, this.InstanceId);
        }

        public void Stop()
        {
            //stop communication listen to incoming messages
            WebApiApplication.ApplicationEndpoint.HandleIncomingInstantMessagingCall -= Instance_HandleIncomingInstantMessagingCall;
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
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] StartInstantMessagingBridgeFlow: LoggingContext: {0}", m_loggingContext));

            CallbackContext callbackcontext = new CallbackContext { InstanceId = this.InstanceId, JobId = this.JobId };
            string callbackContextJsonString = JsonConvert.SerializeObject(callbackcontext);
            string CallbackUrl = string.Format(CultureInfo.InvariantCulture, WebApiApplication.CallbackUri, HttpUtility.UrlEncode(callbackContextJsonString));

            string meetingUrl = string.Empty;

            //There will be two conversation legs for the saas app
            m_p2pConversation = null;
            m_confConversation = null;

            #region Step 1 Start adhoc meeting
            //Step1:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step 1: Start adhoc meeting: LoggingContext: {0}", m_loggingContext));            
            IOnlineMeetingInvitation onlineMeetingInvite =
                await WebApiApplication.ApplicationEndpoint.Application.Communication.StartAdhocMeetingAsync(e.NewInvite,m_handleIncomingMessageInput.Subject, CallbackUrl, m_loggingContext).ConfigureAwait(false);
            
            if (string.IsNullOrEmpty(onlineMeetingInvite.MeetingUrl))
            {
                throw new PlatformserviceApplicationException("Do not get valid MeetingUrl on onlineMeetingInvitation resource after startAdhocMeeting!");
            }
            meetingUrl = onlineMeetingInvite.MeetingUrl;

            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Get meeting uri: {0} LoggingContext: {1}", onlineMeetingInvite.MeetingUrl, m_loggingContext));

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
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step2: add Messaging modality on conference conversation: LoggingContext: {0}", m_loggingContext));
            IMessagingCall confMessaging = m_confConversation.MessagingCall;
            if (confMessaging == null)
            {
                throw new PlatformserviceApplicationException("[InstantMessagingBridgeFlow] No valid Messaging resource on conference conversation");
            }
            //Hook up the event handler on "MessagingModality" of the conference leg and make sure what ever message anon user , or agent input , the app can all know and note down
            confMessaging.IncomingMessageReceived += OnIncomingMessageReceived;
            m_confConversation.HandleParticipantChange += this.OnParticipantChange;
            IMessagingInvitation messageInvitation = await confMessaging.EstablishAsync(m_loggingContext).ConfigureAwait(false);
            await messageInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);//messageInvitation cannot be null here
            #endregion

            #region Step 3 Start AcceptAndBridge
            //Step3:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step3:  Start AcceptAndBridge: LoggingContext: {0}", m_loggingContext));                        
            await e.NewInvite.AcceptAndBridgeAsync(meetingUrl, m_handleIncomingMessageInput.Subject,m_loggingContext).ConfigureAwait(false);

            await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);
            m_p2pConversation = e.NewInvite.RelatedConversation;

            //This is to clean the conf conversation leg when the p2p conversation is removed
            m_p2pConversation.HandleResourceRemoved += (o, args) =>
            {
                m_p2pConversation.HandleResourceRemoved = null;
                this.OnClientChatDisconnected();
            };

            IMessagingCall p2pMessaging = m_p2pConversation.MessagingCall;
            if (p2pMessaging == null || p2pMessaging.State != CallState.Connected)
            {
                Logger.Instance.Error(string.Format("[InstantMessagingBridgeFlow] p2pMessaging is null or not in connected state: LoggingContext: {0}", m_loggingContext));
                throw new PlatformserviceApplicationException("[InstantMessagingBridgeFlow] p2pMessaging is null or not in connected state");
            }
            #endregion

            #region Step 4 Send welcome message
            //Step4:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step4:  Send welcome message: LoggingContext: {0}", m_loggingContext));
            await p2pMessaging.SendMessageAsync(m_handleIncomingMessageInput.WelcomeMessage, m_loggingContext).ConfigureAwait(false);
            #endregion

            #region Step 5 Add bridged participant to enable agent send message to client chat

            //Step5:
            Logger.Instance.Information(string.Format("[InstantMessagingBridgeFlow] Step5: Add bridged participant to enable agent send message to client chat. LoggingContext: {0}", m_loggingContext));
            IConversationBridge conversationBridge = m_p2pConversation.ConversationBridge;
            if (conversationBridge == null)
            {
                Logger.Instance.Error(string.Format("[InstantMessagingBridgeFlow] conversationBridge == null after accept and bridge. LoggingContext: {0}", m_loggingContext));
                throw new PlatformserviceApplicationException("[InstantMessagingBridgeFlow]  conversationBridge == null after accept and bridge");
            }            
            await conversationBridge.AddBridgedParticipantAsync(m_handleIncomingMessageInput.InvitedTargetDisplayName, new SipUri(m_handleIncomingMessageInput.InviteTargetUri), m_handleIncomingMessageInput.EnableMessageFilter, m_loggingContext);
            #endregion

            #region Step 6 Start addParticipant to conference
            //Step 6:
            Logger.Instance.Information(string.Format("[HandleIncomingMessageJob] Step6: Start addParticipant to conference: LoggingContext: {0}", m_loggingContext));
            IParticipantInvitation participantInvitation = await m_confConversation.AddParticipantAsync(new SipUri(m_handleIncomingMessageInput.InviteTargetUri), m_loggingContext).ConfigureAwait(false);
            await participantInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);
            #endregion


            #region Step 7 Send custom message to conference
            string customContent = e.NewInvite.CustomContent;

            if (!string.IsNullOrEmpty(customContent) && !string.IsNullOrWhiteSpace(customContent))
            {
                Logger.Instance.Information(string.Format("[HandleIncomingMessageJob] Step 7 : Start send customcontent to conference: LoggingContext: {0}", m_loggingContext));
                await confMessaging.SendMessageAsync(customContent, m_loggingContext).ConfigureAwait(false);
            }
            #endregion
        }

        private void OnClientChatDisconnected()
        {
            if (m_confConversation != null && m_confConversation.State == ConversationState.Conferenced)
            {
                m_confConversation.DeleteAsync(m_loggingContext).Observe<Exception>();
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
                m_confConversation.DeleteAsync(m_loggingContext).Observe<Exception>();
            }

            if (m_p2pConversation != null && m_p2pConversation.State == ConversationState.Connected)
            {
                m_p2pConversation.DeleteAsync(m_loggingContext).Observe<Exception>();
            }
        }
    }
}
