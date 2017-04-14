using System;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using QuickSamplesCommon;

namespace WebEventChannel
{
    public class WebEventChannel : IEventChannel
    {
        public event EventHandler<EventsChannelArgs> HandleIncomingEvents;

        public static WebEventChannel Instance { get; set; } = new WebEventChannel();

        // Make WebEventChannel a singleton class
        private WebEventChannel()
        {
        }

        public void HandleIncomingHttpRequest(SerializableHttpRequestMessage message)
        {
            HandleIncomingEvents?.Invoke(Instance, new EventsChannelArgs(message));
        }

        public Task TryStartAsync()
        {
            return TaskHelpers.CompletedTask; // NOOP
        }

        public Task TryStopAsync()
        {
            return TaskHelpers.CompletedTask; // NOOP
        }

        public static IDisposable StartHttpServer(out Uri callbackUri)
        {
            // Anyone can read callbackUri from QuickSamplesConfig, we are returning it here just for convenience.

            callbackUri = new Uri(QuickSamplesConfig.MyCallbackUri);

            var localServerListeningAddress = QuickSamplesConfig.LocalServerListeningAddress;
            if (localServerListeningAddress == null)
            {
                // if localServerListeningAddress is not specified in app.config, use MyCallbackUri to start the server
                localServerListeningAddress = callbackUri.GetLeftPart(UriPartial.Authority);
            }

            // Start OWIN host
            return WebApp.Start<Startup>(url: localServerListeningAddress);
        }
    }
}
