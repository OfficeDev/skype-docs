using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace AcceptAndBridgeIM
{
    public class StopIMBridgeController : ApiController
    {
        public HttpResponseMessage PostAsync(InstantMessagingBridgeJobInput input)
        {
            string jobId = Guid.NewGuid().ToString("N");

            InstantMessagingBridgeJobInput imbi = new InstantMessagingBridgeJobInput();
            imbi.IsStart = true;
            imbi.Subject = string.IsNullOrEmpty(input.Subject) ? "IMBridgeSample" : input.Subject;
            imbi.WelcomeMessage = string.IsNullOrEmpty(input.WelcomeMessage) ? "Welcome!!" : input.WelcomeMessage;
            imbi.InviteTargetUri = string.IsNullOrEmpty(input.InviteTargetUri) ? ConfigurationManager.AppSettings["MyAgent"] : input.InviteTargetUri;
            imbi.InvitedTargetDisplayName = string.IsNullOrEmpty(input.InvitedTargetDisplayName) ? "Agent" : imbi.InvitedTargetDisplayName;
            imbi.EnableMessageFilter = input.EnableMessageFilter;

            try
            {
                InstantMessagingBridgeJob job = new InstantMessagingBridgeJob(jobId, WebApiApplication.InstanceId, imbi);
                job.Stop();

                return Request.CreateResponse(HttpStatusCode.OK, "Stop listening incoming call");
            }

            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "Exception while scheduling job.");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Hit exception when running the job\"}");
            }
        }

        protected HttpResponseMessage CreateHttpResponse(HttpStatusCode statusCode, string message)
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
}
