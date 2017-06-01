using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// The job types
    /// </summary>
    public enum JobType
    {
        /// <summary>
        /// Default
        /// </summary>
        None,

        /// <summary>
        /// The simple notification
        /// </summary>
        SimpleNotification,

        /// <summary>
        /// NotificationWithTemplate
        /// </summary>
        NotificationWithTemplate,

        /// <summary>
        /// ImBridge demo
        /// </summary>
        GetAnonToken,

        /// <summary>
        /// Handle new incoming message and set up im bridge
        /// </summary>
        InstantMessagingBridge,

        /// <summary>
        /// Handle an incoming AudioVideo call and either forward it to a user or play an IVR prompt
        /// </summary>
        AudioVideoIVR,

        /// <summary>
        /// HuntGroup sample flow
        /// </summary>
        HuntGroup,
     
        /// <summary>
        /// Get adhoc meeting from Application directly
        /// </summary>
        AdhocMeeting
    }

    /// <summary>
    /// Base class for job input from application level
    /// </summary>
    public abstract class PlatformServiceJobInputBase
    {
    }

    /// <summary>
    /// InstantMessagingBridgeJobInput
    /// </summary>
    public class InstantMessagingBridgeJobInput : PlatformServiceJobInputBase
    {
        public bool IsStart { get; set; }
        public string Subject { get; set; }
        public string WelcomeMessage { get; set; }
        public string InviteTargetUri { get; set; }
        public string InvitedTargetDisplayName { get; set; }
    }

    /// <summary>
    /// The input class for huntgroup job
    /// </summary>
    public class HuntGroupJobInput : PlatformServiceJobInputBase
    {
        public string[] InviteTargetUris { get; set; }

        public bool IsStart { get; set; }
    }

    /// <summary>
    /// The simple notify job input
    /// </summary>
    public class SimpleNotifyJobInput : PlatformServiceJobInputBase
    {
        /// <summary>
        /// The notification message to be sent
        /// </summary>
        public string NotificationMessage { get; set; }

        /// <summary>
        /// TargetUri for notification
        /// </summary>
        public string TargetUri { get; set; }
    }

    /// <summary>
    /// Get AnonToken job input
    /// </summary>
    public class GetAnonTokenInput : PlatformServiceJobInputBase
    {
        public string AllowedOrigins { get; set; }
        public string ApplicationSessionId { get; set; }
        public string MeetingUrl { get; set; }
    }

    /// <summary>
    /// We may not need this class for a simple web role model, but if we want to have a queue and need another process to
    /// pick up job from queue, we need this class to indicate which job and what is the job input.
    /// </summary>
    public class PlatformServiceSampleJobConfiguration
    {
        /// <summary>
        /// get or set the job type
        /// </summary>
        public JobType JobType { get; set; }

        /// <summary>
        /// The simple noptify job input,
        /// </summary>
        public SimpleNotifyJobInput SimpleNotifyJobInput { get; set; }

        /// <summary>
        /// The get anon token job input
        /// </summary>
        public GetAnonTokenInput AnonTokenJobInput { get; set; }

        /// <summary>
        /// The im bridge job input
        /// </summary>
        public InstantMessagingBridgeJobInput InstantMessagingBridgeJobInput { get; set; }

        public AudioVideoIVRJobInput AudioVideoIVRJobInput { get; set; }

        public HuntGroupJobInput HuntGroupJobInput { get; set; }

        public GetAdhocMeetingResourceInput GetAdhocMeetingResourceInput { get; set; }

    }

    /// <summary>
    /// Actions which can be taken by an <see cref="AudioVideoIVRJob"/> in response to an incoming call or a tone event.
    /// </summary>
    public enum AudioVideoIVRActions
    {
        /// <summary>
        /// Play a prompt
        /// </summary>
        /// <remarks>NoOp if Prompt is null</remarks>
        PlayPrompt,

        /// <summary>
        /// Transfer the call to a specified user
        /// </summary>
        /// <remarks>Forwards the call if it hasn't been accepted yet</remarks>
        TransferToUser,

        /// <summary>
        /// Replay current prompt
        /// </summary>
        /// <remarks>NoOp if Prompt is null</remarks>
        RepeatPrompt,

        /// <summary>
        /// Set previous menu/prompt as current
        /// </summary>
        /// <remarks>NoOp if we are at the main menu</remarks>
        GoToPreviousPrompt,

        /// <summary>
        /// Terminate the call
        /// </summary>
        /// <remarks>Declines the call if it hasn't been accepted yet</remarks>
        TerminateCall
    }

    /// <summary>
    /// Configuration for the <see cref="AudioVideoIVRJob"/>. This defines the full menu structure, what prompts to play, whom to transfer
    /// and when.
    /// </summary>
    public class AudioVideoIVRJobInput : PlatformServiceJobInputBase
    {
        /// <summary>
        /// Action specified for the current <see cref="AudioVideoIVRJobInput"/>
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public AudioVideoIVRActions Action { get; set; }

        /// <summary>
        /// User to whom the call is to be transferred if <see cref="Action"/> is <see cref="AudioVideoIVRActions.TransferToUser"/>.
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Prompt to play if <see cref="Action"/> is <see cref="AudioVideoIVRActions.PlayPrompt"/>.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Specifies <see cref="SendAudioMessageJobInput"/> for different user inputs/tone events.
        /// </summary>
        public Dictionary<string, AudioVideoIVRJobInput> KeyMap { get; set; }

        /// <summary>
        /// Parent/previous <see cref="SendAudioMessageJobInput"/> of <i>this</i> object.
        /// </summary>
        /// <remarks>
        /// It is not provided by user but computed based on <see cref="KeyMap"/> entries. It is here to simplify switching between
        /// different prompts/menus.
        /// </remarks>
        public AudioVideoIVRJobInput ParentInput { get; set; }

        #region ShouldSerialize methods

        /// <summary>
        /// Whether to serialize <see cref="ParentInput"/> or not.
        /// </summary>
        /// <returns><code>true</code> iff <see cref="ParentInput"/> needs to be serialized.</returns>
        /// <remarks>
        /// This property is never serialized becuase <see cref="ParentInput"/> creates an infinite loop with the parent referencing the
        /// object through the <see cref="KeyMap"/> property.
        /// </remarks>
        public bool ShouldSerializeParentInput()
        {
            return false;
        }

        /// <summary>
        /// Whether to serialize <see cref="User"/> or not.
        /// </summary>
        /// <returns><code>true</code> iff <see cref="User"/> needs to be serialized.</returns>
        public bool ShouldSerializeUser()
        {
            return User != null;
        }

        /// <summary>
        /// Whether to serialize <see cref="KeyMap"/> or not.
        /// </summary>
        /// <returns><code>true</code> iff <see cref="KeyMap"/> needs to be serialized.</returns>
        public bool ShouldSerializeKeyMap()
        {
            return KeyMap != null && KeyMap.Count > 0;
        }

        /// <summary>
        /// Whether to serialize <see cref="Prompt"/> or not.
        /// </summary>
        /// <returns><code>true</code> iff <see cref="Prompt"/> needs to be serialized.</returns>
        public bool ShouldSerializePrompt()
        {
            return Prompt != null;
        }

        #endregion
    }

    public class SendAudioMessageJobInput : PlatformServiceJobInputBase
    {
        public string To { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    /// Get adhocmeeting resource job input
    /// </summary>
    public class GetAdhocMeetingResourceInput : PlatformServiceJobInputBase
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AccessLevel { get; set; }
        public bool EntryExitAnnouncement { get; set; }
        public bool AutomaticLeaderAssignment { get; set; }
        public bool PhoneUserAdmission { get; set; }
        public bool LobbyBypassForPhoneUsers { get; set; }
    }
}