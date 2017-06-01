using QuickSamplesCommon;
using Microsoft.Practices.Unity;
using Microsoft.SfB.PlatformService.SDK.Common;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;
using System;
using System.Configuration;
using Microsoft.SfB.PlatformService.SDK.ClientModel;

namespace AudioVideoIVRSample
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
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

            //Get singleton logger
            var logger = UnityHelper.Resolve<IPlatformServiceLogger>();
            logger.HttpRequestResponseNeedsToBeLogged = true;

            //Get singleton event channel
            var eventChannel = UnityHelper.Resolve<SimpleEventChannel>();

            //Initialize platform
            var platformSettings = new ClientPlatformSettings
                 (
                   aadClientSecret,
                   Guid.Parse(aadClientId)                 
                 );

            var platform = new ClientPlatform(platformSettings, logger);


            //Initialize application and application endpoint
            var endpointSettings = new ApplicationEndpointSettings(new SipUri(applicationEndpointUri));
            var ApplicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, eventChannel);
            var loggingContext = new LoggingContext(Guid.NewGuid());

            await ApplicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await ApplicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);

            //Hook up listener for incoming call
            ApplicationEndpoint.HandleIncomingAudioVideoCall += HandleIncomingAVCall;
        }


        private void HandleIncomingAVCall(object sender, IncomingInviteEventArgs<IAudioVideoInvitation> args)
        {
            //Read job settings
            string callbackUri = ConfigurationManager.AppSettings["MyCallbackUri"];

            ApplicationEndpoint ae = sender as ApplicationEndpoint;
            AudioVideoIVRJob job = new AudioVideoIVRJob(args, callbackUri);
            job.Start();
        }

    }
}
