using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Microsoft.Rtc.Internal.RestAPI.ResourceModel;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Represent a collection of participants
    /// </summary>
    internal class ParticipantsInternal : BasePlatformResource<ParticipantsResource, ParticipantsCapability>
    {
        #region Private fields

        /// <summary>
        /// The participant cache
        /// </summary>
        private readonly ConcurrentDictionary<string, Participant> m_participantsCache;

        #endregion

        #region Constructor

        internal ParticipantsInternal(IRestfulClient restfulClient, Uri baseUri, Uri resourceUri, Conversation parent)
            : base(restfulClient, null, baseUri, resourceUri, parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent), "Conversation is required");
            }
            m_participantsCache = new ConcurrentDictionary<string, Participant>();
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Get the participants cache
        /// </summary>
        internal ConcurrentDictionary<string, Participant> ParticipantsCache
        {
            get { return m_participantsCache; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// <see cref="ParticipantsInternal"/> doesn't support any capability so always returns <code>false</code>.
        /// </summary>
        /// <param name="capability">Capability that needs to be checked</param>
        /// <returns><code>false</code> </returns>
        public override bool Supports(ParticipantsCapability capability)
        {
            return false;
        }

        #endregion

        #region Internal methods

        internal override bool ProcessAndDispatchEventsToChild(EventContext eventContext)
        {
            ParticipantChangeEventArgs participantChangeEventArgs = null;

            //No child to dispatch any more, need to dispatch to child , process locally for message type
            if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(ParticipantResource))))
            {
                if (eventContext.EventEntity.In != null)
                {
                    //TODO: currently we do not handle in link
                    return true;
                }

                string normalizedUri = UriHelper.NormalizeUri(eventContext.EventEntity.Link.Href, this.BaseUri);
                ParticipantResource participantResource = this.ConvertToPlatformServiceResource<ParticipantResource>(eventContext);
                switch (eventContext.EventEntity.Relationship)
                {
                    case EventOperation.Added:
                        {
                            Participant tempParticipant = null;
                            if (!m_participantsCache.TryGetValue(normalizedUri, out tempParticipant))
                            {
                                tempParticipant = new Participant(this.RestfulClient, participantResource, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, eventContext.EventEntity.Link.Href), this.Parent as Conversation);
                                m_participantsCache.TryAdd(normalizedUri, tempParticipant);
                                participantChangeEventArgs = new ParticipantChangeEventArgs();
                                participantChangeEventArgs.AddedParticipants = new List<IParticipant> { tempParticipant };
                            }
                            else
                            {
                                //Should get some participant added In event , ignore currently
                            }
                            break;
                        }
                    case EventOperation.Updated:
                        {
                            Participant tempParticipant = null;
                            if (!m_participantsCache.TryGetValue(normalizedUri, out tempParticipant))
                            {
                                Logger.Instance.Warning("Get a participant updated event for a participant not in cache, any error happened ?");
                                tempParticipant = new Participant(this.RestfulClient, participantResource, this.BaseUri, UriHelper.CreateAbsoluteUri(this.BaseUri, eventContext.EventEntity.Link.Href), this.Parent as Conversation);
                                m_participantsCache.TryAdd(normalizedUri, tempParticipant);
                            }
                            tempParticipant.HandleResourceEvent(eventContext);
                            participantChangeEventArgs = new ParticipantChangeEventArgs();
                            participantChangeEventArgs.UpdatedParticipants = new List<IParticipant> { tempParticipant };
                            break;
                        }
                    case EventOperation.Deleted:
                        {
                            Participant tempParticipant = null;
                            if (m_participantsCache.TryRemove(normalizedUri, out tempParticipant))
                            {
                                tempParticipant.HandleResourceEvent(eventContext);
                                participantChangeEventArgs = new ParticipantChangeEventArgs();
                                participantChangeEventArgs.RemovedParticipants = new List<IParticipant> { tempParticipant };
                            }
                            break;
                        }
                }

                if (participantChangeEventArgs != null)
                {
                    var conv = this.Parent as Conversation;
                    conv.OnParticipantChange(participantChangeEventArgs);
                }

                return true;
            }
            else if (string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(ParticipantMessagingResource)))
                || string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(ParticipantAudioResource)))
                || string.Equals(eventContext.EventEntity.Link.Token, TokenMapper.GetTokenName(typeof(ParticipantApplicationSharingResource))))
            {
                if (eventContext.EventEntity.In != null)
                {
                    string normalizedParticipantUri = UriHelper.NormalizeUri(eventContext.EventEntity.In.Href, this.BaseUri);
                    Participant tempParticipant = null;
                    if (m_participantsCache.TryGetValue(normalizedParticipantUri, out tempParticipant))
                    {
                        tempParticipant.ProcessAndDispatchEventsToChild(eventContext);
                    }
                }

                return true; //We do not rely on the response of tempParticipant.ProcessAndDispatchEventsToChild since we already know this is participant modality event
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
