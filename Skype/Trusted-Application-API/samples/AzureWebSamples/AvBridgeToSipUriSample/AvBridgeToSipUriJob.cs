using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace AVBridgeToSipUriSample
{
    public class AVBridgeToSipUriJob
    {
        private readonly SipUri m_inviteTargetUri;

        private readonly IAudioVideoInvitation m_incomingInvitation;

        private readonly LoggingContext m_loggingContext;

        public AVBridgeToSipUriJob(IncomingInviteEventArgs<IAudioVideoInvitation> incomingInvitation, string inviteTarget)
        {
            m_incomingInvitation = incomingInvitation.NewInvite;
            m_inviteTargetUri = new SipUri(inviteTarget);
            m_loggingContext = new LoggingContext(Guid.NewGuid().ToString(), string.Empty);
        }

        public void Start()
        {
            //Start async since we do not want to block the event handler thread
            StartAVBridgeFlowAsync().ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "StartHuntGroupFlow failed with exception. Job id {0} ", m_loggingContext.JobId);
                    }
                }
                else
                {
                    Logger.Instance.Information("StartAVBridgeFlowAsync completed, Job id {0}", m_loggingContext.JobId);
                }
            }
            );
        }

        private async Task StartAVBridgeFlowAsync()
        {
            Logger.Instance.Information(string.Format("[StartAVBridgeFlowAsync] StartAVBridgeFlowAsync: LoggingContext: {0}", m_loggingContext));

            // Start AcceptAndBridge to the agent
            await m_incomingInvitation.AcceptAndBridgeAsync(m_inviteTargetUri, m_loggingContext).ConfigureAwait(false);
            await m_incomingInvitation.WaitForInviteCompleteAsync().ConfigureAwait(false);
        }
    }
}
