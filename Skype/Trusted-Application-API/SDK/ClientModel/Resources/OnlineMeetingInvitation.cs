using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class OnlineMeetingInvitation : Invitation<OnlineMeetingInvitationResource, OnlineMeetingInvitationCapability>, IOnlineMeetingInvitation
    {
        #region Public properties

        /// <summary>
        /// Anonymous meeting url
        /// </summary>
        public string MeetingUrl
        {
            get { return PlatformResource?.MeetingUri ?? string.Empty; }
        }

        #endregion

        #region Constructor

        internal OnlineMeetingInvitation(IRestfulClient restfulClient, OnlineMeetingInvitationResource resource, Uri baseUri, Uri resourceUri, Communication parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(OnlineMeetingInvitationCapability capability)
        {
            return false;
        }

        #endregion
    }
}
