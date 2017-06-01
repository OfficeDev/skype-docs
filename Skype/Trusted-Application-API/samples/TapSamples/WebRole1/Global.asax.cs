using Microsoft.Azure;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    public class WebApiApplication : HttpApplication
    {
        #region Public properties

        /// <summary>
        /// A unique ID to identify this <see cref="HttpApplication"/>.
        /// </summary>
        /// <remarks>
        /// It is mostly used for logging purposes.
        /// </remarks>
        public static string InstanceId {get; private set;}

        /// <summary>
        /// The application to start
        /// </summary>
        public static AzureBasedApplicationBase AzureApplication { get; private set; }

        #endregion

        #region Static constructors

        /// <summary>
        /// Initializes static properties.
        /// </summary>
        static WebApiApplication()
        {
            InstanceId = Guid.NewGuid().ToString("N");
        }

        #endregion

        protected void Application_Start()
        {
            //Standard web service start steps
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Register interfaces implementation used global wide
            UnityConfig.RegisterComponents();

            //Read settings from cloud config.
            var audienceUri = CloudConfigurationManager.GetSetting("PlatformAudienceUri");
            var callbackUriFormat = CloudConfigurationManager.GetSetting("CallbackUriFormat");
            var resourcesUriFormat = CloudConfigurationManager.GetSetting("ResourcesUriFormat");
            var applicationEndpointUri = CloudConfigurationManager.GetSetting("ApplicationEndpointId");
            var aadClientId = CloudConfigurationManager.GetSetting("AAD_ClientId");
            var aadClientSecret = CloudConfigurationManager.GetSetting("AAD_ClientSecret");
            var appTokenCertThumbprint = CloudConfigurationManager.GetSetting("AppTokenCertThumbprint");
            bool logFullHttpRequestResponse = false;
            Boolean.TryParse(CloudConfigurationManager.GetSetting("LogFullHttpRequestResponse"), out logFullHttpRequestResponse);
            bool isSandBoxEnv = false;
            Boolean.TryParse(CloudConfigurationManager.GetSetting("IsSandBoxEnv"), out isSandBoxEnv);



            //Initialize application
            AzureApplication = new SampleJobPlayGroundApplication();//The azure app will act as a sample job playground, which accept command from controller (incoming http requests), and execute simple job tasks.        
            AzureApplication.InitializeApplicationEndpointAsync
                (                 
                    applicationEndpointUri,
                    callbackUriFormat,
                    resourcesUriFormat,
                    audienceUri,
                    aadClientId,
                    aadClientSecret,
                    appTokenCertThumbprint,
                    InstanceId,
                    isSandBoxEnv,
                    logFullHttpRequestResponse
                ).Wait();

            AzureApplication.StartAsync().Wait();
        }

        protected void Application_End()
        {
            this.CleanupInstanceStorage();
            if (AzureApplication != null)
            {
                AzureApplication.StopAsync().Observe<Exception>();
            }
        }

        private void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Logger.Instance.Error("Unhandle exception hit: " + e.ToString());
            this.CleanupInstanceStorage();
        }

        private void CleanupInstanceStorage()
        {
            try
            {
                ICallbackMessageQueueManager callbackQueue = IOCHelper.Resolve<ICallbackMessageQueueManager>();
                if (callbackQueue != null)
                {
                    callbackQueue.DeleteCallbackMessageQueue(WebApiApplication.InstanceId);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Exception hit in dispose service bus queue: " + ex.ToString());
            }
        }
    }
}
