using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    [MyCorsPolicy]
    public class HuntGroupJobController : JobControllerBase
    {
        public HttpResponseMessage Post(HuntGroupJobInput input)
        {
            if (input == null /* || input.InviteTargetUris == null || input.InviteTargetUris.Length < 1 */)
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid HuntGroupJobInput\"}");
            }

            if (input.InviteTargetUris != null)
            {
                foreach (string s in input.InviteTargetUris)
                {
                    if (!s.StartsWith("sip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid InviteTargetUri\"}");
                    }
                }
            }

            var jobConfig = new PlatformServiceSampleJobConfiguration
            {
                JobType = JobType.HuntGroup,
                HuntGroupJobInput = input
            };

            string jobId = Guid.NewGuid().ToString("N");

            try
            {
                PlatformServiceJobBase job = PlatformServiceClientJobHelper.GetJob(jobId, WebApiApplication.InstanceId, WebApiApplication.AzureApplication, jobConfig);

                if (job == null)
                {
                    return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Unsupported job input or job type\"}");
                }

                job.ExecuteAsync().Observe<Exception>();
                return Request.CreateResponse(HttpStatusCode.Created, job);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e, "Exception while scheduling job");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Unable to start a job run\"}");
            }
        }

        public HttpResponseMessage Get()
        {
            return CreateHttpResponse(HttpStatusCode.OK, "{\"Message\":\"Test Connection Success!\"}");
        }
    }
}
