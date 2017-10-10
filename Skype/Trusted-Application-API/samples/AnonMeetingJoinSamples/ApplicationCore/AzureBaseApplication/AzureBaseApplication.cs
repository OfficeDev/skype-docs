using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// Define the base class for azure base application,
    /// Define the interface and base helper method for developer coneniece if build azure based application
    /// </summary>
    public abstract class AzureBasedApplicationBase
    {
        /// <summary>
        /// The instance id
        /// </summary>
        protected string InstanceId { get; set; }

        public string ResourceUriFormat { get; private set; }

        public string CallbackUriFormat { get; private set; }

        public ApplicationEndpoint ApplicationEndpoint { get; private set; }

        /// <summary>
        /// The initialize application endpoint function
        /// </summary>
        public async Task InitializeApplicationEndpointAsync(
                 string applicationEndpointUri,
                 string callbackUriFormat,
                 string resourcesUriFormat,
                 string audienceUri,
                 string aadClientId,
                 string aadClientSecret,
                 string appTokenCertThumbprint,
                 string instanceId,
                 bool isSandBoxEnvionment = false,
                 bool logFullHttpRequestResponse = true)
        {
            this.InstanceId = instanceId;
            this.ResourceUriFormat = resourcesUriFormat;
            this.CallbackUriFormat = callbackUriFormat;

            var logger = IOCHelper.Resolve<ConsoleLogger>();
            logger.HttpRequestResponseNeedsToBeLogged = logFullHttpRequestResponse;

            ClientPlatformSettings platformSettings = null;
            if (!string.IsNullOrEmpty(appTokenCertThumbprint))
            {              
                platformSettings = new ClientPlatformSettings(Guid.Parse(aadClientId), appTokenCertThumbprint);
            }
            else if (!string.IsNullOrEmpty(aadClientSecret))
            {   
                platformSettings = new ClientPlatformSettings(aadClientSecret, Guid.Parse(aadClientId));
            }
            else
            {
                throw new InvalidOperationException("Should provide at least one prarameter in aadClientSecret and appTokenCertThumbprint");
            }

            var platform = new ClientPlatform(platformSettings, logger);

            var endpointSettings = new ApplicationEndpointSettings(new SipUri(applicationEndpointUri));

            ApplicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, null);

            var loggingContext = new LoggingContext(Guid.NewGuid());
            await ApplicationEndpoint.InitializeAsync(loggingContext).ConfigureAwait(false);
            await ApplicationEndpoint.InitializeApplicationAsync(loggingContext).ConfigureAwait(false);
        }

        /// <summary>
        /// Start application async
        /// </summary>
        /// <returns></returns>
        public abstract Task StartAsync();

        /// <summary>
        /// Stop application async
        /// </summary>
        /// <returns></returns>
        public abstract Task StopAsync();
    }

    /// <summary>
    /// The job play ground Task
    /// </summary>
    public class SampleJobPlayGroundApplication : AzureBasedApplicationBase
    {
        public override Task StartAsync()
        {
            //Rely on command send from controller, like GetAnonTokenJobController, IncomingMessagingBridgeJobController, SimpleNotifyJobController
            //no need to do anything in app start
            return Task.CompletedTask;
        }

        public override Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
