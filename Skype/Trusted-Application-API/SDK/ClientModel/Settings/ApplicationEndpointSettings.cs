using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    public class ApplicationEndpointSettings
    {
        public SipUri ApplicationEndpointId { get; }

        internal TargetServiceType TargetServiceType { get; }

        public ApplicationEndpointSettings(SipUri applicationEndpointId)
        {
            ApplicationEndpointId = applicationEndpointId;
            TargetServiceType     = TargetServiceType.PlatformService;
        }
    }
}
