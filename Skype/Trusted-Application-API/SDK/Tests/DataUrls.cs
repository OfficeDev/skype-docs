namespace Microsoft.SfB.PlatformService.SDK.Tests
{
    /// <summary>
    /// Contains all the urls stored in Data\*.json files
    /// </summary>
    public static class DataUrls
    {
        // Applications urls
        public  const string Discover        = "https://noammeetings.resources.lync.com/platformservice/discover?deploymentpreference=Weekly";
        private const string RingDomain      = "https://ring2noammeetings.resources.lync.com";
        private const string PoolDomain      = "https://webpoolbl20r04.infra.lync.com";
        private const string EndpointId      = "endpointId=sip:monitoringaudio@0mcorp2cloudperf.onmicrosoft.com";
        private const string ApplicationPath = RingDomain + "/platformservice/v1/applications/1393347000";
        public  const string Applications    = RingDomain + "/platformservice/v1/applications?" + EndpointId;

        // Application urls
        public  const string Application      = ApplicationPath + "?" + EndpointId;
        public  const string Communication    = ApplicationPath + "/communication?" + EndpointId;
        public  const string AdhocMeeting     = ApplicationPath + "/adhocMeetings?" + EndpointId;
        public  const string AnonToken        = ApplicationPath + "/anonApplicationTokens?" + EndpointId;
        private const string TgtApplication   = PoolDomain + "/platformservice/tgt-8c81281c925a5c2ea02ec14ac1b492c6/v1/applications/1393347000";
        private const string TgtCommunication = TgtApplication + "/communication";

        // AdhocMeeting urls
        private const string OnlineMeetingInvitations = PoolDomain + "/platformservice/v1/applications/1393347000/communication/onlineMeetingInvitations";
        public  const string JoinAdhocMeeting         = OnlineMeetingInvitations + "?confUrl=sip:BL20R04meet1692@noammeetings.lync.com;gruu;opaque=app:conf:focus:id:YOKMXY1C&" + EndpointId;

        // Outgoing MessagingInvitation urls
        public const string MessagingInvitations  = ApplicationPath + "/communication/messagingInvitations?" + EndpointId;

        // Outgoing AudioVideoInvitation urls
        public  const string AudioInvitations      = ApplicationPath + "/communication/audioVideoInvitations?modalities=Audio&" + EndpointId;
        public  const string AudioVideoInvitations = ApplicationPath + "/communication/audioVideoInvitations?modalities=AudioVideo&" + EndpointId;

        // Outgoing ParticipantInvitation urls
        public const string ParticipantInvitation = TgtCommunication + "/participantInvitations?"+EndpointId+"&conversationId=869ce4f6-0076-483a-a7c1-968f6b935afe";

        // Incoming MessagingInvitation urls
        public const string AcceptAndBridge   = TgtCommunication + "/messagingInvitations/54537d00-1b4f-44fd-b12c-cbc8ce32317f/acceptAndBridge?" + EndpointId;
        public const string StartAdhocMeeting = TgtCommunication + "/onlineMeetingInvitations/startAdhocMeeting?" + EndpointId;

        // Incoming AudioVideoInvitation urls
        private const string AudioVideoInvitationBase    = TgtCommunication + "/audioVideoInvitations/3f96f5b2-9369-4b5e-b26c-5bc79caa05a6";
        public  const string AudioVideoInvitationAccept  = AudioVideoInvitationBase + "/accept?" + EndpointId;
        public  const string AudioVideoInvitationDecline = AudioVideoInvitationBase + "/decline?" + EndpointId;
        public  const string AudioVideoInvitationForward = AudioVideoInvitationBase + "/forward?" + EndpointId;
        public const string  AudioVideoInvitationAcceptAndBridge = AudioVideoInvitationBase + "/acceptAndBridgeAudioVideo?" + EndpointId;

        // Conversation urls
        private const string TgtConversation                 = TgtCommunication + "/conversations/869ce4f6-0076-483a-a7c1-968f6b935afe";
        public  const string Conversation                    = TgtConversation + "?" + EndpointId;
        public  const string Participant                     = TgtConversation + "/participants/participant@example.com?" + EndpointId;
        public  const string SendMessage                     = TgtConversation + "/messaging/messages?" + EndpointId;
        public  const string TerminateMessagingCall          = TgtConversation + "/messaging/terminate?" + EndpointId;
        public  const string EstablishMessagingCall          = TgtCommunication + "/messagingInvitations?conversationId=869ce4f6-0076-483a-a7c1-968f6b935afe&" + EndpointId;
        public  const string EstablishAudioCall              = TgtCommunication + "/audioVideoInvitations?" + EndpointId + "&conversationId=869ce4f6-0076-483a-a7c1-968f6b935afe&modalities=Audio";
        public  const string EstablishAudioVideoCall         = EstablishAudioCall + "Video";
        public  const string TerminateAudioVideoCall         = TgtConversation + "/audioVideo/terminate?" + EndpointId;
        public  const string Transfer                        = TgtConversation + "/audioVideo/transfer?" + EndpointId;
        public  const string StartPrompt                     = TgtConversation + "/audioVideo/audioVideoFlow/prompts?" + EndpointId;
        public  const string StopPrompts                     = TgtConversation + "/audioVideo/audioVideoFlow/stopPrompts?" + EndpointId;
        public  const string BridgedParticipants             = TgtConversation + "/conversationBridge/bridgedParticipants?" + EndpointId;
        public  const string BridgedParticipant              = TgtConversation + "/conversationBridge/bridgedParticipants/bridgedparticipant@example.com?" + EndpointId;
        public  const string TerminateConversationConference = TgtConversation + "/onlineMeeting/terminateOnlineMeeting?" + EndpointId;

        // Participant urls
        public const string EjectParticipant = TgtConversation + "/participants/participant@example.com/eject?" + EndpointId;
    }
}
