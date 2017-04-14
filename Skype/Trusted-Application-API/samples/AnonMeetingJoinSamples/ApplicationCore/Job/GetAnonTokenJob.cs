using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public class GetAnonTokenJob : PlatformServiceJobBase
    {
        public GetAnonTokenJob(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, GetAnonTokenInput input)
            : base(jobId, instanceId, azureApplication, input, JobType.GetAnonToken)
        {
        }

        public override async Task<T> ExecuteCoreWithResultAsync<T>()
        {
            AnonymousToken result = null;
            Logger.Instance.Information(string.Format("[GetAnonTokenJob] stared: LoggingContext: {0}", LoggingContext.JobId));

            try
            {
                var getAnonTokenInput = this.JobInput as GetAnonTokenInput;
                if (getAnonTokenInput == null)
                {
                    throw new InvalidOperationException("Failed to get valid GetAnonTokenInput intance");
                }

                var tokenResources = await AzureApplication.ApplicationEndpoint.Application.GetAnonApplicationTokenForMeetingAsync(
                    getAnonTokenInput.MeetingUrl,
                    getAnonTokenInput.AllowedOrigins,
                    getAnonTokenInput.ApplicationSessionId,
                    LoggingContext).ConfigureAwait(false);

                if (tokenResources != null)
                {
                    result = new AnonymousToken
                    {
                        DiscoverUri = tokenResources.AnonymousApplicationsDiscoverUri.ToString(),
                        ExpireTime = tokenResources.AuthTokenExpiryTime,
                        Token = tokenResources.AuthToken,
                        TenantEndpointId = AzureApplication.ApplicationEndpoint.ApplicationEndpointId.ToString()
                    };
                }
            }
            catch (CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("Failed to get anon token.", ex);
                throw;
            }
            catch (RemotePlatformServiceException ex)
            {
                Logger.Instance.Error("Failed to get anon token.", ex);
                throw;
            }

            return result as T;
        }
    }
}