using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    [MyCorsPolicy]
    public class IncomingMessagingBridgeJobController : JobControllerBase
    {
        public HttpResponseMessage Post(InstantMessagingBridgeJobInput input)
        {
            if (input == null || string.IsNullOrEmpty(input.InviteTargetUri))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid OutgoingMessagingNotifyInput\"}");
            }

            if (!input.InviteTargetUri.StartsWith("sip", StringComparison.InvariantCultureIgnoreCase))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid To\"}");
            }

            var jobConfig = new PlatformServiceSampleJobConfiguration
            {
                JobType = JobType.InstantMessagingBridge,
                InstantMessagingBridgeJobInput = input
            };
            string jobId = Guid.NewGuid().ToString("N");

            try
            {
                PlatformServiceJobBase job = PlatformServiceClientJobHelper.GetJob(jobId, WebApiApplication.InstanceId, WebApiApplication.AzureApplication, jobConfig);

                if (job == null)
                {
                    return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid job input or job type\"}");
                }

                job.ExecuteAsync().Observe<Exception>();
                return Request.CreateResponse(job);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e, "Exception happened in schedule job");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Unable to start a job run\"}");
            }
        }

        public HttpResponseMessage Get()
        {
            return CreateHttpResponse(HttpStatusCode.OK, "{\"Message\":\"Test Connection Success!\"}");
        }
    }
}
