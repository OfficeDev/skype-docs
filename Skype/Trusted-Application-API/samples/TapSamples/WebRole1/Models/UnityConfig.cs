using Microsoft.Azure;
using Microsoft.Practices.Unity;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    /// <summary>
    /// The Unity Configuration.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            //Register global used interface implementation here
            var container = IOCHelper.DefaultContainer;
            container.RegisterType<IPlatformServiceLogger, AzureDiagnosticLogger>(new ContainerControlledLifetimeManager(),
               new InjectionFactory(c => new AzureDiagnosticLogger()));

            var storageConnectionString = CloudConfigurationManager.GetSetting(Constants.STORAGE_CONNECTION);
            var serviceBusConnectionString = CloudConfigurationManager.GetSetting(Constants.SERVICEBUS_CONNECTION);

            container.RegisterType<IStorage, AzureStorage>(
                       new ContainerControlledLifetimeManager(),
                  new InjectionFactory(c => AzureStorage.Connect(storageConnectionString)));

            container.RegisterType<ICallbackMessageQueueManager, AzureServiceBusCallbackMessageQueueManager>(
                          new ContainerControlledLifetimeManager(),
               new InjectionFactory(c => new AzureServiceBusCallbackMessageQueueManager(serviceBusConnectionString)));
        }
    }
}