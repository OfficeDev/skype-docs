using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class OauthEvoRestfulClient : IRestfulClient, IDisposable
    {
        private readonly ITokenProvider m_tokenProvider;
        private readonly HttpClient m_httpClient;
        private readonly OAuthTokenIdentifier m_oauthIdentity;

        static OauthEvoRestfulClient()
        {
            // Adding this for the CallOrchstration Service call each virtual machines by itself since the cert CN name is the Azure service name not each machine name.
            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch || sslPolicyErrors == SslPolicyErrors.None)
                {
                    return true;
                }

                return false;
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OauthEvoRestfulClient"/> class.
        /// </summary>
        /// <param name="tokenProvider">The token provider.</param>
        /// <param name="oauthIdentity">The oauth identity.</param>
        public OauthEvoRestfulClient(ITokenProvider tokenProvider, OAuthTokenIdentifier oauthIdentity)
        {
            if (tokenProvider == null)
            {
                throw new ArgumentNullException(nameof(tokenProvider), "The parameter named tokenProvider can't be null.");
            }

            this.m_tokenProvider = tokenProvider;
            HttpClientHandler handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            m_httpClient = new HttpClient(handler);
            m_httpClient.Timeout = TimeSpan.FromSeconds(30);
            m_oauthIdentity = oauthIdentity;
        }

        /// <summary>
        /// Get operation.
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <param name="mediaType">The media type.</param>
        /// <param name="charSet">The char set.</param>
        /// <returns>The HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> GetAsync(
            Uri requestUri,
            IDictionary<string, string> customerHeaders = null,
            string mediaType = "application/json",
            string charSet = "utf-8"
           )
        {
            return this.HttpClientBaseMethodAsync(
                requestUri,
               (httpRequestMessage) =>
              {
                  httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType) { CharSet = charSet });
                  return httpRequestMessage;
              },
              customerHeaders
                );
        }

        /// <summary>
        /// Post operation (HttpContent).
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="httpContent">The instance of http content.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> PostAsync(
            Uri requestUri,
            HttpContent httpContent,
           IDictionary<string, string> customerHeaders = null
         )
        {
            return this.HttpClientBaseMethodAsync(
                requestUri,
                 (httpRequestMessage) =>
                 {
                     httpRequestMessage.Method = HttpMethod.Post;
                     httpRequestMessage.Content = httpContent;
                     return httpRequestMessage;
                 },
                customerHeaders
               );
        }

        /// <summary>
        /// Post operation (T).
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="mediaTypeFormatter">The media type formatter which is used for serialize.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> PostAsync<T>(
            Uri requestUri,
            T value,
            MediaTypeFormatter mediaTypeFormatter,
            IDictionary<string, string> customerHeaders = null) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "The parameter named value can't be null reference.");
            }

            if (mediaTypeFormatter == null)
            {
                throw new ArgumentNullException(nameof(mediaTypeFormatter), "The parameter named mediaTypeFormatter can't be null reference.");
            }

            return this.HttpClientBaseMethodAsync(
                requestUri,
                (httpRequestMessage) =>
                {
                    httpRequestMessage.Method = HttpMethod.Post;
                    httpRequestMessage.Content = new ObjectContent<T>(value, mediaTypeFormatter);
                    return httpRequestMessage;
                },
                customerHeaders
                );
        }

        /// <summary>
        /// Put operation (T).
        /// </summary>
        /// <typeparam name="T">Any class</typeparam>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="value">The instance of T.</param>
        /// <param name="mediaTypeFormatter">The media type formatter which is used for serialize.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> PutAsync<T>(
            Uri requestUri,
            T value,
            MediaTypeFormatter mediaTypeFormatter,
            IDictionary<string, string> customerHeaders = null
            ) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "The parameter named value can't be null reference.");
            }

            if (mediaTypeFormatter == null)
            {
                throw new ArgumentNullException(nameof(mediaTypeFormatter), "The parameter named mediaTypeFormatter can't be null reference.");
            }

            return this.HttpClientBaseMethodAsync(
                requestUri,
                 (httpRequestMessage) =>
                 {
                     httpRequestMessage.Method = HttpMethod.Put;
                     httpRequestMessage.Content = new ObjectContent<T>(value, mediaTypeFormatter);
                     return httpRequestMessage;
                 },
                 customerHeaders
                 );
        }

        /// <summary>
        /// Put operation (HttpContent).
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="httpContent">The instance of http content.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> PutAsync(
            Uri requestUri,
            HttpContent httpContent,
            IDictionary<string, string> customerHeaders = null)
        {
            if (httpContent == null)
            {
                throw new ArgumentNullException("stringContent", "The parameter named value can't be null reference.");
            }

            return this.HttpClientBaseMethodAsync(
                requestUri,
                (httpRequestMessage) =>
                {
                    httpRequestMessage.Method = HttpMethod.Put;
                    httpRequestMessage.Content = httpContent;
                    return httpRequestMessage;
                },
                customerHeaders
             );
        }

        /// <summary>
        /// Delete operation.
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        public Task<HttpResponseMessage> DeleteAsync(
            Uri requestUri,
            IDictionary<string, string> customerHeaders = null)
        {
            return this.HttpClientBaseMethodAsync(
                requestUri,
              (httpRequestMessage) =>
              {
                  httpRequestMessage.Method = HttpMethod.Delete;
                  return httpRequestMessage;
              },
              customerHeaders);
        }

        /// <summary>
        /// The base template method.
        /// </summary>
        /// <param name="requestUri">The request uri.</param>
        /// <param name="customizeHttpRequestFunc">The async function delegate.</param>
        /// <param name="customerHeaders">The customer headers.</param>
        /// <returns>The HttpResponseMessage.</returns>
        private async Task<HttpResponseMessage> HttpClientBaseMethodAsync(
            Uri requestUri,
            Func<HttpRequestMessage, HttpRequestMessage> customizeHttpRequestFunc,
            IDictionary<string, string> customerHeaders = null)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri), "The parameter named requestUri can't be null reference.");
            }

            if (!requestUri.IsAbsoluteUri)
            {
                throw new ArgumentException("The parameter named requestUri should be absolute uri.", nameof(requestUri));
            }

            string authToken = string.Empty;
            if (m_tokenProvider != null && m_oauthIdentity != null)
            {
                authToken = await m_tokenProvider.GetTokenAsync(m_oauthIdentity).ConfigureAwait(false);
            }

            var httpRequestMessage = new HttpRequestMessage { RequestUri = requestUri };
            if (customerHeaders?.Any() == true)
            {
                if (customerHeaders.ContainsKey(Constants.OriginalToken))
                {
                    authToken = customerHeaders[Constants.OriginalToken];
                }

                foreach (KeyValuePair<string, string> keyValue in customerHeaders)
                {
                    httpRequestMessage.Headers.Add(keyValue.Key, keyValue.Value);
                }
            }

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                httpRequestMessage.Headers.Authorization = AuthenticationHeaderValue.Parse(authToken);
            }
            String requestId = Guid.NewGuid().ToString();

            var customizedhttpRequestMessage = customizeHttpRequestFunc(httpRequestMessage);

            if (Logger.Instance.HttpRequestResponseNeedsToBeLogged)
            {
                await Logger.Instance.LogHttpRequestAsync(customizedhttpRequestMessage, requestId, false).ConfigureAwait(false);
            }

            var httpResponse = await this.m_httpClient.SendAsync(customizedhttpRequestMessage).ConfigureAwait(false);

            if (Logger.Instance.HttpRequestResponseNeedsToBeLogged)
            {
                await Logger.Instance.LogHttpResponseAsync(httpResponse, requestId, false).ConfigureAwait(false);
            }
            if (httpResponse.StatusCode == HttpStatusCode.TemporaryRedirect)
            {
                Uri redirectUri = null;
                if (httpResponse.Headers != null)
                {
                    redirectUri = httpResponse.Headers.Location;
                }

                if (redirectUri != null)
                {
                    var newHttpRequestMessage = new HttpRequestMessage(httpRequestMessage.Method, redirectUri);
                    if (customerHeaders != null)
                    {
                        foreach (string key in customerHeaders.Keys)
                        {
                            newHttpRequestMessage.Headers.Add(key, customerHeaders[key]);
                        }
                    }

                    newHttpRequestMessage.Headers.Authorization = httpRequestMessage.Headers.Authorization;
                    String newRequestId = Guid.NewGuid().ToString();

                    var newCustomizedHttpRequestMessage = customizeHttpRequestFunc(newHttpRequestMessage);

                    if (Logger.Instance.HttpRequestResponseNeedsToBeLogged)
                    {
                        await Logger.Instance.LogHttpRequestAsync(newCustomizedHttpRequestMessage, requestId, false).ConfigureAwait(false);
                    }
                    httpResponse = await this.m_httpClient.SendAsync(newCustomizedHttpRequestMessage).ConfigureAwait(false);

                    if (Logger.Instance.HttpRequestResponseNeedsToBeLogged)
                    {
                        await Logger.Instance.LogHttpResponseAsync(httpResponse, requestId, false).ConfigureAwait(false);
                    }
                }
            }

            return httpResponse;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            m_httpClient.Dispose();
        }
    }

    /// <summary>
    /// The interface for RestfulClientFactory
    /// </summary>
    internal class RestfulClientFactory :  IRestfulClientFactory
    {
        /// <summary>
        /// The OAuth EVO HTTP client cache
        /// </summary>
        private readonly LeastRecentlyUsedCache<OAuthTokenIdentifier, IRestfulClient> m_oAuthEvoHttpClientCache;

        public RestfulClientFactory()
        {
            LeastRecentlyUsedCacheSettings settings = new LeastRecentlyUsedCacheSettings();
            settings.Staleness = TimeSpan.FromHours(1);
            m_oAuthEvoHttpClientCache = new LeastRecentlyUsedCache<OAuthTokenIdentifier, IRestfulClient>(settings);
        }

        /// <summary>
        /// Gets the restful client.
        /// </summary>
        /// <param name="oauthIdentity">The oauth identity.</param>
        /// <param name="tokenProvider">The token provider.</param>
        /// <returns>IRestfulClient.</returns>
        public IRestfulClient GetRestfulClient(OAuthTokenIdentifier oauthIdentity, ITokenProvider tokenProvider)
        {
            return m_oAuthEvoHttpClientCache.GetOrCreate(oauthIdentity,
                () => new OauthEvoRestfulClient(tokenProvider, oauthIdentity));
        }
    }
}
