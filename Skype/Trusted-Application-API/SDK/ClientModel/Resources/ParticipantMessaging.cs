using System;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents the use of the instant messaging modality from a user in a conversation
    /// </summary>
    internal class ParticipantMessaging : BasePlatformResource<ParticipantMessagingResource, ParticipantMessagingCapability>, IParticipantMessaging
    {
        #region Constructor

        internal ParticipantMessaging(IRestfulClient restfulClient, ParticipantMessagingResource resource, Uri baseUri, Uri resourceUri, Participant parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Conversation is required");
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// <see cref="ParticipantMessaging"/> doesn't support any capability so always returns <code>false</code>.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>false</code> </returns>
        public override bool Supports(ParticipantMessagingCapability capability)
        {
            return false;
        }

        #endregion
    }
}
