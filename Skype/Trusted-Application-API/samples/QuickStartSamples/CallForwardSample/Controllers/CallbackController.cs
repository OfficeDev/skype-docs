using Newtonsoft.Json;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http.Headers;

namespace CallForwardSample.Common
{
    [LoggingContextActionFilter(QueryParameterName = "callbackContext")]
    [Authorize]
    public class CallbackController : ApiController
    {
        private readonly SimpleEventChannel m_eventChannel;

        public CallbackController()
        {
            m_eventChannel = UnityHelper.Resolve<SimpleEventChannel>();
        }

        public HttpResponseMessage Get()
        {
            var loggingContext = this.Request.Properties[Constants.LoggingContext] as LoggingContext;

            return CreateHttpResponse(HttpStatusCode.OK, "{\"Message\":\"Test connection successful.!\"}");
        }

        public async Task<HttpResponseMessage> PostAsync()
        {
            Logger.Instance.Information("AuthHeader: " + this.Request.Headers.Authorization.Parameter);
            var httpmessage = new SerializableHttpRequestMessage();
            CallbackContext callbackContext = null;
            var loggingContext = this.Request.Properties[Constants.LoggingContext] as LoggingContext;
            bool isIncomingNewInvitation = !this.Request.Properties.ContainsKey(Constants.CallbackContext);

            if (!isIncomingNewInvitation)
            {
                callbackContext = this.Request.Properties[Constants.CallbackContext] as CallbackContext;
            }
            else
            {
                //support incoming invite
                Logger.Instance.Information("New incoming invite get, process locally!");
                callbackContext = new CallbackContext();
            }

            try
            {
                await httpmessage.InitializeAsync(this.Request, loggingContext.TrackingId.ToString(), true).ConfigureAwait(false);
                httpmessage.LoggingContext = loggingContext;
                m_eventChannel.ProcessCallbackMessage(httpmessage);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "Fail to parse incoming http message");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Internal error occured.!\"}");
            }

            //This is NOT the initial incoming INVITE message.
            if (isIncomingNewInvitation)
            {
                var responseBody = new EventResponse()
                {
                    CallbackContext = JsonConvert.SerializeObject(callbackContext)
                };

                string jsonContent = JsonConvert.SerializeObject(responseBody);
                return CreateHttpResponse(HttpStatusCode.OK, jsonContent);
            }
            else
            {
                return CreateHttpResponse(HttpStatusCode.OK);
            }
        }

        private HttpResponseMessage CreateHttpResponse(HttpStatusCode statusCode, string message = null)
        {
            var response = new HttpResponseMessage(statusCode);
            if (message != null)
            {
                response.Content = new StringContent(message);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }
            return response;
        }
    }

    public class EventResponse
    {
        public string CallbackContext { get; set; }
    }
}
