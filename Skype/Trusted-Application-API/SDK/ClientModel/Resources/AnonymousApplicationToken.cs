using System;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents a token which can be used by a real user to join a meeting or make P2P calls
    /// </summary>
    internal class AnonymousApplicationToken : BasePlatformResource<AnonymousApplicationTokenResource, AnonymousApplicationTokenCapability>, IAnonymousApplicationToken
    {
        internal AnonymousApplicationToken(IRestfulClient restfulClient, AnonymousApplicationTokenResource resource, Uri baseUri, Uri resourceUri, Application parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
        }

        /// <summary>
        /// The underlying authorization token
        /// </summary>
        public string AuthToken
        {
            get { return PlatformResource.AuthToken; }
        }

        /// <summary>
        /// Expiry time of <see cref="AuthToken"/>
        /// </summary>
        public DateTime AuthTokenExpiryTime
        {
            get { return PlatformResource.AuthTokenExpiryTime; }
        }

        /// <summary>
        /// Uri that can be used to discover SfB services required to join the meeting/make P2P call
        /// </summary>
        public Uri AnonymousApplicationsDiscoverUri
        {
            get
            {
                Uri uri;
                string href = PlatformResource?.AnonymousApplicationsDiscover?.Href;
                if (!Uri.TryCreate(href, UriKind.Absolute, out uri))
                {
                    throw new RemotePlatformServiceException("Couldn't read AnonymousApplicationsDiscover from AnonymousApplicationTokenResource.");
                }

                return uri;
            }
        }

        /// <summary>
        /// <see cref="AnonymousApplicationToken"/> doesn't support any capability so always returns <code>false</code>.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>false</code> </returns>
        public override bool Supports(AnonymousApplicationTokenCapability capability)
        {
            return false;
        }
    }
}
