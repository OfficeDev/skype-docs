using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    public static class LogHelper
    {
        public static async Task LogProtocolHttpResponseAsync(HttpResponseMessage response, string requestId, bool isIncomingRequest)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            try
            {
                SerializableHttpResponseMessage myResponse = new SerializableHttpResponseMessage();
                await myResponse.InitializeAsync(response, requestId, isIncomingRequest).ConfigureAwait(false);

                string logString = await myResponse.GetLogStringAsync().ConfigureAwait(false);
                Logger.Instance.Information(logString);
            }
            catch (Exception ex)
            {
                Logger.Instance.Warning("Log http response failed.  Ex: " + ex.ToString());
            }
        }

        public static async Task LogProtocolHttpRequestAsync(HttpRequestMessage request, string requestId, bool isIncomingRequest)
        {
            var sb = new StringBuilder();
            try
            {
                var myRequest = new SerializableHttpRequestMessage();
                await myRequest.InitializeAsync(request, requestId, isIncomingRequest).ConfigureAwait(false);

                string logString = await myRequest.GetLogStringAsync().ConfigureAwait(false);
                Logger.Instance.Information(logString);
            }
            catch (Exception ex)
            {
                Logger.Instance.Warning("Log http request failed.  Ex: " + ex.ToString());
            }
        }

        public static async Task LogProtocolHttpRequestAsync(SerializableHttpRequestMessage request)
        {
            var sb = new StringBuilder();
            try
            {
                string logString = await request.GetLogStringAsync().ConfigureAwait(false);
                Logger.Instance.Information(logString);
            }
            catch (Exception ex)
            {
                Logger.Instance.Warning("Log http request failed.  Ex: " + ex.ToString());
            }
        }
    }
}
