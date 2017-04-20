using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    /// <summary>
    /// Catches the context of logging
    /// </summary>
    public class LoggingContext : ICloneable
    {
        /// <summary>
        /// Creates a new instance of <see cref="LoggingContext"/>
        /// </summary>
        public LoggingContext()
        {
            PropertyBag = new Dictionary<string, object>();
        }

        /// <summary>
        /// Creates a new instance of <see cref="LoggingContext"/> with given parameters
        /// </summary>
        /// <param name="jobId">ID of the job to which this <see cref="LoggingContext"/> belongs</param>
        /// <param name="instanceId">ID of the application instance to which this <see cref="LoggingContext"/> belongs</param>
        public LoggingContext(string jobId, string instanceId) : this()
        {
            this.JobId = jobId;
            this.InstanceId = instanceId;
        }

        /// <summary>
        /// Creates a new instance of <see cref="LoggingContext"/>
        /// </summary>
        /// <param name="trackingId">unique ID to identify this <see cref="LoggingContext"/></param>
        public LoggingContext(Guid trackingId) : this()
        {
            this.TrackingId = trackingId;
        }

        /// <summary>
        /// Gets the propertyBag
        /// </summary>
        [JsonProperty]
        public Dictionary<string, object> PropertyBag { get; private set; }

        /// <summary>
        /// Gets the Job Id.
        /// This could be a unique id for a session, for example , a visit Id
        /// </summary>
        [JsonProperty]
        public string JobId { get; private set; }

        /// <summary>
        /// Gets the Instance Id.
        /// </summary>
        [JsonProperty]
        public string InstanceId { get; private set; }

        /// <summary>
        /// Gets platform event received time.
        /// </summary>
        [JsonProperty]
        public DateTime? PlatformEventReceivedTime { get; private set; }

        /// <summary>
        /// Gets platform event's CorrelationId.
        /// </summary>
        [JsonProperty]
        public string PlatformEventsCorrelationId { get; private set; }

        /// <summary>
        /// Gets platform event's client request Id.
        /// </summary>
        [JsonProperty]
        public string PlatformEventsClientRequestId { get; private set; }

        /// <summary>
        /// Gets platform event's server FQDN.
        /// </summary>
        [JsonProperty]
        public string PlatformEventsServerFqdn { get; private set; }

        /// <summary>
        /// Gets platform response's CorrelationId.
        /// </summary>
        [JsonProperty]
        public string PlatformResponseCorrelationId { get; private set; }

        /// <summary>
        /// Gets platform response's server FQDN.
        /// </summary>
        [JsonProperty]
        public string PlatformResponseServerFqdn { get; private set; }

        /// <summary>
        /// The tracking Id
        /// </summary>
        [JsonProperty]
        public Guid TrackingId { get; }

        /// <summary>
        /// Get related tracing info from header.
        /// </summary>
        /// <param name="httpHeaders">HTTP headers from request or response.</param>
        /// <param name="isFromRequest">The header is from request or response.</param>
        public void FillTracingInfoFromHeaders(HttpHeaders httpHeaders, bool isFromRequest = true)
        {
            if (httpHeaders == null)
            {
                return;
            }

            var correlationId = HttpMessageHelper.GetHttpHeaderValue(httpHeaders, Constants.UcapMsCorrelationId);
            var serverFqdn = HttpMessageHelper.GetHttpHeaderValue(httpHeaders, Constants.UcapMsServerFqdn);
            var clientRequestId = HttpMessageHelper.GetHttpHeaderValue(httpHeaders, Constants.UcapClientRequestId);
            if (isFromRequest)
            {
                this.PlatformEventsCorrelationId = correlationId;
                this.PlatformEventsServerFqdn = serverFqdn;
                this.PlatformEventsClientRequestId = clientRequestId;
                this.PlatformEventReceivedTime = DateTime.UtcNow;
            }
            else
            {
                this.PlatformResponseCorrelationId = correlationId;
                this.PlatformResponseServerFqdn = serverFqdn;
            }
        }

        /// <summary>
        /// fill call back context related info
        /// </summary>
        /// <param name="callbackContext"></param>
        public void FillFromCallbackContext(CallbackContext callbackContext)
        {
            this.JobId = callbackContext.JobId;
            this.InstanceId = callbackContext.InstanceId;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("LoggingContext: \r\n");

            Type loggingContextType = this.GetType();
            foreach (PropertyInfo p in loggingContextType.GetProperties())
            {
                string propertyName = p.Name;
                object propertyValue = p.GetValue(this);
                sb.Append(propertyName + ":" + (propertyValue == null ? "NULL" : propertyValue.ToString()) + " \r\n");
            }

            foreach (string k in this.PropertyBag.Keys)
            {
                sb.Append(k + ":" + (this.PropertyBag[k] == null ? "NULL" : this.PropertyBag[k].ToString()) + " \r\n");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates a new shallow copy of <see cref="LoggingContext"/>
        /// </summary>
        /// <returns>A new <see cref="LoggingContext"/> with all properties same as this object</returns>
        public object Clone()
        {
            var loggingContext = this.MemberwiseClone() as LoggingContext;
            loggingContext.PropertyBag = new Dictionary<string, object>();
            foreach (var item in this.PropertyBag)
            {
                loggingContext.PropertyBag.Add(item.Key, item.Value);
            }

            return loggingContext;
        }
    }

    /// <summary>
    /// The callback context to attach to callback query parameter
    /// </summary>
    public class CallbackContext
    {
        /// <summary>
        /// Gets or sets the VisitId.
        /// </summary>
        public string JobId { get; set; }

        /// <summary>
        /// Get or set the instance id
        /// </summary>
        public string InstanceId { get; set; }
    }
}
