using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CallForwardSample.Startup))]

namespace CallForwardSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
