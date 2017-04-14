using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Contains some useful methods to help in logging
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Writes a <see cref="HttpResponseMessage"/> object to logs
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/> to be logged</param>
        /// <param name="requestId">ID of the request (if available in request headers)</param>
        /// <param name="isIncomingRequest"><code>true</code> if the <see cref="HttpResponseMessage"/> is in response of an incoming HTTP request</param>
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

        /// <summary>
        /// Writes a <see cref="HttpRequestMessage"/> object to logs
        /// </summary>
        /// <param name="request"><see cref="HttpRequestMessage"/> to be logged</param>
        /// <param name="requestId">ID of the request (if available in request headers)</param>
        /// <param name="isIncomingRequest"><code>true</code> if this is an incoming HTTP request</param>
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

        /// <summary>
        /// Writes a <see cref="SerializableHttpRequestMessage"/> object to logs
        /// </summary>
        /// <param name="request"><see cref="SerializableHttpRequestMessage"/> to be logged</param>
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
