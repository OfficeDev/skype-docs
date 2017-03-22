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

        public Task<HttpResponseMessage> AcceptAsync(LoggingContext loggingContext)
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

        public Task<HttpResponseMessage> ForwardAsync(LoggingContext loggingContext, string forwardTarget)
        {
            if (string.IsNullOrWhiteSpace(forwardTarget))
            {
                throw new ArgumentNullException(nameof(forwardTarget), nameof(forwardTarget) + " should not be null or whitespace");
            }

            string href = PlatformResource?.ForwardLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to forward AudioVideoInvitation is not available.");
            }

            Uri forwardLink = UriHelper.CreateAbsoluteUri(BaseUri, href);

            var input = new ForwardInput() { To = forwardTarget };
            return PostRelatedPlatformResourceAsync(forwardLink, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

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
                case AudioVideoInvitationCapability.StartMeeting:
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
        [Obsolete("Please use StartMeetingAsync instead")]
        public Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(string subject, string callbackContext, LoggingContext loggingContext = null)
        {
            return StartMeetingAsync(subject, callbackContext, loggingContext);
        }

        /// <summary>
        /// schedule and trusted join a adhoc meeting
        /// </summary>
        /// <param name="subject">the meeting subject</param>
        /// <param name="callbackContext">the call back context</param>
        /// <param name="loggingContext">the logging context</param>
        /// <returns></returns>
        public async Task<IOnlineMeetingInvitation> StartMeetingAsync(string subject, string callbackContext, LoggingContext loggingContext = null)
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

            Task completed  = await Task.WhenAny(Task.Delay(WaitForEvents), tcs.Task).ConfigureAwait(false);
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

        /// <summary>
        /// Accept the incoming call and set up b2b call with conference or target user
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="meetingUri">the onlinemeeting uri if you want to bridge to a conference</param>
        /// <param name="to">the sip uri if you want to bridge to a single person</param>
        /// <returns></returns>
        public Task AcceptAndBridgeAsync(LoggingContext loggingContext, string meetingUri, string to)
        {
            if (string.IsNullOrWhiteSpace(meetingUri) && string.IsNullOrWhiteSpace(to))
            {
                throw new ArgumentException("need to at least provide to or meeting uri for bridge");
            }

            Logger.Instance.Information(string.Format("[AudioVideoInviation] calling AcceptAndBridgeAsync. LoggingContext:{0}", loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.AcceptAndBridgeAudioVideoLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to accept and bridge is not available.");
            }

            var input = new AcceptAndBridgeAudioVideoInput
            {
                MeetingUri = meetingUri,
                ToUri = to
            };

            Uri bridge = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            return this.PostRelatedPlatformResourceAsync(bridge, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        #endregion
    }
}
