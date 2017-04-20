using Microsoft.SfB.PlatformService.SDK.Common;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public class PlatformServiceClientJobHelper
    {
        public static PlatformServiceJobBase GetJob(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, PlatformServiceSampleJobConfiguration jobConfig)
        {
            PlatformServiceJobBase returnJob = null;
            switch (jobConfig.JobType)
            {
                case JobType.SimpleNotification:
                    {
                        if (jobConfig.SimpleNotifyJobInput == null)
                        {
                            Logger.Instance.Error("[PlatformServiceClientJobHelper] NULL for SimpleNotifyJobInput when job type is JobType.SimpleNotification!");
                            return null;
                        }
                        returnJob = new SimpleNotifyJob(jobId, instanceId, azureApplication, jobConfig.SimpleNotifyJobInput);
                        break;
                    }
                case JobType.GetAnonToken:
                    {
                        if (jobConfig.AnonTokenJobInput == null)
                        {
                            Logger.Instance.Error("[PlatformServiceClientJobHelper] NULL for GetAnonTokenJob when job type is JobType.ImBridge!");
                            return null;
                        }
                        returnJob = new GetAnonTokenJob(jobId, instanceId, azureApplication, jobConfig.AnonTokenJobInput);
                        break;
                    }
                case JobType.InstantMessagingBridge:
                    {
                        if (jobConfig.InstantMessagingBridgeJobInput == null)
                        {
                            Logger.Instance.Error("[PlatformServiceClientJobHelper] NULL for InstantMessagingBridgeJobInput when job type is JobType.InstantMessagingBridge!");
                            return null;
                        }
                        returnJob = new InstantMessagingBridgeJob(jobId, instanceId, azureApplication, jobConfig.InstantMessagingBridgeJobInput);
                        break;
                    }
                case JobType.AudioVideoIVR:
                    {
                        if (jobConfig.AudioVideoIVRJobInput == null)
                        {
                            Logger.Instance.Error("[PlatformServiceClientJobHelper] NULL for AudioVideoJobInput when job type is JobType.AudioVideoForwardOrIVR!");
                            return null;
                        }
                        returnJob = new AudioVideoIVRJob(jobId, instanceId, azureApplication, jobConfig.AudioVideoIVRJobInput);
                        break;
                    }
                case JobType.HuntGroup:
                    {
                        if (jobConfig.HuntGroupJobInput == null)
                        {
                            Logger.Instance.Error("[PlatformServiceClientJobHelper] NULL for HuntGroupJobInput when job type is JobType.HuntGroupJobInput!");
                            return null;
                        }
                        returnJob = new HuntGroupJob(jobId, instanceId, azureApplication, jobConfig.HuntGroupJobInput);
                        break;
                    }
                case JobType.AdhocMeeting:
                    {
                        if (jobConfig.GetAdhocMeetingResourceInput == null)
                        {
                            Logger.Instance.Error("[PlatformServiceClientJobHelper] NULL for GetAdhocMeetingResourceInput when job type is JobType.AdhocMeeting!");
                            return null;
                        }
                        returnJob = new GetAdhocMeetingResouceJob(jobId, instanceId, azureApplication, jobConfig.GetAdhocMeetingResourceInput);
                        break;
                    }
                default:
                    break;
            }
            return returnJob;
        }
    }
}
