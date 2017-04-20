using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// A <see cref="PlatformServiceJobBase"/> job which listens for an incoming invitation. At one point of time, we can have only one
    /// job listening for incoming invitations. This class provides logic to ensure it.
    /// </summary>
    public abstract class PlatformServiceListeningJobBase : PlatformServiceJobBase
    {
        /// <summary>
        /// <see cref="PlatformServiceListeningJobBase"/> which is currently listening for incoming invitations.
        /// </summary>
        public static PlatformServiceListeningJobBase ListeningJob { get; private set; }

        /// <summary>
        /// The <see cref="lock"/> used to access <see cref="ListeningJob"/>;
        /// </summary>
        private static object syncRoot = new object();

        /// <summary>
        /// Creates an instance of <see cref="PlatformServiceListeningJobBase"/>.
        /// </summary>
        /// <param name="jobId">Unique ID of this job, it distinguishes the job from other jobs.</param>
        /// <param name="instanceId">ID of the service/process/webapp hosting all the jobs.</param>
        /// <param name="input"><see cref="PlatformServiceListeningJobBase"/> providing configuration for the job.</param>
        /// <param name="jobType"><see cref="JobType"/> specifying the type of this job.</param>
        public PlatformServiceListeningJobBase(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, PlatformServiceJobInputBase input, JobType jobType)
            : base(jobId, instanceId, azureApplication, input, jobType)
        {
        }

        /// <summary>
        /// Starts execution of the job.
        /// </summary>
        /// <returns>void; it is an <i>async</i> method.</returns>
        /// <exception cref="PlatformserviceApplicationException">If some other job is already listening for incoming invitations.</exception>
        public override Task ExecuteCoreAsync()
        {
            lock (syncRoot)
            {
                if (ListeningJob != null)
                {
                    throw new PlatformserviceApplicationException("A job with ID " + ListeningJob.JobId + " is already listening, please stop it before starting a new one.");
                }
                ListeningJob = this;
            }

            StartCore();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Provides an extension point for base jobs to start listening for incoming invitations.
        /// </summary>
        protected abstract void StartCore();

        /// <summary>
        /// Stops the job.
        /// </summary>
        public void Stop()
        {
            StopCore();
            lock (syncRoot)
            {
                ListeningJob = null;
            }
        }

        /// <summary>
        /// Provides an extension point for base to stop listening for incoming invitations.
        /// </summary>
        protected abstract void StopCore();
    }
}
