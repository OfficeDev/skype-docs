using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd.Startup))]

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
