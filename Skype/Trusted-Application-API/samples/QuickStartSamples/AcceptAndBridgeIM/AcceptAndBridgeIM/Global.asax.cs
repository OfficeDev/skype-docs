using Microsoft.Practices.Unity;
using Microsoft.SfB.PlatformService.SDK.Common;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using System;
using System.Configuration;
using Microsoft.SfB.PlatformService.SDK.ClientModel;

namespace AcceptAndBridgeIM
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static ApplicationEndpoint ApplicationEndpoint { get; private set; }
        public static string CallbackUri { get; private set; }
        public static string InstanceId { get; private set; }

        static WebApiApplication()
        {
            InstanceId = Guid.NewGuid().ToString("N");
        }

        protected void Application_Start()
        {
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Register global used interface implementation here
            var container = UnityHelper.DefaultContainer;

            container.RegisterType<IPlatformServiceLogger, DiagnosticLogger>(new ContainerControlledLifetimeManager(),
               new InjectionFactory(c => new DiagnosticLogger()));

            container.RegisterType<SimpleEventChannel, SimpleEventChannel>(new ContainerControlledLifetimeManager(),
              new InjectionFactory(c => new SimpleEventChannel()));

            InitializeApplicationEndpointAsync().Wait();
        }

        private async Task InitializeApplicationEndpointAsync()
        {
            //Read application auth settings
            var applicationEndpointUri = ConfigurationManager.AppSettings["ApplicationEndpointId"];
            var aadClientId = ConfigurationManager.AppSettings["AAD_ClientId"];
            var aadClientSecret = ConfigurationManager.AppSettings["AAD_ClientSecret"];
            CallbackUri = ConfigurationManager.AppSettings["MyCallbackUri"];

            //Get singleton logger
            var logger = UnityHelper.Resolve<IPlatformServiceLogger>();
            logger.HttpRequestResponseNeedsToBeLogged = true;

            //Get singleton event channel
            var eventChannel = UnityHelper.Resolve<SimpleEventChannel>();

            //ClientPlatformSettings platformSettings =new ClientPlatformSettings(null, new Guid(aadClientId), null, aadClientSecret, true);

            //public developers use below code path
            var platformSettings = new ClientPlatformSettings(aadClientSecret, Guid.Parse(aadClientId));

            var platform = new ClientPlatform(platformSettings, logger);
            //Initialize application and application endpoint
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(applicationEndpointUri));
            ApplicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, eventChannel);
            var loggingContext = new LoggingContext(Guid.NewGuid());

            await ApplicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await ApplicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);
        }
    }
}
