using System;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using System.Net.Http;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class AudioVideoInvitation : Invitation<AudioVideoInvitationResource, AudioVideoInvitationCapability>, IAudioVideoInvitation
    {
        #region Constructor

        internal AudioVideoInvitation(IRestfulClient restfulClient, AudioVideoInvitationResource resource, Uri baseUri, Uri resourceUri, Communication parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Accepts the <see cref="AudioVideoInvitation"/>> asynchronous.
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="CapabilityNotAvailableException">Link to accept AudioVideoInvitation is not available.</exception>
        public Task<HttpResponseMessage> AcceptAsync(LoggingContext loggingContext = null)

        {
            string href = PlatformResource?.AcceptLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to accept AudioVideoInvitation is not available.");
            }

            Uri acceptLink = UriHelper.CreateAbsoluteUri(BaseUri, href);

            var input = new AcceptInput() { MediaHost = MediaHostType.Remote };
            return PostRelatedPlatformResourceAsync(acceptLink, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        [Obsolete("Please use the other variation")]
        public Task<HttpResponseMessage> ForwardAsync(LoggingContext loggingContext, string forwardTarget)
        {
            return ForwardAsync(new SipUri(forwardTarget), loggingContext);
        }

        /// <summary>
        /// Forwards the <see cref="AudioVideoInvitation"/> asynchronous.
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <param name="forwardTarget">The forward target.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">forwardTarget - forwardTarget</exception>
        /// <exception cref="CapabilityNotAvailableException">Link to forward AudioVideoInvitation is not available.</exception>
        public Task<HttpResponseMessage> ForwardAsync(SipUri forwardTarget, LoggingContext loggingContext = null)

        {
            if (forwardTarget == null)
            {
                throw new ArgumentNullException(nameof(forwardTarget));
            }

            string href = PlatformResource?.ForwardLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to forward AudioVideoInvitation is not available.");
            }

            Uri forwardLink = UriHelper.CreateAbsoluteUri(BaseUri, href);

            var input = new ForwardInput() { To = forwardTarget.ToString() };
            return PostRelatedPlatformResourceAsync(forwardLink, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        /// <summary>
        /// Declines the <see cref="AudioVideoInvitation"/> asynchronous.
        /// </summary>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task&lt;HttpResponseMessage&gt;.</returns>
        /// <exception cref="CapabilityNotAvailableException">Link to decline AudioVideoInvitation is not available.</exception>
        public Task<HttpResponseMessage> DeclineAsync(LoggingContext loggingContext)
        {
            string href = PlatformResource?.DeclineOperationLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to decline AudioVideoInvitation is not available.");
            }

            Uri declineLink = UriHelper.CreateAbsoluteUri(BaseUri, href);

            var input = string.Empty;
            return PostRelatedPlatformResourceAsync(declineLink, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(AudioVideoInvitationCapability capability)
        {
            string href = null;
            switch(capability)
            {
                case AudioVideoInvitationCapability.Accept:
                    {
                        href = PlatformResource?.AcceptLink?.Href;
                        break;
                    }
                case AudioVideoInvitationCapability.Forward:
                    {
                        href = PlatformResource?.ForwardLink?.Href;
                        break;
                    }
                case AudioVideoInvitationCapability.Decline:
                    {
                        href = PlatformResource?.DeclineOperationLink?.Href;
                        break;
                    }
                case AudioVideoInvitationCapability.AcceptAndBridge:
                    {
                        href = PlatformResource?.AcceptAndBridgeAudioVideoLink?.Href;
                        break;
                    }
                #pragma warning disable CS0618 // Type or member is obsolete
                case AudioVideoInvitationCapability.StartAdhocMeeting:
                #pragma warning restore CS0618 // Type or member is obsolete
                    {
                        href = PlatformResource?.StartAdhocMeetingLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        /// <summary>
        /// schedule and trusted join a adhoc meeting
        /// </summary>
        /// <param name="subject">the meeting subject</param>
        /// <param name="callbackContext">the call back context</param>
        /// <param name="loggingContext">the logging context</param>
        /// <returns></returns>
        [Obsolete("Please use ICommunication.StartAdhocMeetingAsync instead")]
        public Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(string subject, string callbackContext, LoggingContext loggingContext = null)
        {
            return StartMeetingAsync(subject, callbackContext, loggingContext);
        }

        /// <summary>
        /// Accept the incoming call and set up b2b call with conference or target user
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="meetingUri">the onlinemeeting uri if you want to bridge to a conference</param>
        /// <returns></returns>
        public Task AcceptAndBridgeAsync(string meetingUri, LoggingContext loggingContext = null)
        {
            if (string.IsNullOrWhiteSpace(meetingUri))
            {
                throw new ArgumentNullException(nameof(meetingUri));
            }

            return AcceptAndBridgeAsync(meetingUri, null, loggingContext);
        }

        /// <summary>
        /// Accept the incoming call and set up b2b call with conference or target user
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="to">the sip uri if you want to bridge to a single person</param>
        /// <returns></returns>
        public Task AcceptAndBridgeAsync(SipUri to, LoggingContext loggingContext = null)
        {
            if (to == null)
            {
                throw new ArgumentNullException(nameof(to));
            }

            return AcceptAndBridgeAsync(null, to, loggingContext);
        }

        /// <summary>
        /// Accept the incoming call and set up b2b call with conference or target user
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="meetingUri">the onlinemeeting uri if you want to bridge to a conference</param>
        /// <param name="to">the sip uri if you want to bridge to a single person</param>
        /// <returns></returns>
        [Obsolete("Please use the other variation")]
        public Task AcceptAndBridgeAsync(LoggingContext loggingContext, string meetingUri, string to)
        {
            if (string.IsNullOrWhiteSpace(meetingUri) && to == null)
            {
                throw new ArgumentException("need to at least provide to or meeting uri for bridge");
            }

            return AcceptAndBridgeAsync(meetingUri, new SipUri(to), loggingContext);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// schedule and trusted join a adhoc meeting
        /// </summary>
        /// <param name="subject">the meeting subject</param>
        /// <param name="callbackContext">the call back context</param>
        /// <param name="loggingContext">the logging context</param>
        /// <returns></returns>
        internal async Task<IOnlineMeetingInvitation> StartMeetingAsync(string subject, string callbackContext, LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.StartAdhocMeetingLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link start adhoc meeting in not available.");
            }

            Logger.Instance.Information(string.Format("[AudioVideoInvitation] calling StartAdhocMeetingAsync. LoggingContext:{0}", loggingContext == null ? string.Empty : loggingContext.ToString()));
            var communication = this.Parent as Communication;

            string operationId = Guid.NewGuid().ToString();
            var tcs = new TaskCompletionSource<IInvitation>();
            //Adding current invitation to collection for tracking purpose.
            communication.HandleNewInviteOperationKickedOff(operationId, tcs);

            string callbackUrl = null;
            var application = communication.Parent as Application;
            application.GetCallbackUrlAndCallbackContext(ref callbackUrl, ref callbackContext);

            IInvitation invite = null;
            var input = new StartAdhocMeetingInput
            {
                Subject = subject,
                CallbackContext = callbackContext,
                CallbackUrl = callbackUrl,
                OperationContext = operationId
            };
            var adhocMeetingUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            await this.PostRelatedPlatformResourceAsync(adhocMeetingUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            Task completed = await Task.WhenAny(Task.Delay(WaitForEvents), tcs.Task).ConfigureAwait(false);
            if (completed != tcs.Task)
            {
                throw new RemotePlatformServiceException("Timeout to get Onlinemeeting Invitation started event from platformservice!");
            }
            else
            {
                invite = await tcs.Task.ConfigureAwait(false);// Incase need to throw exception
            }

            //We are sure the invite sure be there now.
            OnlineMeetingInvitation result = invite as OnlineMeetingInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a Onlinemeeting resource with operationId " + operationId);
            }

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Accept the incoming call and set up b2b call with conference or target user
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="meetingUri">the onlinemeeting uri if you want to bridge to a conference</param>
        /// <param name="to">the sip uri if you want to bridge to a single person</param>
        /// <returns></returns>
        private Task AcceptAndBridgeAsync(string meetingUri, SipUri to, LoggingContext loggingContext = null)
        {
            Logger.Instance.Information(string.Format("[AudioVideoInviation] calling AcceptAndBridgeAsync. LoggingContext:{0}", loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.AcceptAndBridgeAudioVideoLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to accept and bridge is not available.");
            }

            var input = new AcceptAndBridgeAudioVideoInput
            {
                MeetingUri = meetingUri,
                ToUri = to?.ToString()
            };

            Uri bridge = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            return this.PostRelatedPlatformResourceAsync(bridge, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        #endregion
    }
}
