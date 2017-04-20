using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Extends functionalities of a logger
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// logs HTTP response.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="response">The response.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="isIncomingRequest">if set to <c>true</c> [is incoming request].</param>
        /// <returns>Task.</returns>
        public static async Task LogHttpResponseAsync(this Logger logger, HttpResponseMessage response, string requestId, bool isIncomingRequest)
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            try
            {
                var myResponse = new SerializableHttpResponseMessage();
                await myResponse.InitializeAsync(response, requestId, isIncomingRequest).ConfigureAwait(false);

                string logString = await myResponse.GetLogStringAsync().ConfigureAwait(false);
                logger.Information(logString);
            }
            catch (Exception ex)
            {
                logger.Warning("Log http response failed.  Ex: " + ex.ToString());
            }
        }

        /// <summary>
        /// logs HTTP request.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="request">The request.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="isIncomingRequest">if set to <c>true</c> [is incoming request].</param>
        /// <returns>Task.</returns>
        public static async Task LogHttpRequestAsync(this Logger logger, HttpRequestMessage request, string requestId, bool isIncomingRequest)
        {
            var sb = new StringBuilder();
            try
            {
                var myRequest = new SerializableHttpRequestMessage();
                await myRequest.InitializeAsync(request, requestId, isIncomingRequest).ConfigureAwait(false);

                string logString = await myRequest.GetLogStringAsync().ConfigureAwait(false);
                logger.Information(logString);
            }
            catch (Exception ex)
            {
                logger.Warning("Log http request failed.  Ex: " + ex.ToString());
            }
        }
    }
}
