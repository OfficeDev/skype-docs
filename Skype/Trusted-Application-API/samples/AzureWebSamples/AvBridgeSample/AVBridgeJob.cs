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

namespace AVBridgeSample
{
    public class AVBridgeJob
    {
        private string m_inviteTargetUri { get; set; }

        private Uri m_callbackUri;

        private IncomingInviteEventArgs<IAudioVideoInvitation> m_incomingInvitation;

        private IConversation m_pstnCallConversation;

        private IConversation m_confConversation;
        private object m_syncRoot = new object();

        private IApplication m_application;

        private string m_jobId;

        private LoggingContext m_loggingContext;

        public AVBridgeJob(IncomingInviteEventArgs<IAudioVideoInvitation> incomingInvitation, IApplication application, string inviteTarget, string callbackUri)
        {
            m_incomingInvitation = incomingInvitation;
            m_jobId = Guid.NewGuid().ToString();
            m_inviteTargetUri = inviteTarget;
            if (string.IsNullOrEmpty(m_inviteTargetUri))
            {
                throw new ArgumentNullException("Failed to get inviteTarget!");
            }
            m_callbackUri = new Uri(callbackUri);
            m_application = application;
            m_loggingContext = new LoggingContext(m_jobId, string.Empty);
        }

        public void Start()
        {
            //Start async since we do not want to block the event handler thread
            StartAVBridgeFlowAsync(m_incomingInvitation).ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "StartAVBridgeFlowAsync failed with exception. Job id {0} ", m_jobId);
                    }
                }
                else
                {
                    Logger.Instance.Information("StartAVBridgeFlowAsync completed, Job id {0}", m_jobId);
                }
            }
            );
        }

        private async Task StartAVBridgeFlowAsync(IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            Logger.Instance.Information(string.Format("[StartAVBridgeFlowAsync] StartAVBridgeFlowAsync: LoggingContext: {0}", m_loggingContext));

            m_pstnCallConversation = null;
            m_confConversation = null;
            string meetingUrl = string.Empty;

            #region Step 1 Start adhoc meeting
            //Step1:
            Logger.Instance.Information(string.Format("[StartAVBridgeFlowAsync] Step 1: Start adhoc meeting: LoggingContext: {0}", m_loggingContext));

            CallbackContext callbackcontext = new CallbackContext { JobId = m_jobId };
            string callbackContextJsonString = JsonConvert.SerializeObject(callbackcontext);

            IOnlineMeetingInvitation onlineMeetingInvite =
                await m_application.Communication.StartAdhocMeetingAsync(
                    e.NewInvite,
                    "customer support on audioVideo bridge",
                    callbackContextJsonString,
                    m_loggingContext)
                .ConfigureAwait(false);

            if (string.IsNullOrEmpty(onlineMeetingInvite.MeetingUrl))
            {
                throw new Exception("Do not get valid MeetingUrl on onlineMeetingInvitation resource after startAdhocMeeting!");
            }

            meetingUrl = onlineMeetingInvite.MeetingUrl;

            Logger.Instance.Information(string.Format("[StartAVBridgeFlowAsync] Get meeting uri: {0} LoggingContext: {1}", onlineMeetingInvite.MeetingUrl, m_loggingContext));

            //wait on embedded onlinemeetingInvitation to complete, so that we can have valid related conversation
            await onlineMeetingInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);
            //this is conference conversation leg
            m_confConversation = onlineMeetingInvite.RelatedConversation;
            if (m_confConversation == null)
            {
                throw new Exception("onlineMeetingInvite.RelatedConversation is null? this is propably app code bug!");
            }
            #endregion

            #region Step 2 Start AcceptAndBridge
            //Step2:
            Logger.Instance.Information(string.Format("[StartAVBridgeFlowAsync] Step2:  Start AcceptAndBridge: LoggingContext: {0}", m_loggingContext));
            await e.NewInvite.AcceptAndBridgeAsync(meetingUrl, m_loggingContext).ConfigureAwait(false);
            await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);

            m_pstnCallConversation = e.NewInvite.RelatedConversation;

            //This is to clean the conf conversation leg when the p2p conversation is removed
            m_pstnCallConversation.HandleResourceRemoved += (o, args) =>
            {
                m_pstnCallConversation.HandleResourceRemoved = null;
                this.HandlePSTNCallConversationRemoved(o,args);
            };
            /*
            IAudioVideoCall p2pCall = m_pstnCallConversation.AudioVideoCall;
            if (p2pCall == null || p2pCall.State != Microsoft.Rtc.Internal.Platform.ResourceContract.CallState.Connected)
            {
                Logger.Instance.Error(string.Format("[StartAVBridgeFlowAsync] p2pCall is null or not in connected state: LoggingContext: {0}", m_loggingContext));
                throw new Exception("[StartAVBridgeFlowAsync] p2pCall is null or not in connected state");
            }
            */
            #endregion

            #region Step 3 add av modality on conference conversation
            //Step3:
            Logger.Instance.Information(string.Format("[StartAVBridgeFlowAsync] Step3: add AV modality on conference conversation: LoggingContext: {0}", m_loggingContext));
            IAudioVideoCall confAv = m_confConversation.AudioVideoCall;
            if (confAv == null)
            {
                throw new Exception("[InstantMessagingBridgeFlow] No valid Messaging resource on conference conversation");
            }
            IAudioVideoInvitation confAVInvite = await confAv.EstablishAsync(m_loggingContext).ConfigureAwait(false);
            await confAVInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);

            IAudioVideoFlow confAVFlow = await confAv.WaitForAVFlowConnected().ConfigureAwait(false);
            #endregion

            #region Step 4 : play prompt
            // Step 4 : play prompt
            string wavFile =  "CallCenterSample.wav" ;
            var resourceUri = new Uri(string.Format("{0}://{1}/resources/{2}", m_callbackUri.Scheme, m_callbackUri.Host, wavFile));
            try
            {
                await confAVFlow.PlayPromptAsync(resourceUri, m_loggingContext).ConfigureAwait(false);
            }
            catch (CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("[CallCenterJob] PlayPrompt api is not available!", ex);
                throw;
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
            #endregion

            #region Step 5 Sleep for a while
            await Task.Delay(1000).ConfigureAwait(false);
            #endregion

            #region Step 6 Invite a agent to meeting
           IParticipantInvitation participantInvite = await m_confConversation.AddParticipantAsync(new SipUri(m_inviteTargetUri), m_loggingContext).ConfigureAwait(false);
           await participantInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);

            #endregion
        }

        private void CleanUpConversations()
        {
            m_confConversation.DeleteAsync(m_loggingContext).Observe<Exception>();
            m_pstnCallConversation.DeleteAsync(m_loggingContext).Observe<Exception>();
        }

        private void HandlePSTNCallConversationRemoved(object sender, PlatformResourceEventArgs args)
        {
            Logger.Instance.Information("Incoming pstn call conversation is removed");
            this.CleanUpConversations();
        }
    }
}
