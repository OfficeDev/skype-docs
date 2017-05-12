using Newtonsoft.Json;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Http.Headers;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    [LoggingContextActionFilter(QueryParameterName = "callbackContext")]
    // [AADAppAuthorize(Constants.SkypeForBusinessApplicationId)]
    public class CallbackController : ApiController
    {
        private ICallbackMessageQueueManager m_callbackQueue;

        public CallbackController()
        {
            m_callbackQueue = IOCHelper.Resolve<ICallbackMessageQueueManager>();
        }

        public HttpResponseMessage Get()
        {
            var loggingContext = this.Request.Properties[Constants.LoggingContext] as LoggingContext;

            return CreateHttpResponse(HttpStatusCode.OK, loggingContext, "{\"Message\":\"Test connection successful.!\"}");
        }

        public async Task<HttpResponseMessage> PostAsync()
        {
            var httpmessage = new SerializableHttpRequestMessage();
            string serializedMessage = null;
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
                callbackContext.InstanceId = WebApiApplication.InstanceId;
            }

            try
            {
                await httpmessage.InitializeAsync(this.Request, loggingContext.TrackingId.ToString(), true).ConfigureAwait(false);
                httpmessage.LoggingContext = loggingContext;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "Fail to parse incoming http message");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, loggingContext, "{\"Error\":\"Internal error occured.!\"}");
            }

            if (callbackContext.InstanceId == null)
            {
                //In future, we need to support incoming invite, and should not return error here
                return CreateHttpResponse(HttpStatusCode.BadRequest, loggingContext, "{\"Error\":\"No or invalid callback context specified!\"}");
            }
            else
            {
                serializedMessage = JsonConvert.SerializeObject(httpmessage);
                try
                {
                    await m_callbackQueue.SaveCallbackMessageAsync(callbackContext.InstanceId, serializedMessage).ConfigureAwait(false);

                    //This is NOT the initial incoming INVITE message.
                    if (isIncomingNewInvitation)
                    {
                        EventResponse responseBody = new EventResponse()
                        {
                            CallbackContext = JsonConvert.SerializeObject(callbackContext)
                        };

                        string jsonContent = JsonConvert.SerializeObject(responseBody);
                        return CreateHttpResponse(HttpStatusCode.OK, loggingContext, jsonContent);
                    }
                    else
                    {
                        return CreateHttpResponse(HttpStatusCode.OK, loggingContext);
                    }
                }
                catch (PlatformServiceAzureStorageException)
                {
                    return CreateHttpResponse(HttpStatusCode.InternalServerError, loggingContext);
                }
            }
        }

        private HttpResponseMessage CreateHttpResponse(HttpStatusCode statusCode, LoggingContext loggingContext, string message = null)
        {
            HttpResponseMessage response = new HttpResponseMessage(statusCode);
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
