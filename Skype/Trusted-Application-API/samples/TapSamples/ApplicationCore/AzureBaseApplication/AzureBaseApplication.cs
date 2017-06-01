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
            //The callback queue&event channel should be initialized whenever service start
            ICallbackMessageQueueManager callbackQueueManager = IOCHelper.Resolve<ICallbackMessageQueueManager>();
            QueueBasedEventChannel eventChannel = new QueueBasedEventChannel(callbackQueueManager);
            callbackQueueManager.InitializeCallbackMessageQueue(instanceId);
            this.InstanceId = instanceId;
            this.ResourceUriFormat = resourcesUriFormat;
            this.CallbackUriFormat = callbackUriFormat;

            var logger = IOCHelper.Resolve<IPlatformServiceLogger>();
            logger.HttpRequestResponseNeedsToBeLogged = logFullHttpRequestResponse;

            ClientPlatformSettings platformSettings = null;
            if (!string.IsNullOrEmpty(appTokenCertThumbprint))
            {
                //TAP partners use below code path:
                platformSettings = new ClientPlatformSettings
                    (
                     null,
                      Guid.Parse(aadClientId),
                      appTokenCertThumbprint,
                      isSandBoxEnvionment,
                      null,
                      true
                    );


                //public developers use below code path
                /*
                platformSettings = new ClientPlatformSettings(
                    Guid.Parse(aadClientId),
                    appTokenCertThumbprint,
                    isSandBoxEnvionment                
                    );
                    */
            }
            else if (!string.IsNullOrEmpty(aadClientSecret))
            {
                //TAP partners use below code path:
                platformSettings = new ClientPlatformSettings
                    (
                     null,
                      Guid.Parse(aadClientId),
                      null,
                      isSandBoxEnvionment,
                      aadClientSecret,
                      true
                    );

                //public developers use below code path
                /*
                  platformSettings = new ClientPlatformSettings(
                      aadClientSecret,
                       Guid.Parse(aadClientId),                    
                        isSandBoxEnvionment
                        );
                        */

            }
            else
            {
                throw new InvalidOperationException("Should provide at least one prarameter in aadClientSecret and appTokenCertThumbprint");
            }

            var platform = new ClientPlatform(platformSettings, logger);

            var endpointSettings = new ApplicationEndpointSettings(new SipUri(applicationEndpointUri));

            ApplicationEndpoint = new ApplicationEndpoint(platform, endpointSettings, eventChannel);

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

    /*
    /// <summary>
    /// The simple contact center task
    /// </summary>
    public class SimpleAcceptBridgeApplication : AzureBasedApplicationBase
    {
        public async override Task StartAsync()
        {
            //Currently just hard code the values,
            //In real world, should get from config file & presence store

            InstantMessagingBridgeJob job = new InstantMessagingBridgeJob(Guid.NewGuid().ToString(), this.InstanceId,
                new InstantMessagingBridgeJobInput
                {
                    InvitedTargetDisplayName = "Agent1",
                    InviteTargetUri = "sip:liben@metio.onmicrosoft.com",
                    IsStart = true,
                    Subject = "help desk subject",
                    WelcomeMessage = "welcome!",
                    LeaveStartedAfterTriggered = true
                });
            await job.ExecuteAsync().ConfigureAwait(false);
        }

        public async override Task StopAsync()
        {
            InstantMessagingBridgeJob job = new InstantMessagingBridgeJob(Guid.NewGuid().ToString(), this.InstanceId,
                    new InstantMessagingBridgeJobInput
                    {
                        InvitedTargetDisplayName = "Agent1",
                        InviteTargetUri = "sip:liben@metio.onmicrosoft.com",
                        IsStart = false,
                        Subject = "help desk subject",
                        WelcomeMessage = "welcome!",
                        LeaveStartedAfterTriggered = false
                    });

            await job.ExecuteAsync().ConfigureAwait(false);
        }
    }
    */
}
