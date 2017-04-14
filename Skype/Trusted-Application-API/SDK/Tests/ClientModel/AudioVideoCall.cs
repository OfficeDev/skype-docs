using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class AudioVideoCallTests
    {
        private LoggingContext m_loggingContext;
        private Mock<IEventChannel> m_mockEventChannel;
        private MockRestfulClient m_restfulClient;
        private IAudioVideoCall m_audioVideoCall;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_mockEventChannel = data.EventChannel;
            m_restfulClient = data.RestfulClient;

            ApplicationEndpoint applicationEndpoint = data.ApplicationEndpoint;
            await applicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await applicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);

            IAudioVideoInvitation invitation = null;

            applicationEndpoint.HandleIncomingAudioVideoCall += (sender, args) => invitation = args.NewInvite;

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_IncomingAudioCall.json");
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected.json");

            m_audioVideoCall = invitation.RelatedConversation.AudioVideoCall;
        }

        [TestMethod]
        public void ShouldRaiseAudioVideoFlowConnectedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAdded.json");
            var eventReceived = false;
            m_audioVideoCall.AudioVideoFlowConnected += (sender, args) => eventReceived = true;

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldRaiseAudioVideoFlowConnectedEventEvenIfAudioVideoFlowIsAddedAsConnected()
        {
            // Given
            var eventReceived = false;
            m_audioVideoCall.AudioVideoFlowConnected += (sender, args) => eventReceived = true;

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAddedAsConnected.json");

            // Then
            Assert.IsTrue(eventReceived);
        }

        [TestMethod]
        public void ShouldRaiseAudioVideoFlowConnectedEventOnlyOnceIfSameEventHandlerRegisteredMultipleTimes()
        {
            // Given
            var eventsReceived = 0;
            EventHandler<AudioVideoFlowUpdatedEventArgs> handler = (sender, args) => Interlocked.Increment(ref eventsReceived);
            m_audioVideoCall.AudioVideoFlowConnected += handler;
            m_audioVideoCall.AudioVideoFlowConnected += handler;

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAddedAsConnected.json");

            // Then
            Assert.AreEqual(1, eventsReceived);
        }

        [TestMethod]
        public void ShouldRaiseAudioVideoFlowConnectedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var eventsReceived = 0;
            var lastEvent = 0;
            m_audioVideoCall.AudioVideoFlowConnected += (sender, args) => { eventsReceived++; lastEvent = 1; };
            m_audioVideoCall.AudioVideoFlowConnected += (sender, args) => { eventsReceived++; lastEvent = 2; };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAddedAsConnected.json");

            // Then
            Assert.AreEqual(2, eventsReceived);
            Assert.AreEqual(2, lastEvent);
        }

        [TestMethod]
        public void ShouldExposeCallContext()
        {
            // Given
            const string expected = "eyJSZW1vdGVQYXJ0aWNpcGFudFVyaSI6InNpcDpCTDIwUjAwTUVEMDEuaW5mcmEubHluYy5jb21AcmVzb3VyY2VzLmx5bmMuY29t"+
                           "O2dydXU7b3BhcXVlPXNydnI6TWVkaWF0aW9uU2VydmVyOl9UTWZGeTJtd1Zhc3VNaWk4NlNWY1FBQTtncmlkPTE4ZWJkMGRlOWI3"+
                           "OTQ4YTU5OTc3YjY1NWI2NTk5NzM3IiwiQ2FsbElkIjoiNjUyZDlkNTMtZjA2Ny00OTM1LWEzYmMtMmZjYjA1NzZhNDA3IiwiTG9j"+
                           "YWxUYWciOiJiYjMxYThkMTMyIiwiUmVtb3RlVGFnIjoiMzNlNGVlMjZlIn0=";

            // When
            string callContext = m_audioVideoCall.CallContext;

            // Then
            Assert.AreEqual(expected, callContext);
        }

        [TestMethod]
        public void ShouldExposeAudioVideoFlowProperty()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAdded.json");

            // Then
            Assert.IsNotNull(m_audioVideoCall.AudioVideoFlow);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task TransferAsyncShouldThrowWhenCapabilityNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected_NoActionLink.json");

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncShouldReturnOnlyOnTransferStartedEvent()
        {
            // Given
            var transferOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, null, null);
                if(operationId != null)
                {
                    transferOperationId = operationId;
                }
            };

            Task transferTask = m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), null, m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            Assert.IsFalse(transferTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_TransferStarted.json", transferOperationId);

            // Then
            Assert.IsTrue(transferTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task TransferAsyncShouldThrowIfTransferStartedEventNotReceivedInTime()
        {
            // Given
            // Set wait time to 300 milliseconds so that the test doesn't run for too long
            ((AudioVideoCall)m_audioVideoCall).WaitForEvents = TimeSpan.FromMilliseconds(300);

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), null, m_loggingContext).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncShouldMakeHttpRequest()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), null, m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.Transfer));
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task TransferAsyncToUserShouldThrowWhenCapabilityNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected_NoActionLink.json");

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncToUserShouldReturnOnlyOnTransferStartedEvent()
        {
            // Given
            var transferOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, null, null);
                if (operationId != null)
                {
                    transferOperationId = operationId;
                }
            };

            Task transferTask = m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            Assert.IsFalse(transferTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_TransferStarted.json", transferOperationId);

            // Then
            Assert.IsTrue(transferTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task TransferAsyncToUserShouldThrowIfTransferStartedEventNotReceivedInTime()
        {
            // Given
            // Set wait time to 300 milliseconds so that the test doesn't run for too long
            ((AudioVideoCall)m_audioVideoCall).WaitForEvents = TimeSpan.FromMilliseconds(300);

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncToUserShouldWorkWithNullLoggingContext()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), m_loggingContext).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncToUserShouldMakeHttpRequest()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.Transfer));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TransferAsyncToUserShouldThrowIfNullTransferTarget()
        {
            // Given
            // Setup

            // When
            await m_audioVideoCall.TransferAsync((SipUri)null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task TransferAsyncByReplacingShouldThrowWhenCapabilityNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected_NoActionLink.json");

            // When
            await m_audioVideoCall.TransferAsync("__fakecallcontext__", m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncByReplacingShouldReturnOnlyOnTransferStartedEvent()
        {
            // Given
            var transferOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, null, null);
                if (operationId != null)
                {
                    transferOperationId = operationId;
                }
            };

            Task transferTask = m_audioVideoCall.TransferAsync("__fakecallcontext__", m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            Assert.IsFalse(transferTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_TransferStarted.json", transferOperationId);

            // Then
            Assert.IsTrue(transferTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task TransferAsyncByReplacingShouldThrowIfTransferStartedEventNotReceivedInTime()
        {
            // Given
            // Set wait time to 300 milliseconds so that the test doesn't run for too long
            ((AudioVideoCall)m_audioVideoCall).WaitForEvents = TimeSpan.FromMilliseconds(300);

            // When
            await m_audioVideoCall.TransferAsync("__fakecallcontext__", m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncByReplacingShouldWorkWithNullLoggingContext()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.TransferAsync("__fakecallcontext__", m_loggingContext).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task TransferAsyncByReplacingShouldMakeHttpRequest()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.TransferAsync("__fakecallcontext__", m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.Transfer));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task TransferAsyncByReplacingShouldThrowIfNullReplacesCallContext()
        {
            // Given
            // Setup

            // When
            await m_audioVideoCall.TransferAsync((string)null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task TerminateAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected_NoActionLink.json");

            // When
            await m_audioVideoCall.TerminateAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task TerminateAsyncShouldMakeHttpRequest()
        {
            // Given
            // Setup

            // When
            await m_audioVideoCall.TerminateAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.TerminateAudioVideoCall));
        }

        [TestMethod]
        public async Task TerminateAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            // Setup

            // When
            await m_audioVideoCall.TerminateAsync(null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task EstablishAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected_NoActionLink.json");

            // When
            await m_audioVideoCall.EstablishAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task EstablishAsyncShouldMakeHttpRequest()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.EstablishAudioVideoCall, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.EstablishAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.EstablishAudioVideoCall));
        }

        [TestMethod]
        public async Task EstablishAsyncShouldReturnATaskToWaitForInvitationStartedEvent()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            var establishOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.EstablishAudioVideoCall, HttpMethod.Post, null, null);
                if(operationId != null)
                {
                    establishOperationId = operationId;
                }
            };

            Task establishTask = m_audioVideoCall.EstablishAsync(m_loggingContext);
            await Task.Delay(TimeSpan.FromMilliseconds(300)).ConfigureAwait(false);
            Assert.IsFalse(establishTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_AudioVideoInvitationStarted.json", establishOperationId);

            // Then
            Assert.IsTrue(establishTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task EstablishAsyncShouldThrowIfInvitationStartedEventNotReceivedInTime()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            ((AudioVideoCall)m_audioVideoCall).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_audioVideoCall.EstablishAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task EstablishAsyncShouldWorkWithNullLoggingContext()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.EstablishAudioVideoCall, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_mockEventChannel);

            // When
            await m_audioVideoCall.EstablishAsync(null).ConfigureAwait(false);

            // Then
            // No exception is thrown
        }

        [TestMethod]
        public async Task WaitForAVFlowConnectedShouldReturnWhenFlowConnected()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAdded.json");
            Task flowConnectedTask = m_audioVideoCall.WaitForAVFlowConnected();
            await Task.Delay(300).ConfigureAwait(false);
            Assert.IsFalse(flowConnectedTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");

            // Then
            Assert.IsTrue(flowConnectedTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeoutException))]
        public async Task WaitForAVFlowConnectedShouldThrowOnTimeout()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAdded.json");

            // When
            await m_audioVideoCall.WaitForAVFlowConnected(1).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public void ShouldSupportTransferWhenLinkIsAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_audioVideoCall.Supports(AudioVideoCallCapability.Transfer);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotSupportTransferWhenLinkIsNotAvailable()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supported = m_audioVideoCall.Supports(AudioVideoCallCapability.Transfer);

            // Then
            Assert.IsFalse(supported);
        }

        [TestMethod]
        public void ShouldSupportTerminateWhenLinkIsAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_audioVideoCall.Supports(AudioVideoCallCapability.Terminate);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public async Task ShouldNotTerminateSupportWhenLinkIsNotAvailable()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supported = m_audioVideoCall.Supports(AudioVideoCallCapability.Terminate);

            // Then
            Assert.IsFalse(supported);
        }

        [TestMethod]
        public async Task ShouldSupportEstablishWhenLinkIsAvailable()
        {
            // Given
            await m_audioVideoCall.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supported = m_audioVideoCall.Supports(AudioVideoCallCapability.Establish);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public void ShouldNotSupportEstablishWhenLinkIsNotAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_audioVideoCall.Supports(AudioVideoCallCapability.Establish);

            // Then
            Assert.IsFalse(supported);
        }
    }
}
