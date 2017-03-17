using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace AcceptAndBridgeIM
{    
    public class StopIMBridgeController : ApiController
    {
        public async Task<HttpResponseMessage> PostAsync()
        {
            string jobId = Guid.NewGuid().ToString("N");


            try
            {

                InstantMessagingBridgeJobInput imbi = new InstantMessagingBridgeJobInput();
                imbi.IsStart = false;
                imbi.Subject = "Adhoc";
                imbi.WelcomeMessage = "Welcome!!!";
                imbi.InviteTargetUri = "sip:liben@metio.onmicrosoft.com";
                imbi.InvitedTargetDisplayName = "agent";
                imbi.EnableMessageFilter = false;

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
