using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    [Serializable]
    public class PlatformServiceClientException : Exception
    {
        internal PlatformServiceClientException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {
        }
    }

    [Serializable]
    public class RemotePlatformServiceException : PlatformServiceClientException
    {
        #region Public constructors

        /// <summary>
        /// Initializes a new instance of <see cref="RemotePlatformServiceException"/> with given <paramref name="errorMessage"/> and
        /// <paramref name="innerException"/>
        /// </summary>
        /// <param name="errorMessage">Message explaining the exception</param>
        /// <param name="innerException">Original exception which result in this <see cref="RemotePlatformServiceException"/></param>
        internal RemotePlatformServiceException(string errorMessage, Exception innerException = null)
             : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RemotePlatformServiceException class
        /// </summary>
        /// <param name="errorMessage">Message explaining the exception</param>
        /// <param name="errorInformation"><see cref="ErrorInformation"/> returned by PlatformService</param>
        internal RemotePlatformServiceException(string errorMessage, ErrorInformation errorInformation)
             : base(errorMessage, null)
        {
            this.ErrorInformation = errorInformation;
        }

        /// <summary>
        /// Initializes a new instance of the RemotePlatformServiceException class.
        /// </summary>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="httpStatusCode">The http status code.</param>
        /// <param name="loggingContext">The logging context.</param>
        /// <param name="innerException">The inner exception.</param>
        internal RemotePlatformServiceException(
                string errorMessage,
                HttpStatusCode httpStatusCode,
                LoggingContext loggingContext,
                Exception innerException = null)
                : base(errorMessage, innerException)
        {
            HttpStatusCode = httpStatusCode;
            if (loggingContext != null)
            {
                this.PlatformServiceCorrelationId = loggingContext.PlatformResponseCorrelationId;
                this.PlatformServiceFqdn = loggingContext.PlatformResponseServerFqdn;
                if (loggingContext.PropertyBag?.ContainsKey(Constants.RemotePlatformServiceUri) == true)
                {
                    PlatformServiceServiceUri = loggingContext.PropertyBag[Constants.RemotePlatformServiceUri] as Uri;
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets the HttpStatusCode.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Gets the Ucap Correlation Id.
        /// </summary>
        public string PlatformServiceCorrelationId { get; }

        /// <summary>
        /// <see cref="ErrorInformation"/> that caused this <see cref="RemotePlatformServiceException"/>.
        /// </summary>
        public ErrorInformation ErrorInformation { get; }

        /// <summary>
        /// Gets the Ucap Fqdn.
        /// </summary>
        public string PlatformServiceFqdn { get; }

        /// <summary>
        /// Gets the Partner Service Uri.
        /// </summary>
        public Uri PlatformServiceServiceUri { get; }

        /// <summary>
        /// Convert Http Response Message to Partner Service Exception.
        /// </summary>
        /// <param name="httpResponseMessage">The http response message.</param>
        /// <param name="externalServiceType">The external service type.</param>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns><see cref="RemotePlatformServiceException"/>.</returns>
        internal static async Task<RemotePlatformServiceException> ConvertToRemotePlatformServiceExceptionAsync(HttpResponseMessage httpResponseMessage, LoggingContext loggingContext)
        {
            var errorMessage = string.Empty;
            if (httpResponseMessage.Content != null)
            {
                errorMessage = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return new RemotePlatformServiceException(errorMessage, httpResponseMessage.StatusCode, loggingContext);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\r\n HttpStatusCode " + this.HttpStatusCode.ToString());
            if (this.PlatformServiceServiceUri != null)
            {
                sb.Append("\r\n PlatformServiceServiceUri " + this.PlatformServiceServiceUri.ToString());
            }
            if (!string.IsNullOrEmpty(this.PlatformServiceCorrelationId))
            {
                sb.Append("\r\n PlatformServiceCorrelationId " + this.PlatformServiceCorrelationId);
            }
            if (!string.IsNullOrEmpty(this.PlatformServiceFqdn))
            {
                sb.Append("\r\n PlatformServiceFqdn " + this.PlatformServiceFqdn);
            }
            return base.ToString() + sb.ToString();
        }
    }

    [Serializable]
    public class PlatformServiceClientInvalidOperationException : PlatformServiceClientException
    {
        internal PlatformServiceClientInvalidOperationException(string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
        {
        }
    }

    [Serializable]
    public class CapabilityNotAvailableException : PlatformServiceClientInvalidOperationException
    {
        internal CapabilityNotAvailableException(string errorMessage, Exception innerException = null)
            : base(errorMessage, innerException)
        {
        }
    }
}
