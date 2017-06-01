using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    [MyCorsPolicy]
    public class GetAnonTokenJobController : JobControllerBase
    {
        public async Task<HttpResponseMessage> PostAsync(GetAnonTokenInput input)
        {
            if (string.IsNullOrEmpty(input.ApplicationSessionId))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"No or invalid callback context specified!\"}");
            }

            if (string.IsNullOrEmpty(input.AllowedOrigins))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid AllowedOrigins\"}");
            }

            string jobId = Guid.NewGuid().ToString("N");

            try
            {
                PlatformServiceSampleJobConfiguration jobConfig = new PlatformServiceSampleJobConfiguration
                {
                    JobType = JobType.GetAnonToken,
                    AnonTokenJobInput = input
                };
                var job = PlatformServiceClientJobHelper.GetJob(jobId, WebApiApplication.InstanceId, WebApiApplication.AzureApplication, jobConfig) as GetAnonTokenJob;

                if (job == null)
                {
                    return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid job input or job type\"}");
                }

                AnonymousToken token = await job.ExecuteWithResultAndRecordAsync<AnonymousToken>(Storage).ConfigureAwait(false);
                if (token == null)
                {
                    return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Returned token is null\"}");
                }

                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "Exception while scheduling job.");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Hit exception when running the job\"}");
            }
        }
    }
}
