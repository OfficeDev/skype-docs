using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// Define the job status
    /// </summary>
    public class JobStatus
    {
        public const string Enqueued = "Enqueued";
        public const string Running = "Running";
        public const string Success = "Success";
        public const string Failure = "Failure";
        public const string Aborted = "Aborted";
    }

    /// <summary>
    /// Define job metadata
    /// </summary>
    public class JobMetadata : TableEntity
    {
        private JobType m_jobType;

        public JobMetadata(JobType jobType, string jobId)
        {
            m_jobType = jobType;
            this.PartitionKey = jobType.ToString();
            this.RowKey = jobId;
        }

        public JobMetadata()
        {
        }

        public JobType JobType
        {
            get { return m_jobType; }
        }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string JobStatus { get; set; }
    }
}