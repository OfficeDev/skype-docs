using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using ResourceModel = Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    internal class AudioVideoFlow : BasePlatformResource<AudioVideoFlowResource, AudioVideoFlowCapability>, IAudioVideoFlow
    {
        #region Private fields

        private EventHandler<ToneReceivedEventArgs> m_toneReceivedEvent;

        /// <summary>
        /// Outgoing prompts
        /// </summary>
        private readonly ConcurrentDictionary<string, TaskCompletionSource<Prompt>> m_onGoingPromptTcses;

        #endregion

        #region Constructor

        internal AudioVideoFlow(IRestfulClient restfulClient, AudioVideoFlowResource resource, Uri baseUri, Uri resourceUri, object parent)
            : base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            m_onGoingPromptTcses = new ConcurrentDictionary<string, TaskCompletionSource<Prompt>>();
        }

        #endregion

        #region Public events

        /// <summary>
        /// The event raised when a tone event is received
        /// </summary>
        public event EventHandler<ToneReceivedEventArgs> ToneReceivedEvent
        {
            add
            {
                if (m_toneReceivedEvent == null || !m_toneReceivedEvent.GetInvocationList().Contains(value))
                {
                    m_toneReceivedEvent += value;
                }
            }
            remove { m_toneReceivedEvent -= value; }
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Get the audioVideoFlow state
        /// </summary>
        public FlowState State
        {
            get { return PlatformResource?.State ?? FlowState.Disconnected; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Plays prompt with the given <paramref name="promptUri"/> as an asynchronous operation.
        /// </summary>
        /// <param name="promptUri">The prompt URI.</param>
        /// <param name="loggingContext">The logging context.</param>
        /// <returns>Task&lt;IPrompt&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">promptUri</exception>
        /// <exception cref="CapabilityNotAvailableException">Link to play prompt is not available.</exception>
        public async Task<IPrompt> PlayPromptAsync(Uri promptUri, LoggingContext loggingContext = null)
        {
            if (promptUri == null)
            {
                throw new ArgumentNullException(nameof(promptUri));
            }

            string href = PlatformResource?.PlayPromptLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to play prompt is not available.");
            }

            var input = new PlayPromptInput() { PromptUrl = promptUri.ToString(), Loop = false };

            var playPromptLink = UriHelper.CreateAbsoluteUri(BaseUri, href);

            TaskCompletionSource<Prompt> tcs = new TaskCompletionSource<Prompt>();
            var response =  await PostRelatedPlatformResourceAsync(playPromptLink, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            if (response?.Headers?.Location != null)
            {
                m_onGoingPromptTcses.TryAdd(UriHelper.CreateAbsoluteUri(this.BaseUri, response.Headers.Location.ToString()).ToString().ToLower(), tcs);
            }

            // Return task to wait for the prompt completed event
            return await tcs.Task.ConfigureAwait(false);
        }

        /// <summary>
        /// Send stop prompts event and wait for all prompts to complete.
        /// </summary>
        /// <param name="loggingContext"><see cref="LoggingContext"/> to be used to log all related events</param>
        public async Task StopPromptsAsync(LoggingContext loggingContext = null)
        {
            string href = PlatformResource?.StopPromptsLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to stop prompts is not available.");
            }

            var stopPromptLink = UriHelper.CreateAbsoluteUri(BaseUri, href);
            await PostRelatedPlatformResourceAsync(stopPromptLink, null, loggingContext).ConfigureAwait(false);

            foreach (KeyValuePair<string, TaskCompletionSource<Prompt>> entry in m_onGoingPromptTcses)
            {
                try
                {
                    await entry.Value.Task.ConfigureAwait(false);
                } catch (RemotePlatformServiceException psException)
                {
                    if (psException.ErrorInformation.Code != ResourceModel.ErrorCode.Informational
                        || psException.ErrorInformation.Subcode != ResourceModel.ErrorSubcode.PromptsStopped)
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Gets whether a particular capability is available or not.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked.</param>
        /// <returns><code>true</code> iff the capability is available as of now.</returns>
        /// <remarks>Capabilities can change when a resource is updated. So, this method returning <code>true</code> doesn't guarantee that
        /// the capability will be available when it is actually used. Make sure to catch <see cref="T:Microsoft.SfB.PlatformService.SDK.Common.CapabilityNotAvailableException" /></remarks>
        public override bool Supports(AudioVideoFlowCapability capability)
        {
            string href = null;
            switch(capability)
            {
                case AudioVideoFlowCapability.PlayPrompt:
                    {
                        href = PlatformResource?.PlayPromptLink?.Href;
                        break;
                    }
                case AudioVideoFlowCapability.StopPrompts:
                {
                    href = PlatformResource?.StopPromptsLink?.Href;
                    break;
                }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            if (eventContext.EventEntity.Link.Token == ResourceModel.TokenMapper.GetTokenName(typeof(ToneResource)))
            {
                var toneResource = ConvertToPlatformServiceResource<ToneResource>(eventContext);
                Uri audioVideoFlowLink = UriHelper.CreateAbsoluteUri(this.BaseUri, toneResource.AudioVideoFlowLink.Href);

                if (string.Equals(audioVideoFlowLink.ToString(), this.ResourceUri.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var eventArgs = new ToneReceivedEventArgs(toneResource.ToneValue);
                    m_toneReceivedEvent?.Invoke(this, eventArgs);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            //No child to dispatch any more, need to dispatch to child , process locally for message type
            else if (string.Equals(eventContext.EventEntity.Link.Token, ResourceModel.TokenMapper.GetTokenName(typeof(PromptResource))))
            {
                PromptResource prompt = this.ConvertToPlatformServiceResource<PromptResource>(eventContext);
                if (prompt != null)
                {
                    if (eventContext.EventEntity.Relationship == ResourceModel.EventOperation.Completed)
                    {
                        TaskCompletionSource<Prompt> tcs = null;
                        Uri resourceAbsoluteUri = UriHelper.CreateAbsoluteUri(this.BaseUri, eventContext.EventEntity.Link.Href);
                        m_onGoingPromptTcses.TryGetValue(resourceAbsoluteUri.ToString().ToLower(), out tcs);
                        if (tcs != null)
                        {
                            Prompt p = new Prompt(this.RestfulClient, prompt, this.BaseUri, resourceAbsoluteUri, this);

                            if (eventContext.EventEntity.Status == ResourceModel.EventStatus.Success)
                            {
                                tcs.TrySetResult(p);
                            }
                            else if (eventContext.EventEntity.Status == ResourceModel.EventStatus.Failure)
                            {
                                ResourceModel.ErrorInformation error = eventContext.EventEntity.Error;
                                ErrorInformation errorInfo = error == null ? null : new ErrorInformation(error);
                                string errorMessage = errorInfo?.ToString();
                                tcs.TrySetException(new RemotePlatformServiceException("PlayPrompt failed with error " + errorMessage + eventContext.LoggingContext?.ToString(), errorInfo));
                            }
                            else
                            {
                                Logger.Instance.Error("Received invalid status code for prompt completed event");
                                tcs.TrySetException(new RemotePlatformServiceException("PlayPrompt failed"));
                            }
                            m_onGoingPromptTcses.TryRemove(eventContext.EventEntity.Link.Href.ToLower(), out tcs);
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// Class ToneReceivedEventArgs.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ToneReceivedEventArgs : EventArgs
    {
        internal ToneReceivedEventArgs(ToneValue tone)
        {
            Tone = tone;
        }

        /// <summary>
        /// Gets the tone.
        /// </summary>
        /// <value>The tone.</value>
        public ToneValue Tone { get; }
    }
}
