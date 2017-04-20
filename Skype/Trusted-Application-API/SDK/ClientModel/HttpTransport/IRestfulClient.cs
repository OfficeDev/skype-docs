using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// The interface of Restful Client.
    /// </summary>
    internal interface IRestfulClient
    {
        /// <summary>
        /// Get operation.
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <param name="mediaType">The media type.</param>
        /// <param name="charSet">The char set.</param>
        /// <returns>The HttpResponseMessage.</returns>
        Task<HttpResponseMessage> GetAsync(
            Uri requestUri,
            IDictionary<string, string> customerHeaders = null,
            string mediaType = "application/json",
            string charSet = "utf-8");

        /// <summary>
        /// Post operation (T).
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="mediaTypeFormatter">The media type formatter which is used for serialize.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        Task<HttpResponseMessage> PostAsync<T>(
            Uri requestUri,
            T value,
            MediaTypeFormatter mediaTypeFormatter,
            IDictionary<string, string> customerHeaders = null
          ) where T : class;

        /// <summary>
        /// Post operation (HttpContent).
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="httpContent">The instance of http content.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        Task<HttpResponseMessage> PostAsync(
            Uri requestUri,
            HttpContent httpContent,
            IDictionary<string, string> customerHeaders = null
            );

        /// <summary>
        /// Put operation (T).
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="mediaTypeFormatter">The media type formatter which is used for serialize.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        Task<HttpResponseMessage> PutAsync<T>(
            Uri requestUri,
            T value,
            MediaTypeFormatter mediaTypeFormatter,
            IDictionary<string, string> customerHeaders = null) where T : class;

        /// <summary>
        /// Put operation (HttpContent).
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="httpContent">The instance of http content.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        Task<HttpResponseMessage> PutAsync(
            Uri requestUri,
            HttpContent httpContent,
            IDictionary<string, string> customerHeaders = null);

        /// <summary>
        /// Delete operation.
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        Task<HttpResponseMessage> DeleteAsync(
            Uri requestUri,
            IDictionary<string, string> customerHeaders = null);
    }

    /// <summary>
    /// The interface for RestfulClientFactory
    /// </summary>
    internal interface IRestfulClientFactory
    {
        /// <summary>
        /// Gets the restful client.
        /// </summary>
        /// <param name="oauthIdentity">The oauth identity.</param>
        /// <param name="tokenProvider">The token provider.</param>
        /// <returns>IRestfulClient.</returns>
        IRestfulClient GetRestfulClient(OAuthTokenIdentifier oauthIdentity, ITokenProvider tokenProvider);
    }
}
