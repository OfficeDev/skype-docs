using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents settings of an application endpoint.
    /// </summary>
    public class ApplicationEndpointSettings
    {
        /// <summary>
        /// Gets the application endpoint Id.
        /// </summary>
        /// <value>The application endpoint Id.</value>
        public SipUri ApplicationEndpointId { get; }

        internal TargetServiceType TargetServiceType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationEndpointSettings"/> class.
        /// </summary>
        /// <param name="applicationEndpointId">The application endpoint identifier.</param>
        public ApplicationEndpointSettings(SipUri applicationEndpointId)
        {
            ApplicationEndpointId = applicationEndpointId;
            TargetServiceType     = TargetServiceType.PlatformService;
        }
    }
}
