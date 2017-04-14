using System;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    public enum AudioVideoFlowCapability
    {
        PlayPrompt = 0
    }

    public enum PromptCapability
    {
    }

    public enum AudioVideoCallCapability
    {
        Establish = 0,
        Transfer = 1,
        Terminate = 2
    }

    public enum MessagingCallCapability
    {
        Establish = 0,
        SendMessage = 1,
        Terminate = 2
    }

    public enum TransferCapability
    {
    }

    public enum ConversationCapability
    {
        AddParticipant = 0
    }

    public enum ConversationConferenceCapability
    {
        Terminate = 0
    }

    public enum ConversationBridgeCapability
    {
        AddBridgedParticipant = 0
    }

    internal enum ParticipantsCapability
    {
    }

    public enum BridgedParticipantsCapability
    {
    }

    public enum BridgedParticipantCapability
    {
    }

    public enum ParticipantCapability
    {
        Eject = 0
    }

    public enum ParticipantMessagingCapability
    {
    }

    public enum ApplicationCapability
    {
        [Obsolete("Use GetAnonApplicationTokenForMeeting instead")]
        GetAnonApplicationToken = 0,
        [Obsolete("Use CreateAdhocMeeting instead")]
        GetAdhocMeetingResource = 1,
        GetAnonApplicationTokenForMeeting = 2,
        GetAnonApplicationTokenForP2PCall = 3,
        CreateAdhocMeeting = 4
    }

    public enum AnonymousApplicationTokenCapability
    {
    }

    /// <summary>
    /// Capabilities exposed by <see cref="IAdhocMeeting"/>
    /// </summary>
    public enum AdhocMeetingCapability
    {
        JoinAdhocMeeting = 0
    }

    public enum CommunicationCapability
    {
        StartMessaging = 0,
        StartMessagingWithIdentity = 1,
        StartAudioVideo = 2,
        StartAudio = 3
    }

    public enum MessagingInvitationCapability
    {
        StartAdhocMeeting = 0,
        AcceptAndBridge = 1
    }

    public enum AudioVideoInvitationCapability
    {
        Accept = 0,
        Forward = 1,
        Decline = 2,
        StartAdhocMeeting = 3,
        AcceptAndBridge = 4
    }

    public enum OnlineMeetingInvitationCapability
    {
    }

    public enum ParticipantInvitationCapability
    {
    }

    public enum ApplicationsCapability
    {
    }

    public enum DiscoverCapability
    {
    }
}
