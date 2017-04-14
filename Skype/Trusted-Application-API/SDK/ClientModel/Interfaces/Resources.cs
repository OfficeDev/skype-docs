using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    #region public interface IPlatformResource

    /// <summary>
    /// Represents a Resource returned by PlatformService.
    /// </summary>
    /// <typeparam name="TCapabilities">
    /// An enum listing all the capabilties that this <see cref="IPlatformResource{TCapabilities}"/> supports. Capabilities
    /// might not be available at runtime, such cases can be handled by invoking <see cref="Supports(TCapabilities)"/> when a capabilty
    /// needs to be used.
    /// </typeparam>
    public interface IPlatformResource<TCapabilities>
    {
        /// <summary>
        /// Base <see cref="Uri"/> of this Resource. It is defined as the <see cref="string"/> concatenation of
        /// <see cref="ResourceUri"/>'s <see cref="Uri.Scheme"/> and <see cref="Uri.Authority"/>.
        /// </summary>
        /// <remarks>
        /// Most of the <see cref="IPlatformResource{TCapabilities}"/> will have same value of this property.
        /// </remarks>
        /// <example>https://platformservice.example.com/</example>
        Uri BaseUri { get; }

        /// <summary>
        /// HTTP <see cref="Uri"/> of this Resource.
        /// </summary>
        /// <remarks>
        /// Each Resource has a unique <see cref="Uri"/>, which can never change once a Resource is created.
        /// </remarks>
        Uri ResourceUri { get; }

        /// <summary>
        /// Resources returned by PlatformService are in a hierarchy. This property returns the parent of current Resource.
        /// </summary>
        object Parent { get; }

        /// <summary>
        /// <see cref="EventHandler"/> that will be invoked when this Resource gets updated.
        /// </summary>
        EventHandler<PlatformResourceEventArgs> HandleResourceUpdated { get; set; }

        /// <summary>
        /// <see cref="EventHandler"/> that will be invoked when this Resource receives a Completed event.
        /// </summary>
        EventHandler<PlatformResourceEventArgs> HandleResourceCompleted { get; set; }

        /// <summary>
        /// <see cref="EventHandler"/> that will be invoked when this Resource gets removed/deleted.
        /// </summary>
        EventHandler<PlatformResourceEventArgs> HandleResourceRemoved { get; set; }

        /// <summary>
        /// Fetches this Resource from PlatformService and updates it.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="void"/> it is an async method.</returns>
        /// <remarks><see cref="HandleResourceUpdated"/> <see cref="EventHandler"/> will be invoked when the refresh succeeds.</remarks>
        Task RefreshAsync(LoggingContext loggingContext = null);

        /// <summary>
        /// Deletes this Resource from PlatformService.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="void"/> it is an async method.</returns>
        /// <remarks><see cref="HandleResourceRemoved"/> <see cref="EventHandler"/> will be invoked when the delete succeeds.</remarks>
        Task DeleteAsync(LoggingContext loggingContext = null);

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available at the time of invoking.</returns>
        /// <remarks>
        /// Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="CapabilityNotAvailableException"/>
        /// when you are using a capability.
        /// </remarks>
        bool Supports(TCapabilities capability);
    }

    #endregion

    #region public interface IApplication

    /// <summary>
    /// Represents your real-time communication application.
    /// </summary>
    /// <remarks>
    /// This resource represents an application which is similar to a bot in functionality and is not bound to any user.
    /// </remarks>
    public interface IApplication : IPlatformResource<ApplicationCapability>
    {
        /// <summary>
        /// <see cref="ICommunication"/> resource of this <see cref="IApplication"/> that can be used to initiate calls and conversations.
        /// </summary>
        ICommunication Communication { get; }

        /// <summary>
        /// Refreshes this <see cref="IApplication"/> and populates all encapsulated Resources.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="void"/> it is an async method.</returns>
        Task RefreshAndInitializeAsync(LoggingContext loggingContext = null);

        /// <summary>
        /// Gets an anonymous application token for a meeting. This token can be given to a user domain application. Using this token,
        /// the user can sign in and join the meeting.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="input">Specifies properties for the required token.</param>
        /// <returns>A token that can be used by a user to join the specified meeting.</returns>
        [Obsolete("Please use GetAnonApplicationTokenForMeetingAsync instead")]
        Task<AnonymousApplicationTokenResource> GetAnonApplicationTokenAsync(LoggingContext loggingContext, AnonymousApplicationTokenInput input);

        /// <summary>
        /// Gets an anonymous application token for a meeting. This token can be given to a user domain application. Using this token,
        /// the user can sign in and join the meeting.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="meetingUrl">HTTP join url of the meeting</param>
        /// <param name="allowedOrigins">List of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <returns>A token that can be used by a user to join the specified meeting.</returns>
        [Obsolete("Please use the other variation")]
        Task<IAnonymousApplicationToken> GetAnonApplicationTokenForMeetingAsync(LoggingContext loggingContext, string meetingUrl, string allowedOrigins, string applicationSessionId);

        /// <summary>
        /// Gets an anonymous application token for a meeting. This token can be given to a user domain application. Using this token,
        /// the user can sign in and join the meeting.
        /// </summary>
        /// <param name="meetingUrl">HTTP join url of the meeting</param>
        /// <param name="allowedOrigins">List of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns>A token that can be used by a user to join the specified meeting.</returns>
        Task<IAnonymousApplicationToken> GetAnonApplicationTokenForMeetingAsync(string meetingUrl, string allowedOrigins, string applicationSessionId, LoggingContext loggingContext = null);

        /// <summary>
        /// Gets an anonymous application token for a P2P call. This token can be given to a user domain application. Using this token,
        /// the user can make P2P calls.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="allowedOrigins">List of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <returns>A token that can be used by a user to make P2P calls</returns>
        [Obsolete("Please use the other variation")]
        Task<IAnonymousApplicationToken> GetAnonApplicationTokenForP2PCallAsync(LoggingContext loggingContext, string allowedOrigins, string applicationSessionId);

        /// <summary>
        /// Gets an anonymous application token for a P2P call. This token can be given to a user domain application. Using this token,
        /// the user can make P2P calls.
        /// </summary>
        /// <param name="allowedOrigins">List of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns>A token that can be used by a user to make P2P calls</returns>
        Task<IAnonymousApplicationToken> GetAnonApplicationTokenForP2PCallAsync(string allowedOrigins, string applicationSessionId, LoggingContext loggingContext = null);

        /// <summary>
        /// Creates an adhoc meeting
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="input">Specifies properties for the meeting to be created</param>
        /// <returns>An adhoc meeting</returns>
        [Obsolete("Please use CreateAdhocMeetingAsync instead")]
        Task<AdhocMeetingResource> GetAdhocMeetingResourceAsync(LoggingContext loggingContext, AdhocMeetingInput input);

        /// <summary>
        /// Creates an adhoc meeting
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="input">Specifies properties for the meeting to be created</param>
        /// <returns><see cref="IAdhocMeeting"/> which can be used to join the meeting or get meeting url, which can be passed onto real users to join it.</returns>
        [Obsolete("Please use the other variation")]
        Task<IAdhocMeeting> CreateAdhocMeetingAsync(LoggingContext loggingContext, AdhocMeetingCreationInput input);

        /// <summary>
        /// Creates an adhoc meeting
        /// </summary>
        /// <param name="input">Specifies properties for the meeting to be created</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="IAdhocMeeting"/> which can be used to join the meeting or get meeting url, which can be passed onto real users to join it.</returns>
        Task<IAdhocMeeting> CreateAdhocMeetingAsync(AdhocMeetingCreationInput input, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IAnonymousApplicationToken

    /// <summary>
    /// Represents a token which can be used by a real user to join a meeting or make P2P calls
    /// </summary>
    public interface IAnonymousApplicationToken
    {
        /// <summary>
        /// The underlying authorization token
        /// </summary>
        string AuthToken { get; }

        /// <summary>
        /// Expiry time of <see cref="AuthToken"/>
        /// </summary>
        DateTime AuthTokenExpiryTime { get; }

        /// <summary>
        /// Uri that can be used to discover SfB services required to join the meeting/make P2P call
        /// </summary>
        Uri AnonymousApplicationsDiscoverUri { get; }
    }

    #endregion

    #region public interface IAdhocMeeting

    /// <summary>
    /// Represents a meeting
    /// </summary>
    public interface IAdhocMeeting : IPlatformResource<AdhocMeetingCapability>
    {
        /// <summary>
        /// A HTTP url which can be given to users to join this meeting via Lync Web App
        /// </summary>
        string JoinUrl { get; }

        /// <summary>
        /// SIP uri of the meeting
        /// </summary>
        string OnlineMeetingUri { get; }

        /// <summary>
        /// Subject specified when the meeting was created
        /// </summary>
        string Subject { get; }

        /// <summary>
        /// Joins the adhoc meeting
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation"/> which can be used to wait for the meeting join to complete</returns>
        [Obsolete("Please use ICommunication.JoinAdhocMeetingAsync instead")]
        Task<IOnlineMeetingInvitation> JoinAdhocMeeting(LoggingContext loggingContext, string callbackContext);
    }

    #endregion

    #region public interface IApplicationEndpoint

    /// <summary>
    /// Interface for an application endpoint
    /// </summary>
    public interface IApplicationEndpoint
    {
        /// <summary>
        /// Handles incoming instant messaging call
        /// </summary>
        event EventHandler<IncomingInviteEventArgs<IMessagingInvitation>> HandleIncomingInstantMessagingCall;

        /// <summary>
        /// Handles incoming Audio Video call
        /// </summary>
        event EventHandler<IncomingInviteEventArgs<IAudioVideoInvitation>> HandleIncomingAudioVideoCall;

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        IApplication Application { get; }
        /// <summary>
        /// Gets the application endpoint identifier.
        /// </summary>
        /// <value>The application endpoint identifier.</value>
        Uri ApplicationEndpointId { get; }
        /// <summary>
        /// Gets the client platform.
        /// </summary>
        /// <value>The client platform.</value>
        IClientPlatform ClientPlatform { get; }

        /// <summary>
        /// Initializes the application endpoint.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task InitializeAsync(LoggingContext loggingContext = null);

        /// <summary>
        /// Initializes the application.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task InitializeApplicationAsync(LoggingContext loggingContext = null);
        /// <summary>
        /// Uninitializes the application endpoint.
        /// </summary>
        void Uninitialize();
    }

    #endregion

    #region public interface IApplications

    /// <summary>
    /// Interface for applications
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    [Obsolete("Please use IApplication instead")]
    // TODO : Make this interface internal when releasing publicly
    public interface IApplications : IPlatformResource<ApplicationsCapability>
    {
        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        IApplication Application { get; }

        /// <summary>
        /// Refreshes  and initializes the application.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task RefreshAndInitializeAsync(LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IAudioVideoCall

    /// <summary>
    /// Interface for audio video call
    /// </summary>
    /// <seealso cref="ICall{TInvitation}"/>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IAudioVideoCall : ICall<IAudioVideoInvitation>, IPlatformResource<AudioVideoCallCapability>
    {
        /// <summary>
        /// The event when audio video flow is connected.
        /// </summary>
        event EventHandler<AudioVideoFlowUpdatedEventArgs> AudioVideoFlowConnected;
        /// <summary>
        /// Gets the call context.
        /// </summary>
        /// <value>The call context.</value>
        string CallContext { get; }

        /// <summary>
        /// Gets the audio video flow.
        /// </summary>
        /// <value>The audio video flow.</value>
        IAudioVideoFlow AudioVideoFlow { get; }

        /// <summary>
        /// Transfers the audio video call.
        /// </summary>
        /// <param name="transferTarget">SIP uri of the user where the call needs to be transferred to</param>
        /// <param name="replacesCallContext"><see cref="CallContext"/> of the <see cref="IAudioVideoCall"/> which you are trying to transfer to</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="ITransfer"/> which can be used to track the transfer operation</returns>
        /// <remarks>only one of <paramref name="transferTarget"/> or <paramref name="replacesCallContext"/> is supported at a time</remarks>
        [Obsolete("Please use any of the other variations")]
        Task<ITransfer> TransferAsync(string transferTarget, string replacesCallContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Transfers the audio video call.
        /// </summary>
        /// <param name="transferTarget">SIP uri of the user where the call needs to be transferred to</param>
        /// <param name="replacesCallContext"><see cref="CallContext"/> of the <see cref="IAudioVideoCall"/> which you are trying to replace</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="ITransfer"/> which can be used to track the transfer operation</returns>
        /// <remarks>only one of <paramref name="transferTarget"/> or <paramref name="replacesCallContext"/> is supported at a time</remarks>
        [Obsolete("Please use any of the other variations")]
        Task<ITransfer> TransferAsync(SipUri transferTarget, string replacesCallContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Transfers the audio video call to a user
        /// </summary>
        /// <param name="transferTarget">SIP uri of the user where the call needs to be transferred to</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="ITransfer"/> which can be used to track the transfer operation</returns>
        Task<ITransfer> TransferAsync(SipUri transferTarget, LoggingContext loggingContext = null);

        /// <summary>
        /// Transfers the audio video call by replacing an existing audio video call
        /// </summary>
        /// <param name="replacesCallContext"><see cref="CallContext"/> of the <see cref="IAudioVideoCall"/> which you are trying to transfer to</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="ITransfer"/> which can be used to track the transfer operation</returns>
        Task<ITransfer> TransferAsync(string replacesCallContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Waits for the audio video flow to be connected.
        /// </summary>
        /// <param name="timeoutInSeconds">The timeout in seconds.</param>
        /// <returns>Task&lt;IAudioVideoFlow&gt;.</returns>
        Task<IAudioVideoFlow> WaitForAVFlowConnected(int timeoutInSeconds = 30);
    }

    #endregion

    #region public interface IAudioVideoFlow

    /// <summary>
    /// Interface for the audio video flow
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IAudioVideoFlow : IPlatformResource<AudioVideoFlowCapability>
    {
        /// <summary>
        /// Occurs when tone is received.
        /// </summary>
        event EventHandler<ToneReceivedEventArgs> ToneReceivedEvent;

        /// <summary>
        /// Gets the state of the audio video flow.
        /// </summary>
        /// <value>The state.</value>
        FlowState State { get; }

        /// <summary>
        /// Plays the prompt.
        /// </summary>
        /// <param name="promptUri">The prompt URI.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;IPrompt&gt;.</returns>
        Task<IPrompt> PlayPromptAsync(Uri promptUri, LoggingContext loggingContext = null);

        /// <summary>
        /// Send stop prompts event and wait for all prompts to complete.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        Task StopPromptsAsync(LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IAudioVideoInvitation

    /// <summary>
    /// Interface for audio video invitation
    /// </summary>
    /// <seealso cref="IInvitation" />
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IAudioVideoInvitation : IInvitation, IPlatformResource<AudioVideoInvitationCapability>
    {
        /// <summary>
        /// Accepts the audio video invitation.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> AcceptAsync(LoggingContext loggingContext = null);

        /// <summary>
        /// Forwards the audio video invitation.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="forwardTarget">The forward target.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        [Obsolete("Please use the other variation")]
        Task<HttpResponseMessage> ForwardAsync(LoggingContext loggingContext, string forwardTarget);

        /// <summary>
        /// Forwards the audio video invitation.
        /// </summary>
        /// <param name="forwardTarget">The forward target.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> ForwardAsync(SipUri forwardTarget, LoggingContext loggingContext = null);

        /// <summary>
        /// Declines the audio video invitation.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        Task<HttpResponseMessage> DeclineAsync(LoggingContext loggingContext = null);
        /// <summary>
        /// Accepts and bridges the audio video invitation.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="meetingUri">The meeting URI.</param>
        /// <param name="to">To.</param>
        /// <returns>Task.</returns>
        [Obsolete("Please use any other variation")]
        Task AcceptAndBridgeAsync(LoggingContext loggingContext, string meetingUri, string to);

        /// <summary>
        /// Accepts and bridges the audio video invitation.
        /// </summary>
        /// <param name="meetingUri">The meeting URI.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task AcceptAndBridgeAsync(string meetingUri, LoggingContext loggingContext = null);

        /// <summary>
        /// Accepts and bridges the audio video invitation.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task AcceptAndBridgeAsync(SipUri to, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts adhoc meeting.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="callbackContext">The callback context.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;IOnlineMeetingInvitation&gt;.</returns>
        [Obsolete("Please use ICommunication.StartAdhocMeetingAsync instead")]
        Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(string subject, string callbackContext, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IBridgedParticipant

    /// <summary>
    /// Interface for bridged participant
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IBridgedParticipant : IPlatformResource<BridgedParticipantCapability>
    {
        /// <summary>
        /// Updates the bridged participant.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="isEnableFilter">if set to <c>true</c> [is enable filter].</param>
        /// <returns>Task.</returns>
        [Obsolete("Please use the other variation")]
        Task UpdateAsync(LoggingContext loggingContext, string displayName, bool isEnableFilter);

        /// <summary>
        /// Updates the bridged participant.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="isEnableFilter">if set to <c>true</c> [is enable filter].</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(string displayName, bool isEnableFilter, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IBridgedParticipants

    /// <summary>
    /// Interface for bridged participants
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IBridgedParticipants : IPlatformResource<BridgedParticipantsCapability>
    {
    }

    #endregion

    #region public interface ICall    
    /// <summary>
    /// Interface for call
    /// </summary>
    /// <typeparam name="TInvitation">The type of the t invitation.</typeparam>
    public interface ICall<TInvitation>
    {
        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        CallState State { get; }
        /// <summary>
        /// Occurs when the call state is changed.
        /// </summary>
        event EventHandler<CallStateChangedEventArgs> CallStateChanged;
        /// <summary>
        /// Establishes the call.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;TInvitation&gt;.</returns>
        Task<TInvitation> EstablishAsync(LoggingContext loggingContext = null);
        /// <summary>
        /// Terminates the call.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task TerminateAsync(LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IClientPlatform

    /// <summary>
    /// Interface for the client platform
    /// </summary>
    public interface IClientPlatform
    {
        /// <summary>
        /// Gets the discover URI.
        /// </summary>
        /// <value>The discover URI.</value>
        Uri DiscoverUri { get; }

        /// <summary>
        /// Gets the aad client Id.
        /// </summary>
        /// <value>The aad client Id.</value>
        Guid AADClientId { get; }
        /// <summary>
        /// Gets the aad client secret.
        /// </summary>
        /// <value>The aad client secret.</value>
        string AADClientSecret { get; }
        /// <summary>
        /// Gets the aad application certificate.
        /// </summary>
        /// <value>The aad application certificate.</value>
        X509Certificate2 AADAppCertificate { get; }
    }

    #endregion

    #region public interface ICommunication

    /// <summary>
    /// Interface for communication
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface ICommunication : IPlatformResource<CommunicationCapability>
    {
        /// <summary>
        /// Starts messaging with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="IMessagingInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("Please use the other StartMessagingAsync")]
        Task<IMessagingInvitation> StartMessagingAsync(string subject, string to, string callbackUrl, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts messaging with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackContext">An opaque blob which will be provided in all related events so that your application can relate them to this operation</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="IMessagingInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        Task<IMessagingInvitation> StartMessagingAsync(string subject, SipUri to, string callbackContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts messaging with a user; user will see the message as originating from the specified user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="localUserDisplayName">Name to be used as the originating user's identity</param>
        /// <param name="localUserUri">SIP uri to be used as the originating user's identity</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <returns><see cref="IMessagingInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("This feature is not supported by SfB server for public applications")]
        Task<IMessagingInvitation> StartMessagingWithIdentityAsync(string subject, string to, string callbackUrl, string localUserDisplayName, string localUserUri, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts an audio video call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("Please use the other StartAudioVideoAsync")]
        Task<IAudioVideoInvitation> StartAudioVideoAsync(string subject, string to, string callbackUrl, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts an audio video call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackContext">An opaque blob which will be provided in all related events so that your application can relate them to this operation</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        Task<IAudioVideoInvitation> StartAudioVideoAsync(string subject, SipUri to, string callbackContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts an audio call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackUrl">Uri where all related events need to be posted</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        [Obsolete("Please use the other StartAudioAsync")]
        Task<IAudioVideoInvitation> StartAudioAsync(string subject, string to, string callbackUrl, LoggingContext loggingContext = null);

        /// <summary>
        /// Starts an audio call with a user
        /// </summary>
        /// <param name="subject">Subject of the conversation</param>
        /// <param name="to">SIP uri of the user to connect to</param>
        /// <param name="callbackContext">An opaque blob which will be provided in all related events so that your application can relate them to this operation</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns><see cref="IAudioVideoInvitation"/> which can be used to wait for user to accept/decline the invitation</returns>
        Task<IAudioVideoInvitation> StartAudioAsync(string subject, SipUri to, string callbackContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Checks whether the application can join a specific meeting or not
        /// </summary>
        /// <param name="adhocMeeting">Meeting to be checked</param>
        /// <returns><code>true</code> if and only if the application is capable of joining the meeting</returns>
        bool CanJoinAdhocMeeting(IAdhocMeeting adhocMeeting);

        /// <summary>
        /// Checks whether the application can create and join a meeting for the invitation or not
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be started</param>
        /// <returns><code>true</code> if and only if the application is capable of joining the meeting</returns>
        bool CanStartAdhocMeeting(IMessagingInvitation invitation);

        /// <summary>
        /// Checks whether the application can create and join a meeting for the invitation or not
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be started</param>
        /// <returns><code>true</code> if and only if the application is capable of joining the meeting</returns>
        bool CanStartAdhocMeeting(IAudioVideoInvitation invitation);

        /// <summary>
        /// Adds the application to the meeting.
        /// </summary>
        /// <param name="adhocMeeting">Meeting to be joined</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation"/> which can be used to wait for the meeting join to complete</returns>
        Task<IOnlineMeetingInvitation> JoinAdhocMeetingAsync(IAdhocMeeting adhocMeeting, string callbackContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Schedules and joins an adhoc meeting for the invitation
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be created</param>
        /// <param name="subject">Subject of the meeting</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation"/> which can be used to wait for the meeting join to complete</returns>
        Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(IMessagingInvitation invitation, string subject, string callbackContext, LoggingContext loggingContext = null);

        /// <summary>
        /// Schedules and joins an adhoc meeting for the invitation
        /// </summary>
        /// <param name="invitation">Invitation for which the meeting needs to be created</param>
        /// <param name="subject">Subject of the meeting</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation"/> which can be used to wait for the meeting join to complete</returns>
        Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(IAudioVideoInvitation invitation, string subject, string callbackContext, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IConversation

    /// <summary>
    /// Interface for conversation
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IConversation : IPlatformResource<ConversationCapability>
    {
        /// <summary>
        /// Gets the list of active modalities for this Conversation.
        /// </summary>
        IReadOnlyCollection<ConversationModalityType> ActiveModalities { get; }

        /// <summary>
        /// State of Conversation
        /// </summary>
        ConversationState State { get; }

        /// <summary>
        /// Get The messaging call
        /// </summary>
        IMessagingCall MessagingCall { get; }

        /// <summary>
        /// Gets the audio video call.
        /// </summary>
        /// <value>The audio video call.</value>
        IAudioVideoCall AudioVideoCall { get; }

        /// <summary>
        /// Gets the conversation bridge.
        /// </summary>
        /// <value>The conversation bridge.</value>
        IConversationBridge ConversationBridge { get; }
        /// <summary>
        /// Gets the conversation conference.
        /// </summary>
        /// <value>The conversation conference.</value>
        IConversationConference ConversationConference { get; }

        /// <summary>
        /// Get the participants
        /// </summary>
        List<IParticipant> Participants { get; }

        /// <summary>
        /// Occurs when participant in the conversation is changed.
        /// </summary>
        event EventHandler<ParticipantChangeEventArgs> HandleParticipantChange;

        /// <summary>
        /// Occurs when conversation state is changed.
        /// </summary>
        event EventHandler<ConversationStateChangedEventArgs> ConversationStateChanged;

        /// <summary>
        /// Tries to get participant.
        /// </summary>
        /// <param name="href">The href.</param>
        /// <returns>IParticipant.</returns>
        IParticipant TryGetParticipant(string href);

        /// <summary>
        /// Adds participant.
        /// </summary>
        /// <param name="targetSip">The target sip.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;IParticipantInvitation&gt;.</returns>
        [Obsolete("Please use the other variation")]
        Task<IParticipantInvitation> AddParticipantAsync(string targetSip, LoggingContext loggingContext = null);

        /// <summary>
        /// Adds participant.
        /// </summary>
        /// <param name="targetSip">The target sip.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;IParticipantInvitation&gt;.</returns>
        Task<IParticipantInvitation> AddParticipantAsync(SipUri targetSip, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IConversationBridge

    /// <summary>
    /// Interface for conversation bridge
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IConversationBridge : IPlatformResource<ConversationBridgeCapability>
    {
        /// <summary>
        /// Gets the bridged participants.
        /// </summary>
        /// <value>The bridged participants.</value>
        List<IBridgedParticipant> BridgedParticipants { get; }
        /// <summary>
        /// Adds the bridged participant .
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="sipUri">The sip URI.</param>
        /// <param name="enableMessageFilter">if set to <c>true</c> [enable message filter].</param>
        /// <returns>Task&lt;IBridgedParticipant&gt;.</returns>
        [Obsolete("Please use the other variation")]
        Task<IBridgedParticipant> AddBridgedParticipantAsync(LoggingContext loggingContext, string displayName, string sipUri, bool enableMessageFilter);
        /// <summary>
        /// Adds the bridged participant.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="sipUri">The sip URI.</param>
        /// <param name="enableMessageFilter">if set to <c>true</c> [enable message filter].</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;IBridgedParticipant&gt;.</returns>
        Task<IBridgedParticipant> AddBridgedParticipantAsync(string displayName, SipUri sipUri, bool enableMessageFilter, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IConversationConference

    /// <summary>
    /// Interface for conversation conference
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IConversationConference : IPlatformResource<ConversationConferenceCapability>
    {
        /// <summary>
        /// Gets the online meeting URI.
        /// </summary>
        /// <value>The online meeting URI.</value>
        string OnlineMeetingUri { get; }

        /// <summary>
        /// Terminates the conversation conference.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task TerminateAsync(LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IDiscover

    /// <summary>
    /// Interface for discover
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IDiscover : IPlatformResource<DiscoverCapability>
    {
        /// <summary>
        /// Get Applications
        /// </summary>
        [Obsolete("Please use Application property instead")]
        IApplications Applications { get; }

        /// <summary>
        /// Get Application
        /// </summary>
        IApplication Application { get; }

        /// <summary>
        /// Refreshes and initializes the discover.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="endpointId">The endpoint identifier.</param>
        /// <returns>Task.</returns>
        [Obsolete("Please use the other variation")]
        Task RefreshAndInitializeAsync(LoggingContext loggingContext, string endpointId);

        /// <summary>
        /// Refreshes and initializes the discover.
        /// </summary>
        /// <param name="endpointId">The endpoint identifier.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task RefreshAndInitializeAsync(string endpointId, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IEventChannel

    /// <summary>
    /// Interface for event channel
    /// </summary>
    public interface IEventChannel
    {
        /// <summary>
        /// Event handler for incoming events
        /// </summary>
        event EventHandler<EventsChannelArgs> HandleIncomingEvents;

        /// <summary>
        /// Start Event Channel
        /// </summary>
        /// <returns></returns>
        Task TryStartAsync();

        /// <summary>
        /// Stop Event Channel
        /// </summary>
        /// <returns></returns>
        Task TryStopAsync();
    }

    #endregion

    #region public interface IInvitation

    /// <summary>
    /// Interface for an invitation
    /// </summary>
    public interface IInvitation
    {
        /// <summary>
        /// Wait for invite complete
        /// </summary>
        /// <returns></returns>
        Task<IConversation> WaitForInviteCompleteAsync();

        /// <summary>
        /// The related conversation
        /// </summary>
        IConversation RelatedConversation { get; }
    }

    #endregion

    #region public interface IMessagingInvitation

    /// <summary>
    /// Interface for messaging invitation
    /// </summary>
    /// <seealso cref="IInvitation" />
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IMessagingInvitation : IInvitation, IPlatformResource<MessagingInvitationCapability>
    {
        /// <summary>
        /// Starts the adhoc meeting.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="callbackUrl">The callback URL.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task&lt;IOnlineMeetingInvitation&gt;.</returns>
        [Obsolete("Please use ICommunication.StartAdhocMeetingAsync instead")]
        Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(string subject, string callbackUrl, LoggingContext loggingContext = null);

        /// <summary>
        /// Accepts and bridges the messaging invitation
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="meetingUrl">The meeting URL.</param>
        /// <param name="displayName">The display name.</param>
        /// <returns>Task.</returns>
        [Obsolete("Please use the other variation")]
        Task AcceptAndBridgeAsync(LoggingContext loggingContext, string meetingUrl, string displayName);

        /// <summary>
        /// Accepts and bridges the messaging invitation
        /// </summary>
        /// <param name="meetingUrl">The meeting URL.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <returns>Task.</returns>
        Task AcceptAndBridgeAsync(string meetingUrl, string displayName, LoggingContext loggingContext = null);
    }

    #endregion

    #region public interface IMessagingCall

    /// <summary>
    /// Interface for a messaging call
    /// </summary>
    /// <seealso cref="ICall{TInvitation}"/>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IMessagingCall : ICall<IMessagingInvitation>, IPlatformResource<MessagingCallCapability>
    {
        /// <summary>
        /// Handles the event when incoming message is received
        /// </summary>
        /// <value>The incoming message received.</value>
        EventHandler<IncomingMessageEventArgs> IncomingMessageReceived { get; set; }
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>Task.</returns>
        Task SendMessageAsync(string message, LoggingContext loggingContext = null, string contentType = Constants.TextPlainContentType);
    }

    #endregion

    #region public interface IOnlineMeetingInvitation

    /// <summary>
    /// Interface for online meeting invitation
    /// </summary>
    /// <seealso cref="IInvitation" />
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IOnlineMeetingInvitation : IInvitation, IPlatformResource<OnlineMeetingInvitationCapability>
    {
        /// <summary>
        /// Anonymous meeting url
        /// </summary>
        string MeetingUrl { get; }
    }

    #endregion

    #region public interface IParticipant

    /// <summary>
    /// Interface for participant
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IParticipant : IPlatformResource<ParticipantCapability>
    {
        /// <summary>
        /// Get the uri of participant
        /// </summary>
        string Uri { get; }

        /// <summary>
        /// Get the name of participant
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The participant messaging resource
        /// </summary>
        IParticipantMessaging ParticipantMessaging { get; }

        /// <summary>
        /// Ejects the participant from the corresponding meeting.
        /// </summary>
        /// <param name="loggingContext"></param>
        Task EjectAsync(LoggingContext loggingContext = null);

        /// <summary>
        /// Occurs when participant modality is changed.
        /// </summary>
        event EventHandler<ParticipantModalityChangeEventArgs> HandleParticipantModalityChange;
    }

    #endregion

    #region public interface IParticipantInvitation

    /// <summary>
    /// Interface for participant invitation
    /// </summary>
    /// <seealso cref="IInvitation" />
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IParticipantInvitation : IInvitation, IPlatformResource<ParticipantInvitationCapability>
    {
    }

    #endregion

    #region public interface IParticipantMessaging

    /// <summary>
    /// Interface for participant messaging
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IParticipantMessaging : IPlatformResource<ParticipantMessagingCapability>
    {
    }

    #endregion

    #region public interface IPrompt

    /// <summary>
    /// Interface for prompt
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface IPrompt : IPlatformResource<PromptCapability>
    {
    }

    #endregion

    #region public interface ITransfer

    /// <summary>
    /// Interface for transfer
    /// </summary>
    /// <seealso cref="IPlatformResource{TCapabilities}"/>
    public interface ITransfer : IPlatformResource<TransferCapability>
    {
        /// <summary>
        /// Waits for transfer to complete.
        /// </summary>
        /// <returns>Task.</returns>
        Task WaitForTransferCompleteAsync();
    }

    #endregion
}
