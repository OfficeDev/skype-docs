using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Base class for platformservice related exceptions
    /// </summary>
    [Serializable]
    public class PlatformServiceClientException : Exception
    {
        internal PlatformServiceClientException(string errorMessage, Exception innerException) : base(errorMessage, innerException)
        {
        }
    }

    /// <summary>
    /// Represents a remote platform service exception.
    /// </summary>
    /// <seealso cref="Microsoft.SfB.PlatformService.SDK.Common.PlatformServiceClientException" />
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
        /// Initializes a new instance of the RemotePlatformServiceException class with given <paramref name="errorMessage"/> and <paramref name="errorInformation"/>
        /// </summary>
        /// <param name="errorMessage">Message explaining the exception</param>
        /// <param name="errorInformation"><see cref="ErrorInformation"/> returned by PlatformService</param>
        internal RemotePlatformServiceException(string errorMessage, ErrorInformation errorInformation)
             : base(errorMessage, null)
        {
            this.ErrorInformation = errorInformation;
        }

        /// <summary>
        /// Initializes a new instance of the RemotePlatformServiceException class with given <paramref name="errorMessage"/>, 
        /// <paramref name="httpStatusCode"/>, <paramref name="loggingContext"/> and <paramref name="innerException"/>.
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

        #region Public properties
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

        #endregion

        /// <summary>
        /// Convert Http Response Message to Platform Service Exception.
        /// </summary>
        /// <param name="httpResponseMessage">The http response message.</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for logging all related events.</param>
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

        /// <summary>
        /// Creates the string representation of the <see cref="RemotePlatformServiceException"/> using <see cref="HttpStatusCode"/>, <see cref="PlatformServiceServiceUri"/> if applicable,
        /// <see cref="PlatformServiceCorrelationId"/> if applicable and <see cref="PlatformServiceFqdn"/> if applicable.
        /// </summary>
        /// <returns>the string representation of the <see cref="RemotePlatformServiceException"/></returns>
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

    /// <summary>
    /// A <see cref="PlatformServiceClientInvalidOperationException"/> is thrown when an invalid operation within platform service is executed.
    /// </summary>
    [Serializable]
    public class PlatformServiceClientInvalidOperationException : PlatformServiceClientException
    {
        internal PlatformServiceClientInvalidOperationException(string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
        {
        }
    }

    /// <summary>
    /// A <see cref="CapabilityNotAvailableException"/> is thrown when a capability is used while unavailable.
    /// </summary>
    [Serializable]
    public class CapabilityNotAvailableException : PlatformServiceClientInvalidOperationException
    {
        internal CapabilityNotAvailableException(string errorMessage, Exception innerException = null)
            : base(errorMessage, innerException)
        {
        }
    }
}
