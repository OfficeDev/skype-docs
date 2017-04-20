using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents an OnlineMeeting
    /// </summary>
    /// <seealso cref="BasePlatformResource{TPlatformResource, TCapabilities}"/>
    /// <seealso cref="IConversationConference" />
    internal class ConversationConference : BasePlatformResource<ConversationConferenceResource, ConversationConferenceCapability>, IConversationConference
    {
        #region Constructor

        /// <summary>
        /// Initializes an instance of <see cref="ConversationConference"/>>
        /// </summary>
        /// <param name="restfulClient"></param>
        /// <param name="resource"></param>
        /// <param name="baseUri"></param>
        /// <param name="resourceUri"></param>
        /// <param name="parent"></param>
        internal ConversationConference(IRestfulClient restfulClient, ConversationConferenceResource resource, Uri baseUri, Uri resourceUri, Conversation parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get The onlineMeeting Uri property
        /// </summary>
        public string OnlineMeetingUri
        {
            get { return PlatformResource?.OnlineMeetingUri; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Terminate Online Meeting
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <returns></returns>
        public Task TerminateAsync(LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.TerminateMeetingResourceLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to terminate messaging is not available.");
            }

            Uri stopLink = UriHelper.CreateAbsoluteUri(this.BaseUri, href);
            return this.PostRelatedPlatformResourceAsync(stopLink, null, loggingContext);
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(ConversationConferenceCapability capability)
        {
            string href = null;
            switch (capability)
            {
                case ConversationConferenceCapability.Terminate:
                    {
                        href = PlatformResource?.TerminateMeetingResourceLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion
    }
}
