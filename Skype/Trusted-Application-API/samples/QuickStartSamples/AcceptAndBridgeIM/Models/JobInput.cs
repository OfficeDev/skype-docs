using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace AcceptAndBridgeIM
{
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
    
    /// <summary>
    /// Base class for job input from application level
    /// </summary>
    public abstract class PlatformServiceJobInputBase
    {
    }

    /// <summary>
    /// InstantMessagingBridgeJobInput
    /// </summary>
    public class InstantMessagingBridgeJobInput : PlatformServiceJobInputBase
    {
        public bool IsStart { get; set; }
        public string Subject { get; set; }
        public string WelcomeMessage { get; set; }
        public string InviteTargetUri { get; set; }
        public string InvitedTargetDisplayName { get; set; }
        public bool EnableMessageFilter { get; set; }
    }
    
    /// <summary>
    /// Get AnonToken job input
    /// </summary>
    public class GetAnonTokenInput : PlatformServiceJobInputBase
    {
        public string AllowedOrigins { get; set; }
        public string ApplicationSessionId { get; set; }
        public string MeetingUrl { get; set; }
    }

}