using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class ConversationTests
    {
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private Mock<IEventChannel> m_eventChannel;
        private IConversation m_conversation;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;
            m_eventChannel = data.EventChannel;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);

            var communication = data.ApplicationEndpoint.Application.Communication;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                // Deliver invitation started events
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioInvitationStarted.json", m_eventChannel);
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

                // Deliver conversation deleted event
                TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Conversation, HttpMethod.Delete, "Event_ConversationDeleted.json", m_eventChannel);
            };

            // Start a conversation with messaging modality
            IMessagingInvitation invitation = await communication
                .StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), "https://example.com/callback")
                .ConfigureAwait(false);

            m_conversation = invitation.RelatedConversation;
        }

        [TestMethod]
        public void ConversationStateShouldBeDisconnectedByDefault()
        {
            // Given
            // Setup

            // Then
            Assert.AreEqual(ConversationState.Disconnected, m_conversation.State);
        }

        [TestMethod]
        public void ConversationStateShouldTransitionToConnectingOnEvent()
        {
            // Given
            var eventRaised = false;
            ConversationState oldState = ConversationState.Disconnected;
            ConversationState newState = ConversationState.Disconnected;
            m_conversation.ConversationStateChanged += (sender, args) => { eventRaised = true; oldState = args.OldState; newState = args.NewState; };
            Assert.AreEqual(ConversationState.Disconnected, m_conversation.State);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationAdded.json");

            // Then
            Assert.AreEqual(ConversationState.Connecting, m_conversation.State);
            Assert.IsTrue(eventRaised, "ConversationStateChanged event not raised.");
            Assert.AreEqual(ConversationState.Disconnected, oldState);
            Assert.AreEqual(ConversationState.Connecting, newState);
        }

        [TestMethod]
        public void ConversationStateShouldTransitionToConnectedOnEvent()
        {
            // Given
            var eventRaised = false;
            ConversationState oldState = ConversationState.Disconnected;
            ConversationState newState = ConversationState.Disconnected;
            m_conversation.ConversationStateChanged += (sender, args) => { eventRaised = true; oldState = args.OldState; newState = args.NewState; };
            Assert.AreEqual(ConversationState.Disconnected, m_conversation.State);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // Then
            Assert.AreEqual(ConversationState.Connected, m_conversation.State);
            Assert.IsTrue(eventRaised, "ConversationStateChanged event not raised.");
            Assert.AreEqual(ConversationState.Disconnected, oldState);
            Assert.AreEqual(ConversationState.Connected, newState);
        }

        [TestMethod]
        public void ConversationStateShouldTransitionToDisconnectedOnEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");
            var eventRaised = false;
            ConversationState oldState = ConversationState.Disconnected;
            ConversationState newState = ConversationState.Disconnected;
            m_conversation.ConversationStateChanged += (sender, args) => { eventRaised = true; oldState = args.OldState; newState = args.NewState; };
            Assert.AreEqual(ConversationState.Connected, m_conversation.State);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationDisconnected.json");

            // Then
            Assert.AreEqual(ConversationState.Disconnected, m_conversation.State);
            Assert.IsTrue(eventRaised, "ConversationStateChanged event not raised.");
            Assert.AreEqual(ConversationState.Connected, oldState);
            Assert.AreEqual(ConversationState.Disconnected, newState);
        }

        [TestMethod]
        public void ConversationShouldHaveInLobbyStateIfNotAdmittedToTheConference()
        {
            // Given
            var eventRaised = false;
            ConversationState oldState = ConversationState.Disconnected;
            ConversationState newState = ConversationState.Disconnected;
            m_conversation.ConversationStateChanged += (sender, args) => { eventRaised = true; oldState = args.OldState; newState = args.NewState; };
            Assert.AreEqual(ConversationState.Disconnected, m_conversation.State);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationInLobby.json");

            // Then
            Assert.AreEqual(ConversationState.InLobby, m_conversation.State);
            Assert.IsTrue(eventRaised, "ConversationStateChanged event not raised.");
            Assert.AreEqual(ConversationState.Disconnected, oldState);
            Assert.AreEqual(ConversationState.InLobby, newState);
        }

        [TestMethod]
        public void ConversationShouldHaveConferencingStateIfNotYetJoinedToConference()
        {
            // Given
            var eventRaised = false;
            ConversationState oldState = ConversationState.Disconnected;
            ConversationState newState = ConversationState.Disconnected;
            m_conversation.ConversationStateChanged += (sender, args) => { eventRaised = true; oldState = args.OldState; newState = args.NewState; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferencing.json");

            // Then
            Assert.AreEqual(ConversationState.Conferencing, m_conversation.State);
            Assert.IsTrue(eventRaised, "ConversationStateChanged event not raised.");
            Assert.AreEqual(ConversationState.Disconnected, oldState);
            Assert.AreEqual(ConversationState.Conferencing, newState);
        }

        [TestMethod]
        public void ConversationShouldHaveConferencedStateIfConferenceJoinSuccessful()
        {
            // Given
            var eventRaised = false;
            ConversationState oldState = ConversationState.Disconnected;
            ConversationState newState = ConversationState.Disconnected;
            m_conversation.ConversationStateChanged += (sender, args) => { eventRaised = true; oldState = args.OldState; newState = args.NewState; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced.json");

            // Then
            Assert.AreEqual(ConversationState.Conferenced, m_conversation.State);
            Assert.IsTrue(eventRaised, "ConversationStateChanged event not raised.");
            Assert.AreEqual(ConversationState.Disconnected, oldState);
            Assert.AreEqual(ConversationState.Conferenced, newState);
        }

        [TestMethod]
        public async Task ShouldBeAbleToDeleteConversation()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");
            Assert.AreEqual(ConversationState.Connected, m_conversation.State);

            var resourceRemoved = false;
            m_conversation.HandleResourceRemoved += (sender, args) => resourceRemoved = true;
            var eventRaised = false;
            m_conversation.ConversationStateChanged += (sender, args) => eventRaised = true;

            // When
            await m_conversation.DeleteAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("DELETE " + DataUrls.Conversation), "Request to delete conversation was not sent out.");
            Assert.IsTrue(resourceRemoved, "ResourceRemoved event not raised for the conversation.");
            Assert.IsFalse(eventRaised, "ConversationStateChanged event raised when not expected.");
        }

        [TestMethod]
        public void ConversationShouldReflectActiveModalities()
        {
            string originalContent = File.ReadAllText("Data\\Event_ConversationConnected.json");

            // We will change active modalities in file Event_ConversationConnected.json and verfiy that m_conversation object reflects it when event is raised

            var allModalities = (ConversationModalityType[])Enum.GetValues(typeof(ConversationModalityType));

            foreach (var combination in GenerateAllCombinations(allModalities))
            {
                var modalitiesList = ToStringModalities(combination);
                var newContent = originalContent.Replace("\"Messaging\"", modalitiesList);

                var eventsChannelArgs = TestHelper.GetEventsChannelArgsFromContent(newContent);
                m_eventChannel.Raise(mock => mock.HandleIncomingEvents += null, m_eventChannel.Object, eventsChannelArgs);

                Assert.IsTrue(ConversationContainsAllModalities(combination), "Modalities don't match for string: " + modalitiesList);
            }
        }

        [TestMethod]
        public void ShouldExposeMessagingCallIfAvailable()
        {
            // Given
            // Setup

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // Then
            Assert.IsNotNull(m_conversation.MessagingCall);
        }

        [TestMethod]
        public void ShouldNotExposeMessagingCallIfNotAvailable()
        {
            // Given
            // Setup

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected_NoModality.json");

            // Then
            Assert.IsNull(m_conversation.MessagingCall);
        }

        [TestMethod]
        public void ShouldExposeAudioVideoCallIfAvailable()
        {
            // Given
            // Setup

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // Then
            Assert.IsNotNull(m_conversation.AudioVideoCall);
        }

        [TestMethod]
        public void ShouldNotExposeAudioVideoCallIfNotAvailable()
        {
            // Given
            // Setup

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected_NoModality.json");

            // Then
            Assert.IsNull(m_conversation.AudioVideoCall);
        }

        [TestMethod]
        public void ShouldExposeConversationConferenceIfAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced.json");

            // When
            IConversationConference conf = m_conversation.ConversationConference;

            // Then
            Assert.IsNotNull(conf);
        }

        [TestMethod]
        public void ShouldNotExposeConversationConferenceIfNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced_NoActionLink.json");

            // When
            IConversationConference conf = m_conversation.ConversationConference;

            // Then
            Assert.IsNull(conf);
        }

        [TestMethod]
        public void ShouldExposeConversationBridgeIfAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected_WithConversationBridge.json");

            // When
            IConversationBridge bridge = m_conversation.ConversationBridge;

            // Then
            Assert.IsNotNull(bridge);
        }

        [TestMethod]
        public void ShouldNotExposeConversationBridgeIfNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // When
            IConversationBridge bridge = m_conversation.ConversationBridge;

            // Then
            Assert.IsNull(bridge);
        }

        [TestMethod]
        public void ShouldExposeParticipantsIfAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // When
            List<IParticipant> participants = m_conversation.Participants;

            // Then
            Assert.IsNotNull(participants);
            Assert.AreEqual(1, participants.Count);
        }

        [TestMethod]
        public void ShouldNotExposeParticipantsIfNotAvailable()
        {
            // Given
            // Setup

            // When
            List<IParticipant> participants = m_conversation.Participants;

            // Then
            Assert.IsNotNull(participants);
            Assert.AreEqual(0, participants.Count);
        }

        /// <summary>
        /// Makes sure we return a copy of the internal list, so that if client modifies the list we don't end up in
        /// a messed up state in the SDK.
        /// </summary>
        [TestMethod]
        public void ParticipantsPropertyShouldReturnACopyOfTheInternalList()
        {
            // Given
            List<IParticipant> participants = m_conversation.Participants;
            Assert.IsNotNull(participants);
            Assert.AreEqual(0, participants.Count);

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // Then

            // The new list should have the added participant
            List<IParticipant> newParticipants = m_conversation.Participants;
            Assert.IsNotNull(newParticipants);
            Assert.AreEqual(1, newParticipants.Count);

            // The old list should still be empty
            Assert.AreEqual(0, participants.Count);
        }

        [TestMethod]
        public void ShouldProvideAddedParticipantsInParticipantChangedEvent()
        {
            // Given
            var eventReceived = false;
            m_conversation.HandleParticipantChange += (sender, args) => { if (args.AddedParticipants?.Count == 1) eventReceived = true; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldProvideRemovedParticipantsInParticipantChangedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            var eventReceived = false;
            m_conversation.HandleParticipantChange += (sender, args) => { if (args.RemovedParticipants?.Count == 1) eventReceived = true; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantDeleted.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldProvideUpdatedParticipantsInParticipantChangedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            var eventReceived = false;
            m_conversation.HandleParticipantChange += (sender, args) => { if (args.UpdatedParticipants?.Count == 1) eventReceived = true; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantUpdated.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldProvideAllChangesInParticipantChangedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded_SecondParticipant.json");
            var addedEventReceived = false;
            var updatedEventReceived = false;
            var deletedEventReceived = false;
            m_conversation.HandleParticipantChange += (sender, args) =>
            {
                // SDK may decide to provide multiple events even though all events arrive in same package
                if(args.AddedParticipants?.Count == 1 && args.AddedParticipants[0].Uri == "sip:participant3@example.com")
                {
                    addedEventReceived = true;
                }
                if (args.UpdatedParticipants?.Count == 1 && args.UpdatedParticipants[0].Uri == "sip:participant@example.com")
                {
                    updatedEventReceived = true;
                }
                if (args.RemovedParticipants?.Count == 1 && args.RemovedParticipants[0].Uri == "sip:participant2@example.com")
                {
                    deletedEventReceived = true;
                }
            };

            // When
            // participant is updated, participant2 is deleted and participant3 is added
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_Participant_MultipleEvents.json");

            // Then
            Assert.IsTrue(addedEventReceived);
            Assert.IsTrue(updatedEventReceived);
            Assert.IsTrue(deletedEventReceived);
        }

        [TestMethod]
        public void ShouldNotTriggerParticipantChangedEventIfNoParticipantEvent()
        {
            // Given
            var eventReceived = false;
            m_conversation.HandleParticipantChange += (sender, args) => eventReceived = true;

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // Then
            Assert.IsFalse(eventReceived);
        }

        [TestMethod]
        public void ShouldDeliverParticipantChangedEventsInOrderIfMultipleEventHandlersRegistered()
        {
            // Given
            var numberOfEventsReceived = 0;
            var lastReceivedEvent = 0;
            m_conversation.HandleParticipantChange += (sender, args) => { ++numberOfEventsReceived; lastReceivedEvent = 1; };
            m_conversation.HandleParticipantChange += (sender, args) => { ++numberOfEventsReceived; lastReceivedEvent = 2; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // Then
            Assert.AreEqual(2, numberOfEventsReceived);
            Assert.AreEqual(2, lastReceivedEvent);
        }

        [TestMethod]
        public void ShouldDeliverParticipantChangedEventsOnlyOnceIfSameEventHandlerRegisteredMutipleTimes()
        {
            // Given
            var numberOfEventsReceived = 0;
            EventHandler<ParticipantChangeEventArgs> handler = (sender, args) => Interlocked.Increment(ref numberOfEventsReceived);
            m_conversation.HandleParticipantChange += handler;
            m_conversation.HandleParticipantChange += handler;

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // Then
            Assert.AreEqual(1, numberOfEventsReceived);
        }

        [TestMethod]
        public void ShouldDeliverConversationStateChangedEventInOrderIfMultipleEventHandlersRegistered()
        {
            // Given
            var numberOfEventsReceived = 0;
            var lastReceivedEvent = 0;
            m_conversation.ConversationStateChanged += (sender, args) => { ++numberOfEventsReceived; lastReceivedEvent = 1; };
            m_conversation.ConversationStateChanged += (sender, args) => { ++numberOfEventsReceived; lastReceivedEvent = 2; };

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // Then
            Assert.AreEqual(2, numberOfEventsReceived);
            Assert.AreEqual(2, lastReceivedEvent);
        }

        [TestMethod]
        public void ShouldDeliverConversationStateChangedEventOnlyOnceIfSameEventHandlerRegisteredMutipleTimes()
        {
            // Given
            var numberOfEventsReceived = 0;
            EventHandler<ConversationStateChangedEventArgs> handler = (sender, args) => Interlocked.Increment(ref numberOfEventsReceived);
            m_conversation.ConversationStateChanged += handler;
            m_conversation.ConversationStateChanged += handler;

            // When
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // Then
            Assert.AreEqual(1, numberOfEventsReceived);
        }

        [TestMethod]
        public void TryGetParticipantShouldReturnNullIfNoParticipantInConversation()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConnected.json");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(DataUrls.Participant);

            // Then
            Assert.IsNull(participant);
        }

        [TestMethod]
        public void TryGetParticipantShouldReturnNullIfParticipantNotInConversation()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            string nonExistingParticipantUrl = DataUrls.Participant.Replace("participant@example.com", "participant_notexisting@example.com");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(nonExistingParticipantUrl);

            // Then
            Assert.IsNull(participant);
        }

        [TestMethod]
        public void TryGetParticipantShouldRetrieveTheParticipant()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(DataUrls.Participant);

            // Then
            Assert.IsNotNull(participant);
        }

        [TestMethod]
        public void TryGetParticipantShouldBeCaseInsensitiveToParticipantUri()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            string participantUrl = DataUrls.Participant.Replace("participant@example.com", "PARTICIPANT@EXAMPLE.COM");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(participantUrl);

            // Then
            Assert.IsNotNull(participant);
        }

        [TestMethod]
        public void TryGetParticipantShouldWorkForUriWithoutDomain()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            string participantUrl = DataUrls.Participant.Replace("https://webpoolbl20r04.infra.lync.com", "");
            Assert.IsTrue(participantUrl.StartsWith("/"));

            // When
            IParticipant participant = m_conversation.TryGetParticipant(participantUrl);

            // Then
            Assert.IsNotNull(participant);
        }

        [TestMethod]
        public void TryGetParticipantShouldWorkForUriWithDomain()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");
            Assert.IsTrue(DataUrls.Participant.StartsWith("https://"));

            // When
            IParticipant participant = m_conversation.TryGetParticipant(DataUrls.Participant);

            // Then
            Assert.IsNotNull(participant);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetParticipantShouldThrowForNullParticipantUri()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(null);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TryGetParticipantShouldThrowForEmptyParticipantUri()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(string.Empty);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public void TryGetParticipantShouldHandleMalformedParticipantUri()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ParticipantAdded.json");

            // When
            IParticipant participant = m_conversation.TryGetParticipant(":asdf//invaliduri\\+-&");

            // Then
            Assert.IsNull(participant);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task AddParticipantAsyncShouldThrowIfUrlNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced_NoActionLink.json");

            // When
            await m_conversation.AddParticipantAsync(new SipUri("sip:user@example.com"), m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task AddParticipantAsyncShouldMakeHttpRequest()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced.json");

            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.ParticipantInvitation, HttpMethod.Post, "Event_ParticipantInvitationStarted.json", m_eventChannel);

            // When
            await m_conversation.AddParticipantAsync(new SipUri("sip:user@example.com"), m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.ParticipantInvitation));
        }

        [TestMethod]
        public async Task AddParticipantAsyncShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced.json");

            // Capture operation id when HTTP POST request is made
            var invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.ParticipantInvitation, HttpMethod.Post, null, null);
                if(!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            Task invitationTask = m_conversation.AddParticipantAsync(new SipUri("sip:user@example.com"), m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_ParticipantInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        public void ShouldSupportAddParticipantCapabilityIfLinkIsAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced.json");

            // When
            bool supported = m_conversation.Supports(ConversationCapability.AddParticipant);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public void ShouldNotSupportAddParticipantCapabilityIfLinkIsNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_eventChannel, "Event_ConversationConferenced_NoActionLink.json");

            // When
            bool supported = m_conversation.Supports(ConversationCapability.AddParticipant);

            // Then
            Assert.IsFalse(supported);
        }

        #region Private methods

        private IEnumerable<List<T>> GenerateAllCombinations<T>(T[] allValues)
        {
            double count = Math.Pow(2, allValues.Length);
            for (int i = 0; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(allValues.Length, '0');
                var list = new List<T>();
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        list.Add(allValues[j]);
                    }
                }
                yield return list;
            }
        }

        private string ToStringModalities(List<ConversationModalityType> list)
        {
            var retValue = string.Empty;
            var isFirst = true;
            foreach(var value in list)
            {
                if(!isFirst)
                {
                    retValue += ", ";
                }
                else
                {
                    isFirst = false;
                }

                retValue += "\"" + value.ToString() + "\"";
            }

            return retValue;
        }

        private bool ConversationContainsAllModalities(List<ConversationModalityType> modalities)
        {
            if(m_conversation.ActiveModalities.Count != modalities.Count)
            {
                return false;
            }

            foreach(var value in modalities)
            {
                var found = false;
                foreach(var active in m_conversation.ActiveModalities)
                {
                    if(active == value)
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
