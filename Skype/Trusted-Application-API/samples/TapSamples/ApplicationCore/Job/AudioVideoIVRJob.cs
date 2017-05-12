using System;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// This job waits for an incoming call and when it receives one, it presents the user an IVR prompt. User can press a key
    /// to:
    ///   1. Transfer the call
    ///   2. Terminate the call
    ///   3. Repeat the prompt
    ///   4. Go to the previous menu
    /// </summary>
    /// <remarks>
    /// If it is configured to <code>transfer</code> an incoming call without playing prompt, it will <i>forward</i> it immediately to the configured user.
    /// If it is configured to <code>terminate</code> an incoming call without playing prompt, it will <i>decline</i> it immediately.
    /// </remarks>
    public class AudioVideoIVRJob : PlatformServiceListeningJobBase
    {
        #region Constructor

        /// <summary>
        /// Constructs instances of <see cref="AudioVideoIVRJob"/>.
        /// </summary>
        /// <param name="jobId">Unique ID of this job, it distinguishes the job from other jobs.</param>
        /// <param name="instanceId">ID of the service/process/webapp hosting all the jobs.</param>
        /// <param name="input"><see cref="AudioVideoIVRJobInput"/> providing configuration for the job.</param>
        public AudioVideoIVRJob(string jobId, string instanceId, AzureBasedApplicationBase azureApplication, AudioVideoIVRJobInput input)
            : base(jobId, instanceId, azureApplication, input, JobType.AudioVideoIVR)
        {
        }

        #endregion

        #region Protected overrides
        /// <summary>
        /// Starts listening for incoming <see cref="AudioVideoInvitation"/>s.
        /// </summary>
        protected override void StartCore()
        {
            Logger.Instance.Information("[AudioVideoIVRJob] Starting.");

            AzureApplication.ApplicationEndpoint.HandleIncomingAudioVideoCall += HandleIncomingAudioVideoCall;
        }

        /// <summary>
        /// Stops listening for incoming <see cref="AudioVideoInvitation"/>s.
        /// </summary>
        protected override void StopCore()
        {
            Logger.Instance.Information("[AudioVideoIVRJob] Stopping.");
            AzureApplication.ApplicationEndpoint.HandleIncomingAudioVideoCall -= HandleIncomingAudioVideoCall;
        }

        #endregion

        #region Private event handlers

        /// <summary>
        /// Method to be invoked on an incoming <see cref="AudioVideoInvitation"/>.
        /// </summary>
        /// <param name="sender"><see cref="ApplicationEndpoint"/> which received the <see cref="AudioVideoInvitation"/>.</param>
        /// <param name="e"><see cref="IncomingInviteEventArgs{T}"/> containing the incoming <see cref="AudioVideoInvitation"/>.</param>
        private void HandleIncomingAudioVideoCall(object sender, IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            HandleIncomingAudioVideoCallAsync(e).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    Logger.Instance.Error("[AudioVideoIVRJob] Exception while processing incoming audio video call.", t.Exception);
                }
                else
                {
                    Logger.Instance.Information("[AudioVideoIVRJob] Incoming audio video call processed.");
                }
            });
        }

        #endregion

        #region Private methods

        private async Task HandleIncomingAudioVideoCallAsync(IncomingInviteEventArgs<IAudioVideoInvitation> e)
        {
            Logger.Instance.Information("[AudioVideoIVRJob] Incoming AudioVideoCall.");

            var input = JobInput as AudioVideoIVRJobInput;
            IAudioVideoInvitation invite = e.NewInvite;

            if (input.Action == AudioVideoIVRActions.TerminateCall)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Declining incoming call.");
                await invite.DeclineAsync(LoggingContext).ConfigureAwait(false);
            }

            if (input.Action == AudioVideoIVRActions.TransferToUser)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Forwarding the call to {0}.", input.User);
                await invite.ForwardAsync(LoggingContext, input.User).ConfigureAwait(false);
            }

            if (input.Action == AudioVideoIVRActions.PlayPrompt || input.Action == AudioVideoIVRActions.RepeatPrompt)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Accepting the call.");
                await invite.AcceptAsync(LoggingContext).ConfigureAwait(false);
                await e.NewInvite.WaitForInviteCompleteAsync().ConfigureAwait(false);
                if (e.NewInvite.RelatedConversation?.AudioVideoCall == null)
                {
                    Logger.Instance.Warning("[AudioVideoIVRJob] AudioVideoModality not found in the conversation.");
                    return;
                }

                Logger.Instance.Information("[AudioVideoIVRJob] Call accepted.");

                var promptHandler = new AudioVideoIVRPromptHandler(input, this.AzureApplication, LoggingContext);
                promptHandler.HandleEstablishedAudioVideo(e.NewInvite.RelatedConversation.AudioVideoCall);
            }
        }

        #endregion
    }

    /// <summary>
    /// Handles IVR prompt for an already established AudioVideo call.
    /// </summary>
    internal class AudioVideoIVRPromptHandler
    {
        /// <summary>
        /// <see cref="AudioVideoIVRJobInput"/> can be a hierarchy of multiple objects, to allow multiple prompts. This reference points
        /// to the object which is currenlty selected by the user.
        /// </summary>
        private AudioVideoIVRJobInput currentMenu;

        /// <summary>
        /// <see cref="LoggingContext"/> to be used for all log statements.
        /// </summary>
        private LoggingContext loggingContext;

        private AzureBasedApplicationBase azureApplication;

        /// <summary>
        /// Create instance of <see cref="AudioVideoIVRPromptHandler"/>.
        /// </summary>
        /// <param name="input"><see cref="AudioVideoIVRJobInput"/> providing configuration for the job</param>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used for all log statements</param>
        internal AudioVideoIVRPromptHandler(AudioVideoIVRJobInput input, AzureBasedApplicationBase azureApplication, LoggingContext loggingContext)
        {
            currentMenu = input;
            this.azureApplication = azureApplication;
            this.loggingContext = loggingContext;
        }

        /// <summary>
        /// Play prompts and listen for user inputs on <paramref name="audioVideoCall"/>.
        /// </summary>
        /// <param name="audioVideoCall"><see cref="AudioVideoCall"/> on which we need to play prompts and listen for user inputs.</param>
        internal void HandleEstablishedAudioVideo(IAudioVideoCall audioVideoCall)
        {
            audioVideoCall.AudioVideoFlowConnected += AudioVideoFlowConnected;
        }

        /// <summary>
        /// Method to be invoked when <see cref="AudioVideoFlow"/> is connected.
        /// </summary>
        /// <param name="sender"><see cref="AudioVideoCall"/> which received the <see cref="AudioVideoFlow"/> updated event.</param>
        /// <param name="e"><see cref="AudioVideoFlowUpdatedEventArgs"/> containing information about the event.</param>
        private void AudioVideoFlowConnected(object sender, AudioVideoFlowUpdatedEventArgs e)
        {
            Logger.Instance.Information("[AudioVideoIVRJob] AudioVideoFlowConnected event received.");
            e.AudioVideoFlow.ToneReceivedEvent += ToneReceivedEvent;

            PlayPromptAsync(e.AudioVideoFlow).ContinueWith(t => Logger.Instance.Information("[AudioVideoIVRJob] PlayPrompt completed."));
        }

        /// <summary>
        /// Removes all eventhandlers from <paramref name="audioVideoFlow"/>.
        /// </summary>
        /// <param name="audioVideoFlow">The <see cref="AudioVideoFlow"/> object on which we are currently listening for events</param>
        private void CleanupEventHandlers(IAudioVideoFlow audioVideoFlow)
        {
            audioVideoFlow.ToneReceivedEvent -= ToneReceivedEvent;

            var audioVideoCall = audioVideoFlow.Parent as IAudioVideoCall;
            audioVideoCall.AudioVideoFlowConnected -= AudioVideoFlowConnected;
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

        private async Task HandleToneEventAsync(IAudioVideoFlow flow, ToneReceivedEventArgs e)
        {
            string tone = ToneValueToString(e.Tone);
            if (tone == null)
            {
                Logger.Instance.Warning("[AudioVideoIVRJob] Tone could not be identified : {0}.", e.Tone.ToString());
                return;
            }

            Logger.Instance.Information("[AudioVideoIVRJob] ToneReceivedEvent received : {0}.", tone);

            if (currentMenu?.KeyMap == null || !currentMenu.KeyMap.ContainsKey(tone))
            {
                Logger.Instance.Information("[AudioVideoIVRJob] No action defined for this tone.");
                return;
            }

            var keyAction = currentMenu.KeyMap[tone];

            if (keyAction.Action == AudioVideoIVRActions.PlayPrompt)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Playing prompt.");
                currentMenu = keyAction;
                await PlayPromptAsync(flow).ConfigureAwait(false);
            }
            else if (keyAction.Action == AudioVideoIVRActions.RepeatPrompt)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Repeating prompt.");
                await PlayPromptAsync(flow).ConfigureAwait(false);
            }
            else if (keyAction.Action == AudioVideoIVRActions.GoToPreviousPrompt)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Going to previous prompt.");
                currentMenu = currentMenu.ParentInput ?? currentMenu;
                await PlayPromptAsync(flow).ConfigureAwait(false);
            }
            else if (keyAction.Action == AudioVideoIVRActions.TransferToUser)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Transferring the call to {0}.", keyAction.User);
                currentMenu = keyAction;
                await PlayPromptAsync(flow).ConfigureAwait(false);

                var audioVideoCall = flow.Parent as IAudioVideoCall;

                await audioVideoCall.TransferAsync(keyAction.User, null, loggingContext).ConfigureAwait(false);
                CleanupEventHandlers(flow);
            }
            else if (keyAction.Action == AudioVideoIVRActions.TerminateCall)
            {
                Logger.Instance.Information("[AudioVideoIVRJob] Terminating the call.");
                var audioVideoCall = flow.Parent as IAudioVideoCall;

                await audioVideoCall.TerminateAsync(loggingContext).ConfigureAwait(false);
                CleanupEventHandlers(flow);
            }
        }

        /// <summary>
        /// Plays the prompt specified by <see cref="currentMenu"/> on the <paramref name="flow"/>.
        /// </summary>
        /// <param name="flow"><see cref="AudioVideoFlow"/> on which the prompt is to be played.</param>
        /// <returns>void; it is an <i>async</i> method.</returns>
        private async Task PlayPromptAsync(IAudioVideoFlow flow)
        {
            if (flow == null)
            {
                throw new ArgumentNullException(nameof(flow));
            }

            string resourceName = currentMenu.Prompt;
            Logger.Instance.Information("[AudioVideoIVRJob] Playing prompt : {0}.", resourceName);

            try
            {
                var resourceUri = new Uri(string.Format(azureApplication.ResourceUriFormat, resourceName));
                await flow.PlayPromptAsync(resourceUri, loggingContext).ConfigureAwait(false);
            }
            catch(CapabilityNotAvailableException ex)
            {
                Logger.Instance.Error("[AudioVideoIVRJob] PlayPrompt api is not available!", ex);
            }
            catch (RemotePlatformServiceException ex)
            {
                ClientModel.ErrorInformation error = ex.ErrorInformation;
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

        /// <summary>
        /// Converts a <paramref name="tone"/> to the corresponding <see cref="string"/>.
        /// </summary>
        /// <param name="tone"><see cref="ToneValue"/> to be converted.</param>
        /// <returns><see cref="string"/> value of the <paramref name="tone"/>.</returns>
        private string ToneValueToString(ToneValue tone)
        {
            switch (tone)
            {
                case ToneValue.Tone0:
                    return "0";
                case ToneValue.Tone1:
                    return "1";
                case ToneValue.Tone2:
                    return "2";
                case ToneValue.Tone3:
                    return "3";
                case ToneValue.Tone4:
                    return "4";
                case ToneValue.Tone5:
                    return "5";
                case ToneValue.Tone6:
                    return "6";
                case ToneValue.Tone7:
                    return "7";
                case ToneValue.Tone8:
                    return "8";
                case ToneValue.Tone9:
                    return "9";
                case ToneValue.ToneStar:
                    return "*";
                case ToneValue.TonePound:
                    return "#";
            }

            return null;
        }
    }
}
