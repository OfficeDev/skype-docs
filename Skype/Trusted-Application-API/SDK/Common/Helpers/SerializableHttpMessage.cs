using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Common
{
    public abstract class SerializableHttpMessage
    {
        public string RequestId { get; set; }
        public DateTime Timestamp { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public List<Tuple<string, string>> ContentHeaders { get; set; }
        public bool IsIncoming { get; set; }

        protected async Task<HttpContent> GetHttpContentForLogAsync()
        {
            HttpContent content = null;

            if (!string.IsNullOrEmpty(this.Content))
            {
                content = new StringContent(this.Content);
                await content.LoadIntoBufferAsync().ConfigureAwait(false);

                if (this.ContentHeaders != null)
                {
                    try
                    {
                        foreach (Tuple<string, string> header in this.ContentHeaders)
                        {
                            if (string.Equals(header.Item1, "Content-Type", StringComparison.OrdinalIgnoreCase))
                            {
                                content.Headers.ContentType = this.GetContentTypeHeaderValue(header.Item2);
                            }
                            else if (string.Equals(header.Item1, "Content-Length", StringComparison.OrdinalIgnoreCase))
                            {
                                //we can just ignore it; it will be computed anyways
                            }
                            else
                            {
                                content.Headers.Add(header.Item1, header.Item2);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // TODO : Replace this catch block with something more specific
                        Logger.Instance.Warning(ex, "Exception of type" + ex.GetType());
                    }
                }
            }

            return content;
        }

        private MediaTypeHeaderValue GetContentTypeHeaderValue(string headerValue)
        {
            MediaTypeHeaderValue newContentType = null;
            MediaTypeHeaderValue.TryParse(headerValue, out newContentType);

            return newContentType;
        }
    }

    /// <summary>
    /// The class for seriazlied http request message
    /// </summary>
    public class SerializableHttpRequestMessage : SerializableHttpMessage
    {
        #region properties/fields
        public HttpMethod Method { get; set; }
        public string Uri { get; set; }
        public List<Tuple<string, string>> RequestHeaders { get; set; }
        public LoggingContext LoggingContext { get; set; }

        public SerializableHttpRequestMessage()
        {
        }

        /// <summary>
        /// InitializeFromHttpContent
        /// </summary>
        /// <param name="RequestUri"></param>
        /// <param name="content"></param>
        /// <param name="method"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public async Task InitializeAsync(HttpRequestMessage requestMessage, string requestId, bool isIncoming = true)
        {
            this.Timestamp = DateTime.Now;
            this.RequestId = requestId;
            this.Method = requestMessage.Method;
            this.Uri = requestMessage.RequestUri.ToString();
            this.IsIncoming = isIncoming;
            if (requestMessage.Content != null)
            {
                if (requestMessage.Content.Headers?.ContentType != null)
                {
                    this.ContentType = requestMessage.Content.Headers.ContentType.ToString();
                }

                await requestMessage.Content.LoadIntoBufferAsync().ConfigureAwait(false);

                if (requestMessage.Content.Headers != null)
                {
                    this.ContentHeaders = new List<Tuple<string, string>>();

                    foreach (KeyValuePair<string, IEnumerable<string>> header in requestMessage.Content.Headers)
                    {
                        foreach (string value in header.Value)
                        {
                            this.ContentHeaders.Add(new Tuple<string, string>(header.Key, value));
                        }
                    }
                }
                this.Content = await requestMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            if (requestMessage.Headers != null)
            {
                this.RequestHeaders = new List<Tuple<string, string>>();

                foreach (KeyValuePair<string, IEnumerable<string>> header in requestMessage.Headers)
                {
                    foreach (string value in header.Value)
                    {
                        this.RequestHeaders.Add(new Tuple<string, string>(header.Key, value));
                    }
                }
            }
        }

        public async Task<string> GetLogStringAsync()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (this.IsIncoming)
                {
                    sb.AppendLine(Constants.NotifierInboundHttpRequestStart);
                }
                else
                {
                    sb.AppendLine(Constants.TransportOutboundHttpRequestStart);
                }

                AppendRequestInfo(sb);

                HttpContent content = await this.GetHttpContentForLogAsync().ConfigureAwait(false);

                if (content != null)
                {
                    await HttpMessageHelper.FormatHttpContentAsync(sb, content).ConfigureAwait(false);
                }

                if (this.IsIncoming)
                {
                    sb.AppendLine(Constants.NotifierInboundHttpRequestEnd);
                }
                else
                {
                    sb.AppendLine(Constants.TransportOutboundHttpRequestEnd);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Serializing http request failed.  Ex: " + ex.ToString());
            }

            return sb.ToString();
        }

        private void AppendRequestInfo(StringBuilder sb)
        {
            if (this.IsIncoming)
            {
                sb.Append("<<<<<  ");
            }
            else
            {
                sb.Append(">>>>>  ");
            }
            sb.AppendLine(string.Format("{0} {1}", this.Method.ToString(), this.Uri));
            sb.AppendLine("TimeStamp: " + this.Timestamp.ToShortTimeString());
            if (LoggingContext?.JobId != null)
            {
                sb.AppendLine("JobId: " + this.LoggingContext.JobId);
            }

            if (LoggingContext?.InstanceId != null)
            {
                sb.AppendLine("InstanceId: " + this.LoggingContext.InstanceId);
            }

            sb.AppendLine("RequestId: " + this.RequestId);
            sb.AppendLine();

            if (this.RequestHeaders != null)
            {
                foreach (var header in this.RequestHeaders)
                {
                    sb.AppendLine(string.Format("{0} : {1}", header.Item1, header.Item2));
                }
            }
        }
    }

    /// <summary>
    /// Wrapper class httprequestmessage for serialization purposes.
    /// This is because httprequestmessage and responsemessage are not serializable.
    /// </summary>
    public class SerializableHttpResponseMessage : SerializableHttpMessage
    {
        #region properties/fields
        public HttpStatusCode StatusCode { get; set; }

        public List<Tuple<string, string>> ResponseHeaders { get; set; }
        #endregion

        /// <summary>
        /// .ctor
        /// </summary>
        public SerializableHttpResponseMessage()
        {
        }

        public async Task InitializeAsync(HttpResponseMessage responseMessage, string requestId, bool isIncoming = true)
        {
            this.Timestamp = DateTime.Now;
            this.RequestId = requestId;
            this.StatusCode = responseMessage.StatusCode;
            this.IsIncoming = isIncoming;

            if (responseMessage.Content != null)
            {
                await responseMessage.Content.LoadIntoBufferAsync().ConfigureAwait(false);

                if (responseMessage.Content.Headers != null)
                {
                    this.ContentHeaders = new List<Tuple<string, string>>();

                    foreach (var header in responseMessage.Content.Headers)
                    {
                        foreach (var value in header.Value)
                        {
                            this.ContentHeaders.Add(new Tuple<string, string>(header.Key, value));
                        }
                    }
                }

                this.Content = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (responseMessage.Headers != null)
                {
                    this.ResponseHeaders = new List<Tuple<string, string>>();

                    foreach (KeyValuePair<string, IEnumerable<string>> header in responseMessage.Headers)
                    {
                        foreach (string value in header.Value)
                        {
                            this.ResponseHeaders.Add(new Tuple<string, string>(header.Key, value));
                        }
                    }
                }
            }
        }

        public async Task<string> GetLogStringAsync()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (this.IsIncoming)
                {
                    sb.AppendLine(Constants.NotifierInboundHttpRequestResponseStart);
                }
                else
                {
                    sb.AppendLine(Constants.TransportOutboundHttpRequestResponseStart);
                }

                this.AppendResponseInfo(sb);

                HttpContent content = await this.GetHttpContentForLogAsync().ConfigureAwait(false);
                if (content != null)
                {
                    await HttpMessageHelper.FormatHttpContentAsync(sb, content).ConfigureAwait(false);
                }

                sb.AppendLine();
                if (this.IsIncoming)
                {
                    sb.AppendLine(Constants.NotifierInboundHttpRequestResponseEnd);
                }
                else
                {
                    sb.AppendLine(Constants.TransportOutboundHttpRequestResponseEnd);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.Error("Serializing http response failed.  Ex: " + ex.ToString());
            }

            return sb.ToString();
        }

        private void AppendResponseInfo(StringBuilder sb)
        {
            sb.AppendLine(":Id= " + this.RequestId);
            sb.Append(this.StatusCode);
            sb.AppendLine("TimeStamp: " + this.Timestamp.ToShortTimeString());
            if (this.ResponseHeaders != null)
            {
                foreach (var header in this.ResponseHeaders)
                {
                    sb.AppendLine(string.Format("{0} : {1}", header.Item1, header.Item2));
                }
            }
        }
    }
}
#endregion