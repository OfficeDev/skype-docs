using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class ParticipantInvitation : Invitation<ParticipantInvitationResource, ParticipantInvitationCapability>, IParticipantInvitation
    {
        internal ParticipantInvitation(IRestfulClient restfulClient, ParticipantInvitationResource resource, Uri baseUri, Uri resourceUri, Communication parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(ParticipantInvitationCapability capability)
        {
            return false;
        }
    }
}
