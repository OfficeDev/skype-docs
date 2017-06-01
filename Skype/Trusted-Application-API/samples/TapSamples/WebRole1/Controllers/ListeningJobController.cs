using System.Net;
using System.Net.Http;
using Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore;

namespace Microsoft.SfB.PlatformService.SDK.Samples.FrontEnd
{
    /// <summary>
    /// Controller to handle jobs which are currently listening for incoming invitations.
    /// </summary>
    /// <remarks>At one point of time, we can have only one job listening.</remarks>
    [MyCorsPolicy]
    public class ListeningJobController : JobControllerBase
    {
        /// <summary>
        /// Get the currently running job.
        /// </summary>
        /// <returns>Currently running job details or a <code>404</code>, if no job is currently listening.</returns>
        public HttpResponseMessage Get()
        {
            var listeningJob = PlatformServiceListeningJobBase.ListeningJob;
            if (listeningJob == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "{\"Error\":\"No listening job present at the moment\"}");
            }

            return Request.CreateResponse(HttpStatusCode.OK, listeningJob);
        }

        /// <summary>
        /// Stops the currently running job.
        /// </summary>
        /// <returns><code>204</code> in case of success or a <code>404</code>, if no job is currently listening.</returns>
        public HttpResponseMessage Delete()
        {
            var listeningJob = PlatformServiceListeningJobBase.ListeningJob;
            if (listeningJob == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "{\"Error\":\"No listening job present at the moment\"}");
            }

            listeningJob.Stop();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}