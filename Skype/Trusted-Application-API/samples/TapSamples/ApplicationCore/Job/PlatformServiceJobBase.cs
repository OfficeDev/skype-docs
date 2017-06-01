using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    public abstract class PlatformServiceJobBase
    {
        public PlatformServiceJobInputBase JobInput { get; private set; }

        public string JobId { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public JobType JobType { get; set; }

        public string InstanceId { get; set; }

        protected LoggingContext LoggingContext { get; private set; }

        [JsonIgnore]
        public AzureBasedApplicationBase AzureApplication { get; private set; }

        public PlatformServiceJobBase(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, PlatformServiceJobInputBase input, JobType jobType)
        {
            JobId = jobId;
            InstanceId = instanceId;
            JobInput = input;
            JobType = jobType;
            AzureApplication = azureApplication;
            LoggingContext = new LoggingContext(JobId, InstanceId);
        }

        /// <summary>
        /// Get job meta data
        /// </summary>
        /// <returns></returns>
        private JobMetadata GetJobMetaData() {
            DateTime startTime = DateTime.UtcNow;
            var jobMetadata = new JobMetadata(this.JobType, this.JobId);
            jobMetadata.JobStatus = JobStatus.Running;
            jobMetadata.StartTime = startTime;
            return jobMetadata;
        }

        /// <summary>
        /// Execute job async
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            try
            {
                await this.ExecuteCoreAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, string.Format("[PlatformServiceJobBase] Job {0} on instance {1} failed", this.JobId, this.InstanceId));
                throw new PlatformserviceApplicationException(string.Format("Job {0} on instance {1} failed", this.JobId, this.InstanceId), ex);
            }
        }

        /// <summary>
        /// ExecuteCore  job async
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual Task ExecuteCoreAsync()
        {
            return TaskHelpers.CompletedTask;
        }

        public async Task ExecuteAndRecordAsync(IStorage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            var jobMetadata = this.GetJobMetaData();
            await storage.SaveJobMetadata(jobMetadata).ConfigureAwait(false);
            try
            {
                await this.ExecuteAsync().ConfigureAwait(false);
                jobMetadata.JobStatus = JobStatus.Success;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, string.Format("[PlatformServiceJobBase] {0} Job {1} on instance {1} failed", this.GetType().ToString(), this.JobId, this.InstanceId));
                jobMetadata.JobStatus = JobStatus.Failure;
            }

            jobMetadata.EndTime = DateTime.UtcNow;

            await storage.SaveJobMetadata(jobMetadata).ConfigureAwait(false);
        }

        /// <summary>
        /// ExecuteCore  job async and return result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual Task<T> ExecuteCoreWithResultAsync<T>() where T:class
        {
            return TaskHelpers.FromResult<T>(null);
        }

        public async Task<T> ExecuteWithResultAsync<T>() where T : class
        {
            var result = default(T);
            try
            {
                result = await this.ExecuteCoreWithResultAsync<T>().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(string.Format("[PlatformServiceJobBase] Job {0} on instance {1} failed", this.JobId, this.InstanceId));
                throw new PlatformserviceApplicationException(string.Format("Job {0} on instance {1} failed", this.JobId, this.InstanceId), ex);
            }
            return result;
        }

        public async Task<T> ExecuteWithResultAndRecordAsync<T>(IStorage storage) where T:class
        {
            T result = default(T);

            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            var jobMetadata = this.GetJobMetaData();
            await storage.SaveJobMetadata(jobMetadata).ConfigureAwait(false);
            try
            {
                result = await this.ExecuteWithResultAsync<T>().ConfigureAwait(false);
                jobMetadata.JobStatus = JobStatus.Success;
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, string.Format("[PlatformServiceJobBase] {0} Job {1} on instance {1} failed", this.GetType().ToString(),this.JobId, this.InstanceId));
                jobMetadata.JobStatus = JobStatus.Failure;
            }

            jobMetadata.EndTime = DateTime.UtcNow;

            await storage.SaveJobMetadata(jobMetadata).ConfigureAwait(false);
            return result;
        }
    }
}