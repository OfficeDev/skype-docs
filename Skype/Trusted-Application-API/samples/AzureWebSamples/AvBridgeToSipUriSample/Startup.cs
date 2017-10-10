using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AVBridgeToSipUriSample.Startup))]

namespace AVBridgeToSipUriSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
