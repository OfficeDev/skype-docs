using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Net.Http;
using System.Net;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    public class SimpleNotifyJobController : JobControllerBase
    {
        [MyCorsPolicy]
        public HttpResponseMessage Post(SimpleNotifyJobInput input)
        {
            if (input == null || string.IsNullOrEmpty(input.TargetUri) || string.IsNullOrEmpty(input.NotificationMessage))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid OutgoingMessagingNotifyInput\"}");
            }

            if (!input.TargetUri.StartsWith("sip", StringComparison.InvariantCultureIgnoreCase))
            {
                return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid To\"}");
            }

            string jobId = Guid.NewGuid().ToString("N");
            try
            {
                //New job and run asyncronizedly
                var jobConfig = new PlatformServiceSampleJobConfiguration
                {
                    JobType = JobType.SimpleNotification,
                    SimpleNotifyJobInput = input
                };
                PlatformServiceJobBase job = PlatformServiceClientJobHelper.GetJob(jobId, WebApiApplication.InstanceId, WebApiApplication.AzureApplication, jobConfig);

                if (job == null)
                {
                    return CreateHttpResponse(HttpStatusCode.BadRequest, "{\"Error\":\"Invalid job input or job type\"}");
                }

                job.ExecuteAndRecordAsync(Storage).Observe<Exception>();

                return Request.CreateResponse(HttpStatusCode.Created, job);
            }
            catch (Exception e)
            {
                Logger.Instance.Error(e, "Exception happened in schedule job");
                return CreateHttpResponse(HttpStatusCode.InternalServerError, "{\"Error\":\"Unable to start a job run\"}");
            }
        }
    }
}
