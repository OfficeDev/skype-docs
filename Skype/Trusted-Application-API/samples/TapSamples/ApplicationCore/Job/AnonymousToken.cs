using System;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// The Anonymous Token Resource.
    /// </summary>
    public class AnonymousToken
    {
        /// <summary>
        /// Gets or sets the discover uri.
        /// </summary>
        public string DiscoverUri { get; set; }

        /// <summary>
        /// Gets or sets the anonymous token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the tenant endpoint id.
        /// </summary>
        public string TenantEndpointId { get; set; }

        /// <summary>
        /// Gets or sets the expire time.
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
