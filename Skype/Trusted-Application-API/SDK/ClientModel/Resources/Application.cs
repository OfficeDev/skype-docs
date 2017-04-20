using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents your real-time communication application.
    /// </summary>
    /// <remarks>
    /// This resource represents an application which is similar to a bot in functionality and is not bound to any user.
    /// </remarks>
    internal class Application : BasePlatformResource<ApplicationResource, ApplicationCapability>, IApplication
    {
        #region Private fields

        private Communication m_communication;

        #endregion

        #region Public properties

        /// <summary>
        /// Communication of <see cref="Application"/>
        /// </summary>
        public ICommunication Communication
        {
            get { return m_communication; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="restfulClient">The restful client.</param>
        /// <param name="resource">The resource.</param>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="resourceUri">The resource URI.</param>
        /// <param name="parent">The parent.</param>
        internal Application(IRestfulClient restfulClient, ApplicationResource resource, Uri baseUri, Uri resourceUri, Applications parent)
                : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call Get on application and initialize communication resource
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <returns></returns>
        public async Task RefreshAndInitializeAsync(LoggingContext loggingContext = null)
        {
            Logger.Instance.Information("calling Application.RefreshAndInitializeAsync");
            await this.RefreshAsync(loggingContext).ConfigureAwait(false);
            if (this.PlatformResource?.Communication != null)
            {
                Uri resourceUri = UriHelper.CreateAbsoluteUri(this.BaseUri, this.PlatformResource.Communication.SelfUri);
                m_communication = new Communication(this.RestfulClient, this.PlatformResource.Communication, this.BaseUri, resourceUri, this);
            }
            else
            {
                throw new RemotePlatformServiceException("Not get communication resource from application");
            }
        }

        /// <summary>
        /// Gets an anonymous application token for a meeting. This token can be given to a user domain application. Using this token,
        /// the user can sign in and join the meeting.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="input">Specifies properties for the required token.</param>
        /// <returns>A token that can be used by a user to join the specified meeting.</returns>
        [Obsolete("Please use GetAnonApplicationTokenForMeetingAsync instead")]
        // All obsolete methods will be removed when releasing 1.0.0
        // We are keeping methods for prerelease as we don't want to break our partners every week :)
        public async Task<AnonymousApplicationTokenResource> GetAnonApplicationTokenAsync(LoggingContext loggingContext, AnonymousApplicationTokenInput input)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Logger.Instance.Information("Calling Application.RefreshAsync");
            await this.RefreshAsync(loggingContext).ConfigureAwait(false);

            Logger.Instance.Information("Start to fetching anonToken");

            string href = PlatformResource?.AnonymousApplicationTokens?.Href;
            if(href == null)
            {
                throw new CapabilityNotAvailableException("Link to get anonymous tokens is not available.");
            }

            Uri url = UriHelper.CreateAbsoluteUri(this.BaseUri, href);

            HttpResponseMessage httpResponse = await this.PostRelatedPlatformResourceAsync(url, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            try
            {
                //Does it neccessary to create a helper class from Common layer to do deserialize?
                Stream platformResourceStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
                return MediaTypeFormattersHelper.ReadContentWithType(typeof(AnonymousApplicationTokenResource), httpResponse.Content.Headers.ContentType, platformResourceStream) as AnonymousApplicationTokenResource;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Failed to diserialize anon token ");
                throw new RemotePlatformServiceException("Not get valid anon token resource from server, deserialize failure.", ex);
            }
        }

        /// <summary>
        /// Gets an anonymous application token for a meeting. This token can be given to a user domain application. Using this token,
        /// the user can sign in and join the meeting.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="meetingUrl">HTTP join url of the meeting</param>
        /// <param name="allowedOrigins">Semi colon separated list of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <returns>A token that can be used by a user to join the specified meeting.</returns>
        public Task<IAnonymousApplicationToken> GetAnonApplicationTokenForMeetingAsync(string meetingUrl, string allowedOrigins, string applicationSessionId, LoggingContext loggingContext = null)
        {
            if(string.IsNullOrEmpty(meetingUrl))
            {
                throw new ArgumentNullException(nameof(meetingUrl));
            }

            return GetAnonApplicationTokenAsync(loggingContext, meetingUrl, allowedOrigins, applicationSessionId);
        }

        /// <summary>
        /// Gets an anonymous application token for a meeting. This token can be given to a user domain application. Using this token,
        /// the user can sign in and join the meeting.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="meetingUrl">HTTP join url of the meeting</param>
        /// <param name="allowedOrigins">Semi colon separated list of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <returns>A token that can be used by a user to join the specified meeting.</returns>
        [Obsolete("Please use the other variation")]
        public Task<IAnonymousApplicationToken> GetAnonApplicationTokenForMeetingAsync(LoggingContext loggingContext, string meetingUrl, string allowedOrigins, string applicationSessionId)
        {
            return GetAnonApplicationTokenForMeetingAsync(meetingUrl, allowedOrigins, applicationSessionId, loggingContext);
        }

        /// <summary>
        /// Gets an anonymous application token for a P2P call. This token can be given to a user domain application. Using this token,
        /// the user can make P2P calls.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="allowedOrigins">List of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <returns>A token that can be used by a user to make P2P calls</returns>
        public Task<IAnonymousApplicationToken> GetAnonApplicationTokenForP2PCallAsync(string allowedOrigins, string applicationSessionId, LoggingContext loggingContext = null)
        {
            return GetAnonApplicationTokenAsync(loggingContext, null, allowedOrigins, applicationSessionId);
        }

        /// <summary>
        /// Gets an anonymous application token for a P2P call. This token can be given to a user domain application. Using this token,
        /// the user can make P2P calls.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="allowedOrigins">List of origins from where the user should be allowed to join the meeting using the IAnonymousApplicationToken</param>
        /// <param name="applicationSessionId">A unique ID required to get the token</param>
        /// <returns>A token that can be used by a user to make P2P calls</returns>
        [Obsolete("Please use the other variation")]
        public Task<IAnonymousApplicationToken> GetAnonApplicationTokenForP2PCallAsync(LoggingContext loggingContext, string allowedOrigins, string applicationSessionId)
        {
            return GetAnonApplicationTokenForP2PCallAsync(allowedOrigins, applicationSessionId, loggingContext);
        }

        /// <summary>
        /// Gets the AdhocMeeting Resource
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="input">Specifies configurations for the meeting to be created</param>
        /// <returns>An adhoc meeting</returns>
        [Obsolete("Please use CreateAdhocMeetingAsync instead")]
        public async Task<AdhocMeetingResource> GetAdhocMeetingResourceAsync(LoggingContext loggingContext, AdhocMeetingInput input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Logger.Instance.Information("calling Application.RefreshAndInitializeAsync");
            await this.RefreshAsync(loggingContext).ConfigureAwait(false);
            AdhocMeetingResource adhocMeetingResource = null;

            string href = PlatformResource?.OnlineMeetings?.SelfUri;
            if(href == null)
            {
                throw new CapabilityNotAvailableException("Link to create adhoc meeting is not available.");
            }

            Logger.Instance.Information("Start to fetching adhocMeetingResource");
            var url = UriHelper.CreateAbsoluteUri(this.BaseUri, href);

            var httpResponse = await this.PostRelatedPlatformResourceAsync(url, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            try
            {
                var skypeResourceStream = await httpResponse.Content.ReadAsStreamAsync().ConfigureAwait(false);
                adhocMeetingResource = MediaTypeFormattersHelper.ReadContentWithType(typeof(AdhocMeetingResource), httpResponse.Content.Headers.ContentType, skypeResourceStream) as AdhocMeetingResource;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Failed to diserialize anon token ");
                throw new RemotePlatformServiceException("Not get valid AdhocMeetingResource from server, deserialize failure.", ex);
            }

            return adhocMeetingResource;
        }

        /// <summary>
        /// Creates an adhoc meeting
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
        /// <param name="input">Specifies properties for the meeting to be created</param>
        /// <returns><see cref="IAdhocMeeting"/> which can be used to join the meeting or get meeting url, which can be passed onto real users to join it.</returns>
        public async Task<IAdhocMeeting> CreateAdhocMeetingAsync(AdhocMeetingCreationInput input, LoggingContext loggingContext = null)
        {
            if(input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            #pragma warning disable CS0618 // Type or member is obsolete
            AdhocMeetingResource adhocMeetingResource = await GetAdhocMeetingResourceAsync(loggingContext, input?.ToPlatformInput()).ConfigureAwait(false);
            #pragma warning restore CS0618 // Type or member is obsolete

            return new AdhocMeeting(RestfulClient, adhocMeetingResource, BaseUri, UriHelper.CreateAbsoluteUri(BaseUri, adhocMeetingResource.SelfUri), this);
        }

        /// <summary>
        /// Creates an adhoc meeting
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext" /> to be used for logging all related events.</param>
        /// <param name="input">Specifies properties for the meeting to be created</param>
        /// <returns><see cref="IAdhocMeeting" /> which can be used to join the meeting or get meeting url, which can be passed onto real users to join it.</returns>
        [Obsolete("Please use the other variation")]
        public Task<IAdhocMeeting> CreateAdhocMeetingAsync(LoggingContext loggingContext, AdhocMeetingCreationInput input)
        {
            return CreateAdhocMeetingAsync(input, loggingContext);
        }

        /// <summary>
        /// Gets whether a particular capability is available or not
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>true</code> if the capability is available at the time of invoking</returns>
        /// <remarks>
        /// Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="CapabilityNotAvailableException"/>
        /// when you are using a capability.
        /// </remarks>
        public override bool Supports(ApplicationCapability capability)
        {
            string href = null;

            switch(capability)
            {
                #pragma warning disable CS0618 // Type or member is obsolete
                case ApplicationCapability.GetAnonApplicationToken:
                #pragma warning restore CS0618 // Type or member is obsolete

                case ApplicationCapability.GetAnonApplicationTokenForMeeting:
                case ApplicationCapability.GetAnonApplicationTokenForP2PCall:
                    {
                        href = PlatformResource?.AnonymousApplicationTokens?.Href;
                        break;
                    }
                #pragma warning disable CS0618 // Type or member is obsolete
                case ApplicationCapability.GetAdhocMeetingResource:
                #pragma warning restore CS0618 // Type or member is obsolete

                case ApplicationCapability.CreateAdhocMeeting:
                    {
                        href = PlatformResource?.OnlineMeetings?.SelfUri;
                        break;
                    }
            }

            return !string.IsNullOrEmpty(href);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Extracts CustomizedCallbackUrl from corresponding <see cref="ClientPlatform"/>
        /// </summary>
        /// <returns>CustomizedCallbackUrl to be given to SfB</returns>
        internal string GetCustomizedCallbackUrl()
        {
            // CustomizedCallbackUrl is part of ClientPlatformSettings which is stored in IClientPlatform.
            // Traverse the chain of parents to get GetCustomizedCallbackUrl.
            object applicationsParent = (Parent as Applications).Parent;

            // Applications' parent is Discover in a prod environment but is ApplicationEndpoint for Sandbox applications
            ApplicationEndpoint applicationEndpoint = (applicationsParent is Discover ? (applicationsParent as Discover).Parent : applicationsParent) as ApplicationEndpoint;

            var clientPlatform = applicationEndpoint.ClientPlatform as ClientPlatform;
            return clientPlatform.CustomizedCallbackUrl;
        }

        /// <summary>
        /// Calculates what callbackUrl and callbackContext should be passed to PlatformService.
        /// </summary>
        /// <param name="callbackUrl">CallbackUrl as specified by SDK consumer as method input/parameter</param>
        /// <param name="callbackContext">CallbackContext as specified by SDK consumer as method input/parameter</param>
        /// <remarks>
        /// We have some obsolete methods which expose callbackUrl to the application consuming the SDK.
        /// These methods will be removed when we release 1.0.0 version of the SDK.
        /// Application can also provide callbackUrl as part of the ClientPlatformSettings, which is the preferred way.
        /// Logic explained:
        ///  1. If the application provides callbackUrl as parameter to a method, use it as it is and do not pass callbackContext.
        ///  2. If the application doesn't provide callbackUrl as parameter, then
        ///    a. If callbackUrl is set in ClientPlatformSettings and callbackContext is specified, append callbackContext as a
        ///       query parameter to callbackUrl and do not pass callbackContext
        ///    b. If callbackUrl is set in ClientPlatformSettings and callbackContext is not specified, use callbackUrl as it is
        ///       and do not pass callbackContext
        ///    c. If callbackUrl is not set in ClientPlatformSettings, then pass callbackContext
        /// </remarks>
        internal void GetCallbackUrlAndCallbackContext(ref string callbackUrl, ref string callbackContext)
        {
            if (callbackUrl == null)
            {
                callbackUrl = GetCustomizedCallbackUrl();
                if (callbackUrl != null && callbackContext != null)
                {
                    callbackUrl += callbackUrl.Contains("?") ? "&" : "?";
                    callbackUrl += "callbackContext=" + callbackContext;

                    callbackContext = null;
                }
            }
            else
            {
                callbackContext = null;
            }
        }

        #endregion

        #region Private methods

        private async Task<IAnonymousApplicationToken> GetAnonApplicationTokenAsync(LoggingContext loggingContext, string meetingUrl, string allowedOrigins, string applicationSessionId)
        {
            var input = new AnonymousApplicationTokenInput()
            {
                MeetingUrl = meetingUrl,
                AllowedOrigins = allowedOrigins,
                ApplicationSessionId = applicationSessionId
            };

            #pragma warning disable CS0618 // Type or member is obsolete
            AnonymousApplicationTokenResource anonymousApplicationToken = await GetAnonApplicationTokenAsync(loggingContext, input).ConfigureAwait(false);
            #pragma warning restore CS0618 // Type or member is obsolete

            return new AnonymousApplicationToken(RestfulClient, anonymousApplicationToken, BaseUri, UriHelper.CreateAbsoluteUri(BaseUri, anonymousApplicationToken.SelfUri), this);
        }

        #endregion
    }
}
