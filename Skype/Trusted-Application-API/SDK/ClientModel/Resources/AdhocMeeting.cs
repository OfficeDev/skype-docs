using System;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents a meeting
    /// </summary>
    internal class AdhocMeeting : BasePlatformResource<AdhocMeetingResource, AdhocMeetingCapability>, IAdhocMeeting
    {
        /// <summary>
        /// Creates an instance of <see cref="AdhocMeeting"/>
        /// </summary>
        /// <param name="restfulClient"><see cref="IRestfulClient"/> to be used to make HTTP requests</param>
        /// <param name="resource">This object's underlying resource from SfB's resource contract</param>
        /// <param name="baseUri">Base uri of the resource</param>
        /// <param name="resourceUri"><paramref name="resource"/>'s uri relative to <paramref name="baseUri"/></param>
        /// <param name="parent">Parent <see cref="Application"/> to which this object belongs to</param>
        internal AdhocMeeting(IRestfulClient restfulClient, AdhocMeetingResource resource, Uri baseUri, Uri resourceUri, Application parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        /// <summary>
        /// A HTTP url which can be given to users to join this meeting via Lync Web App
        /// </summary>
        public string JoinUrl
        {
            get { return PlatformResource.JoinUrl; }
        }

        /// <summary>
        /// SIP uri of the meeting
        /// </summary>
        public string OnlineMeetingUri
        {
            get { return PlatformResource.OnlineMeetingUri; }
        }

        /// <summary>
        /// Subject specified when the meeting was created
        /// </summary>
        public string Subject
        {
            get { return PlatformResource.Subject; }
        }

        /// <summary>
        /// Joins the adhoc meeting
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events</param>
        /// <param name="callbackContext">A state/context object which will be provided by SfB in all related events</param>
        /// <returns><see cref="IOnlineMeetingInvitation"/> which can be used to wait for the meeting join to complete</returns>
        public async Task<IOnlineMeetingInvitation> JoinAdhocMeeting(LoggingContext loggingContext, string callbackContext)
        {
            string href = PlatformResource?.JoinAdhocMeetingLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to join adhoc meeting is not available.");
            }

            var callbackUrl = (Parent as Application).GetCustomizedCallbackUrl();
            if (callbackUrl != null && callbackContext != null)
            {
                // We need to append callbackContext as a query paramter to callbackUrl
                callbackUrl += callbackUrl.Contains("?") ? "&" : "?";
                callbackUrl += "callbackContext=" + callbackContext;

                // We don't want to pass callbackContext if callbackUrl is being passed
                callbackContext = null;
            }

            var joinMeetingInput = new JoinMeetingInvitationInput()
            {
                CallbackContext = callbackContext,
                OperationContext = Guid.NewGuid().ToString(),
                CallbackUrl = callbackUrl
            };

            var communication = (Parent as Application).Communication as Communication;
            IInvitation invite = null;

            //Adding current invitation to collection for tracking purpose.
            var tcs = new TaskCompletionSource<IInvitation>();
            communication.HandleNewInviteOperationKickedOff(joinMeetingInput.OperationContext, tcs);

            Logger.Instance.Information("Joining adhoc meeting " + href);

            var adhocMeetingUri = UriHelper.CreateAbsoluteUri(BaseUri, href);

            await PostRelatedPlatformResourceAsync(adhocMeetingUri, joinMeetingInput, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);
            Task finishedTask = await Task.WhenAny(Task.Delay(WaitForEvents), tcs.Task).ConfigureAwait(false);

            if (finishedTask != tcs.Task)
            {
                throw new RemotePlatformServiceException("Timeout to get OnlinemeetingInvitation started event from platformservice");
            }
            else
            {
                invite = await tcs.Task.ConfigureAwait(false); // Incase need to throw exception
            }

            // We know for sure that the invitation is there now.
            IOnlineMeetingInvitation result = invite as IOnlineMeetingInvitation;
            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice did not deliver a OnlinemeetingInvitation resource with operationId " + joinMeetingInput.OperationContext);
            }

            return result;
        }

        /// <summary>
        /// Gets whether a particular capability is available or not
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>true</code> iff the capability is available at the time of invoking</returns>
        /// <remarks>
        /// Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="CapabilityNotAvailableException"/>
        /// when you are using a capability.
        /// </remarks>
        public override bool Supports(AdhocMeetingCapability capability)
        {
            string href = null;

            switch (capability)
            {
                case AdhocMeetingCapability.JoinAdhocMeeting:
                    {
                        href = PlatformResource?.JoinAdhocMeetingLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrEmpty(href);
        }
    }
}
