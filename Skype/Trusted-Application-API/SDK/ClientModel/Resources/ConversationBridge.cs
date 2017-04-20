using Microsoft.Rtc.Internal.Platform.ResourceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.RestAPI.Common.MediaTypeFormatters;
using System.Collections.Concurrent;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represents a ConversationBridge
    /// </summary>
    /// <seealso cref="BasePlatformResource{TPlatformResource, TCapabilities}"/>
    /// <seealso cref="IConversationBridge" />
    internal class ConversationBridge : BasePlatformResource<ConversationBridgeResource, ConversationBridgeCapability>, IConversationBridge
    {
        #region Private fields

        /// <summary>
        /// the bridged participants collection
        /// </summary>
        private readonly ConcurrentDictionary<string, BridgedParticipant> m_bridgedParticipants;

        /// <summary>
        /// tcses to track bridged participant added events
        /// </summary>
        private readonly ConcurrentDictionary<string, TaskCompletionSource<BridgedParticipant>> m_bridgedParticipantTcses;

        #endregion

        #region Public Properties

        /// <summary>
        /// fetch the bridged participants
        /// </summary>
        public List<IBridgedParticipant> BridgedParticipants
        {
            get
            {
                if (m_bridgedParticipants?.Count > 0)
                {
                    return m_bridgedParticipants.Values.Cast<IBridgedParticipant>().ToList();
                }
                else
                {
                    return new List<IBridgedParticipant>();
                }
            }
        }

        #endregion

        #region Constructor

        internal ConversationBridge(IRestfulClient restfulClient, ConversationBridgeResource resource, Uri baseUri, Uri resourceUri, Conversation parent) :
            base(restfulClient, resource, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Conversation is required");
            }
            m_bridgedParticipants = new ConcurrentDictionary<string, BridgedParticipant>();
            m_bridgedParticipantTcses = new ConcurrentDictionary<string, TaskCompletionSource<BridgedParticipant>>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add bridged participant
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="displayName"></param>
        /// <param name="sipUri"></param>
        /// <param name="enableMessageFilter"></param>
        /// <returns>the bridgeParticipant added</returns>
        public async Task<IBridgedParticipant> AddBridgedParticipantAsync(string displayName, SipUri sipUri, bool enableMessageFilter, LoggingContext loggingContext = null)
        {
            if (sipUri == null)
            {
                throw new ArgumentNullException(nameof(sipUri));
            }

            string href = PlatformResource?.BridgedParticipantsResourceLink?.Href;
            if (string.IsNullOrWhiteSpace(href))
            {
                throw new CapabilityNotAvailableException("Link to get BridgedsParticipants is not available.");
            }

            Uri bridgeUri = UriHelper.CreateAbsoluteUri(this.BaseUri, href);

            var input = new BridgedParticipantInput()
            {
                DisplayName = displayName,
                MessageFilterState = enableMessageFilter ? FilterState.Enabled : FilterState.Disabled,
                Uri = sipUri.ToString()
            };

            var tcs = new TaskCompletionSource<BridgedParticipant>();
            m_bridgedParticipantTcses.TryAdd(sipUri.ToString().ToLower(), tcs);
            //Waiting for bridgedParticipant operation added
            await PostRelatedPlatformResourceAsync(bridgeUri, input, new ResourceJsonMediaTypeFormatter(), loggingContext).ConfigureAwait(false);

            BridgedParticipant result = null;

            try
            {
                result = await tcs.Task.TimeoutAfterAsync(WaitForEvents).ConfigureAwait(false);
            }
            catch (TimeoutException)
            {
                throw new RemotePlatformServiceException("Timeout to get bridged participant added from platformservice!");
            }

            if (result == null)
            {
                throw new RemotePlatformServiceException("Platformservice do not deliver a bridged participant added resource with uri " + sipUri);
            }

            return result;
        }

        /// <summary>
        /// Add bridged participant
        /// </summary>
        /// <param name="loggingContext"></param>
        /// <param name="displayName"></param>
        /// <param name="sipUri"></param>
        /// <param name="enableMessageFilter"></param>
        /// <returns>the bridgeParticipant added</returns>
        [Obsolete("Please use the other variation")]
        public Task<IBridgedParticipant> AddBridgedParticipantAsync(LoggingContext loggingContext, string displayName, string sipUri, bool enableMessageFilter)
        {
            return AddBridgedParticipantAsync(displayName, new SipUri(sipUri), enableMessageFilter, loggingContext);
        }

        public override bool Supports(ConversationBridgeCapability capability)
        {
            string href = null;
            switch (capability)
            {
                case ConversationBridgeCapability.AddBridgedParticipant:
                    {
                        href = PlatformResource?.BridgedParticipantsResourceLink?.Href;
                        break;
                    }
            }

            return !string.IsNullOrWhiteSpace(href);
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// process conversation bridge participant events
        /// </summary>
        /// <param name="eventContext"></param>
        /// <returns></returns>
        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            //No child to dispatch any more, need to dispatch to child , process locally for message type
            if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(BridgedParticipantResource))))
            {
                if (eventContext.EventEntity.Relationship == EventOperation.Added)
                {
                    BridgedParticipantResource resource = this.ConvertToPlatformServiceResource<BridgedParticipantResource>(eventContext);
                    if (resource != null)
                    {
                        BridgedParticipant newBridgedParticipant = new BridgedParticipant(this.RestfulClient, resource, this.BaseUri,
                             UriHelper.CreateAbsoluteUri(eventContext.BaseUri, resource.SelfUri), this);
                        TaskCompletionSource<BridgedParticipant> tcs = null;
                        newBridgedParticipant.HandleResourceEvent(eventContext);
                        m_bridgedParticipants.TryAdd(UriHelper.NormalizeUri(resource.SelfUri, this.BaseUri), newBridgedParticipant);
                        if (m_bridgedParticipantTcses.TryRemove(resource.Uri.ToLower(), out tcs))
                        {
                            tcs.SetResult(newBridgedParticipant);
                        }
                    }
                }

                if (eventContext.EventEntity.Relationship == EventOperation.Updated)
                {
                    BridgedParticipantResource resource = this.ConvertToPlatformServiceResource<BridgedParticipantResource>(eventContext);
                    if (resource != null)
                    {
                        BridgedParticipant newBridgedParticipant = null;

                        if (m_bridgedParticipants.TryGetValue(UriHelper.NormalizeUri(resource.SelfUri, this.BaseUri), out newBridgedParticipant))
                        {
                            newBridgedParticipant.HandleResourceEvent(eventContext);
                        }
                    }
                }

                if (eventContext.EventEntity.Relationship == EventOperation.Deleted)
                {
                    BridgedParticipant newBridgedParticipant = null;
                    if (m_bridgedParticipants.TryRemove(UriHelper.NormalizeUri(eventContext.EventEntity.Link.Href, this.BaseUri), out newBridgedParticipant))
                    {
                        newBridgedParticipant.HandleResourceEvent(eventContext);
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
}
