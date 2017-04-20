using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Configuration.ConfigurationHelpers;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.Rtc.Internal.Platform.ResourceContract;

namespace AudioVideoIVRSample
{
    public class AudioVideoIVRJob
    {
        private readonly Uri m_callbackUri;
             
        private readonly IncomingInviteEventArgs<IAudioVideoInvitation> m_incomingInvitation;

        private IConversation m_pstnCallConversation;

        private readonly string m_jobId;

        private readonly LoggingContext m_loggingContext;

        private bool m_isPromptPlaying;

        /// <summary>
        /// Actions which can be taken by an <see cref="AudioVideoIVRJob"/> in response to an incoming call or a tone event.
        /// </summary>
        public enum AudioVideoIVRAction
        {
            /// <summary>
            /// Play a prompt
            /// </summary>
            /// <remarks>NoOp if Prompt is null</remarks>
            PlayMainPrompt = ToneValue.Tone0,

            /// <summary>
            /// Play other prompt, it is like switching to other prompt
            /// </summary>
            PlayPromptOne,

            /// <summary>
            /// Play another prompt, it is like switching to other prompt
            /// </summary>
            PlayPromptTwo,

            /// <summary>
            /// Repeat the prompt
            /// </summary>
            RepeatPrompt,

            /// <summary>
            /// Terminate the call
            /// </summary>
            TerminateCall
        }

        private Dictionary<AudioVideoIVRAction, string> promptMap = new Dictionary<AudioVideoIVRAction, string>()
        {
            { AudioVideoIVRAction.PlayMainPrompt,  "prompt.wav"},
            { AudioVideoIVRAction.PlayPromptOne,  "promptOne.wav" },
            { AudioVideoIVRAction.PlayPromptTwo,  "promptTwo.wav" },
            { AudioVideoIVRAction.RepeatPrompt,  "prompt.wav" }
        };


        public AudioVideoIVRJob(IncomingInviteEventArgs<IAudioVideoInvitation> mIncomingInvitation, string callbackUri)
        {
            this.m_incomingInvitation = mIncomingInvitation;
            this.m_callbackUri = new Uri(callbackUri);
            m_jobId = Guid.NewGuid().ToString();
            m_loggingContext = new LoggingContext(m_jobId, string.Empty);
            m_isPromptPlaying = false;
        }

        public void Start()
        {
            //Start async since we do not want to block the event handler thread
            StartAudioVideoIVRFlowAsync(m_incomingInvitation).ContinueWith(p =>
            {
                if (p.IsFaulted)
                {
                    if (p.Exception != null)
                    {
                        Exception baseException = p.Exception.GetBaseException();
                        Logger.Instance.Error(baseException, "AudioVideoIVRJob failed with exception. Job id {0} ", m_jobId);
                    }
                }
                else
                {
                    Logger.Instance.Information("AudioVideoIVRJob completed, Job id {0}", m_jobId);
                }
            }
            );
        }



        private async Task StartAudioVideoIVRFlowAsync(IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            Logger.Instance.Information(string.Format("[StartAudioVideoIVRFlowAsync]: LoggingContext: {0}", m_loggingContext));

            m_pstnCallConversation = null;

            // Step1: accept incoming call
            Logger.Instance.Information(string.Format("[StartAudioVideoIVRFlowAsync] Step 1: Accept incoming av call: LoggingContext: {0}", m_loggingContext));
            await e.NewInvite.AcceptAsync(m_loggingContext).ConfigureAwait(false);
            await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);

            // if everything is fine, you will be able to get the related conversation
            m_pstnCallConversation = e.NewInvite.RelatedConversation;
            m_pstnCallConversation.HandleResourceRemoved += HandlePSTNCallConversationRemoved;

            // Step 2 : wait AV flow connected and play Promt
            IAudioVideoCall pstnAv = m_pstnCallConversation.AudioVideoCall;
            IAudioVideoFlow pstnFlow = await pstnAv.WaitForAVFlowConnected().ConfigureAwait(false);
            pstnFlow.ToneReceivedEvent += ToneReceivedEvent;
            // Step 3 : play prompt
            await PlayPromptAsync(pstnFlow, AudioVideoIVRAction.PlayMainPrompt).ConfigureAwait(false);

        }


        /// <summary>
        /// Method to be invoked when user presses a key on the dial pad.
        /// </summary>
        /// <param name="sender"><see cref="AudioVideoFlow"/> which received the event.</param>
        /// <param name="e"><see cref="ToneReceivedEventArgs"/> containing information about the event.</param>
        private void ToneReceivedEvent(object sender, ToneReceivedEventArgs e)
        {
            HandleToneEventAsync(sender as IAudioVideoFlow, e).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Logger.Instance.Warning("[AudioVideoIVRJob] Exception while processing tone received event.", t.Exception);
                }
                else
                {
                    Logger.Instance.Information("[AudioVideoIVRJob] ToneReceivedEvent processed.");
                }
            });
        }

        private async Task HandleToneEventAsync(IAudioVideoFlow avFlow, ToneReceivedEventArgs e)
        {
            AudioVideoIVRAction action = (AudioVideoIVRAction)e.Tone;
            Logger.Instance.Information("[AudioVideoIVRJob] ToneReceivedEvent received : {0}.", action);

            if (!promptMap.ContainsKey(action))
            {
                Logger.Instance.Information("[AudioVideoIVRJob] No action defined for this tone.");
                return;
            }

            if (action == AudioVideoIVRAction.TerminateCall)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Terminating the call.");
                var avCall = avFlow.Parent as IAudioVideoCall;

                await avCall.TerminateAsync(m_loggingContext).ConfigureAwait(false);
                CleanupEventHandlers(avFlow);
            }
            else 
            {
                await PlayPromptAsync(avFlow, action).ConfigureAwait(false);
            }
        }

        private async Task PlayPromptAsync(IAudioVideoFlow flow, AudioVideoIVRAction action)
        {
            string wavFile = promptMap.GetOrNull(action);
            Logger.Instance.Information("[AudioVideoIVRJob] playing prompt: {0}.", wavFile);
            var resourceUri = new Uri(string.Format("{0}://{1}/resources/{2}", m_callbackUri.Scheme, m_callbackUri.Host, wavFile));
            try
            {
                if (m_isPromptPlaying)
                {
                    await flow.StopPromptsAsync(m_loggingContext).ConfigureAwait(false);
                }
                m_isPromptPlaying = true;
                await flow.PlayPromptAsync(resourceUri, m_loggingContext).ConfigureAwait(false);
                m_isPromptPlaying = false;
            }
            catch (CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("[AudioVideoIVRJob] PlayPrompt api is not available!", ex);
            }
            catch (RemotePlatformServiceException ex)
            {
                Microsoft.SfB.PlatformService.SDK.ClientModel.ErrorInformation error = ex.ErrorInformation;
                if (error != null && error.Code == ErrorCode.Informational && error.Subcode == ErrorSubcode.CallTerminated)
                {
                    Logger.Instance.Information("[AudioVideoIVRJob] Call terminated while playing prompt.");
                }
                else
                {
                    throw;
                }
            }
        }

        private void CleanupEventHandlers(IAudioVideoFlow audioVideoFlow)
        {
            audioVideoFlow.ToneReceivedEvent -= ToneReceivedEvent;
        }

        private void HandlePSTNCallConversationRemoved(object sender, PlatformResourceEventArgs args)
        {
            Logger.Instance.Information("Incoming pstn call conversation is removed");
        }
    }
}
