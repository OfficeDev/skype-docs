using System;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Capabilities of an <see cref="AudioVideoFlow"/>, exposed by <see cref="IAudioVideoFlow"/>
    /// </summary>
    public enum AudioVideoFlowCapability
    {
        /// <summary>
        /// Play prompt in an <see cref="IAudioVideoFlow"/>
        /// </summary>
        PlayPrompt = 0,

        /// <summary>
        /// Stop all prompts playing in an <see cref="IAudioVideoFlow"/>
        /// </summary>
        StopPrompts = 1
    }

    /// <summary>
    /// Capabilities of a <see cref="Prompt"/>, exposed by <see cref="IPrompt"/>
    /// </summary>
    public enum PromptCapability
    {
    }

    /// <summary>
    /// Capabilities of an <see cref="AudioVideoCall"/>, exposed by <see cref="IAudioVideoCall"/>
    /// </summary>
    public enum AudioVideoCallCapability
    {
        /// <summary>
        /// Establish an <see cref="IAudioVideoCall"/> in an existing <see cref="IConversation"/>
        /// </summary>
        Establish = 0,

        /// <summary>
        /// Transfer the <see cref="IAudioVideoCall"/> to some other user
        /// </summary>
        Transfer = 1,

        /// <summary>
        /// Terminate an established <see cref="IAudioVideoCall"/>
        /// </summary>
        Terminate = 2
    }

    /// <summary>
    /// Capabilities of a <see cref="MessagingCall"/>, exposed by <see cref="IMessagingCall"/>
    /// </summary>
    public enum MessagingCallCapability
    {
        /// <summary>
        /// Establish an <see cref="IMessagingCall"/> in an existing <see cref="IConversation"/>
        /// </summary>
        Establish = 0,

        /// <summary>
        /// Send an Instant Message to an established <see cref="IMessagingCall"/>
        /// </summary>
        SendMessage = 1,

        /// <summary>
        /// Terminate an established <see cref="IMessagingCall"/>
        /// </summary>
        Terminate = 2
    }

    /// <summary>
    /// Capabilities of a <see cref="Transfer"/>, exposed by <see cref="ITransfer"/>
    /// </summary>
    public enum TransferCapability
    {
    }

    /// <summary>
    /// Capabilities of a <see cref="Conversation"/>, exposed by <see cref="IConversation"/>
    /// </summary>
    public enum ConversationCapability
    {
        /// <summary>
        /// Add a new participant to an ongoing <see cref="IConversation"/>
        /// </summary>
        AddParticipant = 0
    }

    /// <summary>
    /// Capabilities of a <see cref="ConversationConference"/>, exposed by <see cref="IConversationConference"/>
    /// </summary>
    public enum ConversationConferenceCapability
    {
        /// <summary>
        /// Terminate the <see cref="IConversationConference"/>
        /// </summary>
        Terminate = 0
    }

    /// <summary>
    /// Capabilities of a <see cref="ConversationBridge"/>, exposed by <see cref="IConversationBridge"/>
    /// </summary>
    public enum ConversationBridgeCapability
    {
        /// <summary>
        /// Add a bridged participant to <see cref="IConversationBridge"/>
        /// </summary>
        AddBridgedParticipant = 0
    }

    /// <summary>
    /// Capabilities of <see cref="Participant"/>, exposed by <see cref="IParticipant"/>
    /// </summary>
    internal enum ParticipantsCapability
    {
    }

    /// <summary>
    /// Capabilities of <see cref="BridgedParticipants"/>, exposed by <see cref="IBridgedParticipants"/>
    /// </summary>
    public enum BridgedParticipantsCapability
    {
    }

    /// <summary>
    /// Capabilities of a <see cref="BridgedParticipant"/>, exposed by <see cref="IBridgedParticipant"/>
    /// </summary>
    public enum BridgedParticipantCapability
    {
    }

    /// <summary>
    /// Capabilities of a <see cref="Participant"/>, exposed by <see cref="IParticipant"/>
    /// </summary>
    public enum ParticipantCapability
    {
        /// <summary>
        /// Eject/remove a <see cref="IParticipant"/> from the corresponding <see cref="IConversation"/>
        /// </summary>
        Eject = 0
    }

    /// <summary>
    /// Capabilities of a <see cref="ParticipantMessaging"/>, exposed by <see cref="IParticipantMessaging"/>
    /// </summary>
    public enum ParticipantMessagingCapability
    {
    }

    /// <summary>
    /// Capabilities of an <see cref="Application"/>, exposed by <see cref="IApplication"/>
    /// </summary>
    public enum ApplicationCapability
    {
        /// <summary>
        /// Get a token which can be used by anyone to join a meeting anonymously
        /// </summary>
        [Obsolete("Use GetAnonApplicationTokenForMeeting instead")]
        GetAnonApplicationToken = 0,

        /// <summary>
        /// Schdule a meeting
        /// </summary>
        [Obsolete("Use CreateAdhocMeeting instead")]
        GetAdhocMeetingResource = 1,

        /// <summary>
        /// Get a token which can be used by anyone to join a meeting anonymously
        /// </summary>
        GetAnonApplicationTokenForMeeting = 2,

        /// <summary>
        /// Get a token which can be used by anyone to start a P2P call anonymously
        /// </summary>
        GetAnonApplicationTokenForP2PCall = 3,

        /// <summary>
        /// Schdule a meeting
        /// </summary>
        CreateAdhocMeeting = 4
    }

    /// <summary>
    /// Capabilities of an <see cref="AnonymousApplicationToken"/>, exposed by <see cref="IAnonymousApplicationToken"/>
    /// </summary>
    public enum AnonymousApplicationTokenCapability
    {
    }

    /// <summary>
    /// Capabilities of an <see cref="AdhocMeeting"/>, exposed by <see cref="IAdhocMeeting"/>
    /// </summary>
    public enum AdhocMeetingCapability
    {
        /// <summary>
        /// Add the application as a trusted participant in a meeting
        /// </summary>
        /// <remarks>
        /// Trusted participant is invisible to normal users
        /// </remarks>
        [Obsolete("Use ICommunication.CanJoinAdhocMeeting instead")]
        JoinAdhocMeeting = 0
    }

    /// <summary>
    /// Capabilities of a <see cref="Communication"/>, exposed by <see cref="ICommunication"/>
    /// </summary>
    public enum CommunicationCapability
    {
        /// <summary>
        /// Start messaging with a user
        /// </summary>
        StartMessaging = 0,

        /// <summary>
        /// Start messaging with a user; user will see the message as originating from the specified identity
        /// </summary>
        StartMessagingWithIdentity = 1,

        /// <summary>
        /// Start an audio video call with a user
        /// </summary>
        StartAudioVideo = 2,

        /// <summary>
        /// Start an audio call with a user
        /// </summary>
        StartAudio = 3
    }

    /// <summary>
    /// Capabilities of a <see cref="MessagingInvitation"/>, exposed by <see cref="IMessagingInvitation"/>
    /// </summary>
    public enum MessagingInvitationCapability
    {
        /// <summary>
        /// Schedule and join a meeting related to the invitation
        /// </summary>
        [Obsolete("Use ICommunication.CanStartAdhocMeeting instead")]
        StartAdhocMeeting = 0,

        /// <summary>
        /// Accept the incoming invitation and bridge it to a meeting or a user. Bridging means
        /// that the application is sitting in middle of the call and can listen to all the messages.
        /// </summary>
        AcceptAndBridge = 1,
    }

    /// <summary>
    /// Capabilities of a <see cref="AudioVideoInvitation"/>, exposed by <see cref="IAudioVideoInvitation"/>
    /// </summary>
    public enum AudioVideoInvitationCapability
    {
        /// <summary>
        /// Accept the incoming <see cref="IAudioVideoInvitation"/>
        /// </summary>
        Accept = 0,

        /// <summary>
        /// Forward the invitation to some other user/application
        /// </summary>
        Forward = 1,

        /// <summary>
        /// Decline the invitation
        /// </summary>
        Decline = 2,

        /// <summary>
        /// Schedule and join a meeting related to the invitation
        /// </summary>
        [Obsolete("Use ICommunication.CanStartAdhocMeeting instead")]
        StartAdhocMeeting = 3,

        /// <summary>
        /// Accept the incoming invitation and bridge it to a meeting or a user. Bridging means
        /// that the application is sitting in middle of the call and can listen to all the messages.
        /// </summary>
        AcceptAndBridge = 4,
    }

    /// <summary>
    /// Capabilities of a <see cref="OnlineMeetingInvitation"/>, exposed by <see cref="IOnlineMeetingInvitation"/>
    /// </summary>
    public enum OnlineMeetingInvitationCapability
    {
    }

    /// <summary>
    /// Capabilities of a <see cref="ParticipantInvitation"/>, exposed by <see cref="IParticipantInvitation"/>
    /// </summary>
    public enum ParticipantInvitationCapability
    {
    }

    /// <summary>
    /// Capabilities of a <see cref="Applications"/>, exposed by <see cref="IApplications"/>
    /// </summary>
    public enum ApplicationsCapability
    {
    }

    /// <summary>
    /// Capabilities of a <see cref="Discover"/>, exposed by <see cref="IDiscover"/>
    /// </summary>
    public enum DiscoverCapability
    {
    }
}
