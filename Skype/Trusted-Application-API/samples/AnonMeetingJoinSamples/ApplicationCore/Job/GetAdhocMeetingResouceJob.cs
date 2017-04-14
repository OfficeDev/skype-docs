using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public class GetAdhocMeetingResouceJob : PlatformServiceJobBase
    {
        public GetAdhocMeetingResouceJob(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, GetAdhocMeetingResourceInput input)
            : base(jobId, instanceId, azureApplication, input, JobType.GetAnonToken)
        {
        }

        public override async Task<T> ExecuteCoreWithResultAsync<T>()
        {
            AdhocMeetingToken result = null;
            var loggingContext = new LoggingContext(this.JobId, this.InstanceId);
            Logger.Instance.Information(string.Format("[GetAdhoc meeting job] stared: LoggingContext: {0}", loggingContext.JobId));

            try
            {
                var getAnonTokenInput = this.JobInput as GetAdhocMeetingResourceInput;
                if (getAnonTokenInput == null)
                {
                    throw new InvalidOperationException("Failed to get valid AdhocMeetingInput intance");
                }

                var adhocinput = new AdhocMeetingCreationInput(getAnonTokenInput.Subject);

                var adhocmeetingResources = await AzureApplication.ApplicationEndpoint.Application.CreateAdhocMeetingAsync(adhocinput, loggingContext).ConfigureAwait(false);

                if (adhocmeetingResources != null)
                {
                    result = new AdhocMeetingToken
                    {
                        JoinUrl = adhocmeetingResources.JoinUrl,
                        OnlineMeetingUri = adhocmeetingResources.OnlineMeetingUri
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get anon token and discover url " + ex.Message);
            }

            return result as T;
        }
    }
}