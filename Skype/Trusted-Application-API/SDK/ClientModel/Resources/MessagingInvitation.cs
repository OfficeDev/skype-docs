using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class MessagingInvitation : Invitation<MessagingInvitationResource, MessagingInvitationCapability>, IMessagingInvitation
    {
        #region Constructor

        internal MessagingInvitation(IRestfulClient restfulClient, MessagingInvitationResource resource, Uri baseUri, Uri resourceUri, Communication parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Communication is required");
            }
        }

        #endregion

        #region Public methods

        [Obsolete("Please use ICommunication.StartAdhocMeetingAsync instead")]
        public Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(string subject, string callbackUrl, LoggingContext loggingContext = null)
        {
            return StartAdhocMeetingAsync(subject, null, callbackUrl, loggingContext);
        }

        /// <summary>
        /// AcceptAndBridgeAsync
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="meetingUrl"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public Task AcceptAndBridgeAsync(string meetingUrl, string displayName, LoggingContext loggingContext = null)
        {
            if (string.IsNullOrWhiteSpace(meetingUrl))
            {
                throw new ArgumentException(nameof(meetingUrl), "It cannot be null or whitespaces.");
            }

            Logger.Instance.Information(string.Format("[OnlineMeetingInviation] calling AcceptAndBridgeAsync. LoggingContext:{0}", loggingContext == null ? string.Empty : loggingContext.ToString()));

            string href = PlatformResource?.AcceptAndBridgeLink?.Href;

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to accept and bridge is not available.");
            }

            var input = new AcceptAndBridgeInput
            {
                MeetingUri = meetingUrl,
                LocalUserDisplayName = displayName
            };

            Uri bridge = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            return this.PostRelatedPlatformResourceAsync(bridge, input, new ResourceJsonMediaTypeFormatter(), loggingContext);
        }

        /// <summary>
        /// AcceptAndBridgeAsync
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="meetingUrl"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        [Obsolete("Please use the other variation")]
        public Task AcceptAndBridgeAsync(LoggingContext loggingContext, string meetingUrl, string displayName)
        {
            return AcceptAndBridgeAsync(meetingUrl, displayName, loggingContext);
        }

        public override bool Supports(MessagingInvitationCapability capability)
        {
            string href = null;
            switch (capability)
            {
                #pragma warning disable CS0618 // Type or member is obsolete
                case MessagingInvitationCapability.StartAdhocMeeting:
                #pragma warning restore CS0618 // Type or member is obsolete
                    {
                        href = PlatformResource?.StartAdhocMeetingLink?.Href;
                        break;
                    }
                case MessagingInvitationCapability.AcceptAndBridge:
                    {
                        href = PlatformResource?.AcceptAndBridgeLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        internal async Task<IOnlineMeetingInvitation> StartAdhocMeetingAsync(string subject, string callbackContext, string callbackUrl, LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.StartAdhocMeetingLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link start adhoc meeting in not available.");
            }

            Logger.Instance.Information(string.Format("[MessagingInvitation] calling StartAdhocMeetingAsync. LoggingContext:{0}", loggingContext == null ? string.Empty : loggingContext.ToString()));
            Communication communication = this.Parent as Communication;

            string operationId = Guid.NewGuid().ToString();
            TaskCompletionSource<IInvitation> tcs = new TaskCompletionSource<IInvitation>();
            //Adding current invitation to collection for tracking purpose.
            communication.HandleNewInviteOperationKickedOff(operationId, tcs);

            (communication.Parent as Application).GetCallbackUrlAndCallbackContext(ref callbackUrl, ref callbackContext);

            IInvitation invite = null;
            StartAdhocMeetingInput input = new StartAdhocMeetingInput
            {
                Subject = subject,
                CallbackContext = callbackContext,
                CallbackUrl = callbackUrl,
                OperationContext = operationId
            };
            var adhocMeetingUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            await this.PostRelatedPlatformResourceAsync(adhocMeetingUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            await Task.WhenAny(Task.Delay(WaitForEvents), tcs.Task).ConfigureAwait(false);
            if (!tcs.Task.IsCompleted && !tcs.Task.IsFaulted && !tcs.Task.IsCanceled)
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
    }
}
