using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace CallForwardSample
{
    public class CallForwardJob
    {
        private readonly SipUri m_inviteTargetUri;

        private readonly IAudioVideoInvitation m_incomingInvitation;

        private readonly LoggingContext m_loggingContext;

        public CallForwardJob(IncomingInviteEventArgs<IAudioVideoInvitation> incomingInvitation, string inviteTarget)
        {
            m_incomingInvitation = incomingInvitation.NewInvite;
            m_inviteTargetUri = new SipUri(inviteTarget);
            m_loggingContext = new LoggingContext(Guid.NewGuid().ToString(), string.Empty);
        }

        public void Start()
        {
            //Start async since we do not want to block the event handler thread
            StartCallForwardAsync().ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "StartCallForwardAsync failed with exception. Job id {0} ", m_loggingContext.JobId);
                    }
                }
                else
                {
                    Logger.Instance.Information("StartCallForwardAsync completed, Job id {0}", m_loggingContext.JobId);
                }
            }
            );
        }

        private async Task StartCallForwardAsync()
        {
            Logger.Instance.Information(string.Format("[StartCallForwardAsync] StartCallForwardAsync: LoggingContext: {0}", m_loggingContext));

            await m_incomingInvitation.ForwardAsync(m_inviteTargetUri, m_loggingContext).ConfigureAwait(false);
        }
    }
}
