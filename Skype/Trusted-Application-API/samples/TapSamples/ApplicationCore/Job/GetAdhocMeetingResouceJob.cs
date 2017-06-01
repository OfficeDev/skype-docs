using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
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
            LoggingContext loggingContext = new LoggingContext(this.JobId, this.InstanceId);
            Logger.Instance.Information(string.Format("[GetAdhoc meeting job] stared: LoggingContext: {0}", loggingContext.JobId));

            try
            {
                GetAdhocMeetingResourceInput getAnonTokenInput = this.JobInput as GetAdhocMeetingResourceInput;
                if (getAnonTokenInput == null)
                {
                    throw new InvalidOperationException("Failed to get valid AdhocMeetingInput intance");
                }
                                
                AdhocMeetingInput adhocinput = new AdhocMeetingInput()
                {
                    AccessLevel = AccessLevel.Everyone,
                    EntryExitAnnouncement = EntryExitAnnouncement.Disabled,
                    AutomaticLeaderAssignment = AutomaticLeaderAssignment.Disabled,
                    Subject = getAnonTokenInput.Subject,
                    LobbyBypassForPhoneUsers = LobbyBypassForPhoneUsers.Disabled,
                    PhoneUserAdmission = PhoneUserAdmission.Disabled,
                    Description = getAnonTokenInput.Description
                };

                var adhocmeetingResources = await AzureApplication.ApplicationEndpoint.Application.GetAdhocMeetingResourceAsync(loggingContext, adhocinput);

                if (adhocmeetingResources != null)
                {
                    result = new AdhocMeetingToken
                    {
                        DiscoverUri = adhocmeetingResources.DiscoverLink.Href,
                        ExpireTime = adhocmeetingResources.ExpirationTime,
                        JoinUrl = adhocmeetingResources.JoinUrl,
                        OnlineMeetingUri = adhocmeetingResources.OnlineMeetingUri,
                        OrganizerUri = adhocmeetingResources.OrganizerUri
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