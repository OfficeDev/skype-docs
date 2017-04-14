using System;
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
    public class AudioVideoFlowTests
    {
        private LoggingContext m_loggingContext;
        private Mock<IEventChannel> m_mockEventChannel;
        private MockRestfulClient m_restfulClient;
        private IAudioVideoFlow m_audioVideoFlow;

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
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowAdded.json");

            m_audioVideoFlow = invitation.RelatedConversation.AudioVideoCall.AudioVideoFlow;
        }

        [TestMethod]
        public void ShouldRaiseToneReceivedEvent()
        {
            // Given
            var toneReceived = false;
            m_audioVideoFlow.ToneReceivedEvent += (sender, args) => toneReceived = true;

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ToneReceived.json");

            // Then
            Assert.IsTrue(toneReceived);
        }

        [TestMethod]
        public void ShouldRaiseToneReceivedEventOnlyOnceIfSameEventHandlerRegisteredMultipleTimes()
        {
            // Given
            var tonesReceived = 0;
            EventHandler<ToneReceivedEventArgs> handler = (sender, args) => Interlocked.Increment(ref tonesReceived);
            m_audioVideoFlow.ToneReceivedEvent += handler;
            m_audioVideoFlow.ToneReceivedEvent += handler;

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ToneReceived.json");

            // Then
            Assert.AreEqual(1, tonesReceived);
        }

        [TestMethod]
        public void ShouldRaiseToneReceivedEventForAllRegisteredEventHandlersInOrder()
        {
            // Given
            var tonesReceived = 0;
            var lastTone = 0;
            m_audioVideoFlow.ToneReceivedEvent += (sender, args) => { ++tonesReceived; lastTone = 1; };
            m_audioVideoFlow.ToneReceivedEvent += (sender, args) => { ++tonesReceived; lastTone = 2; };

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ToneReceived.json");

            // Then
            Assert.AreEqual(2, tonesReceived);
            Assert.AreEqual(2, lastTone);
        }

        [TestMethod]
        public void ShouldBeAbleToDeregisterEventHandlerFromToneReceivedEvent()
        {
            // Given
            var toneReceived = false;
            EventHandler<ToneReceivedEventArgs> handler = (sender, args) => toneReceived = true;
            m_audioVideoFlow.ToneReceivedEvent += handler;
            m_audioVideoFlow.ToneReceivedEvent -= handler;

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ToneReceived.json");

            // Then
            Assert.IsFalse(toneReceived);
        }

        [TestMethod]
        public void StatePropertyShouldGetUpdatedOnEvent()
        {
            // Given
            Assert.AreEqual(FlowState.Disconnected, m_audioVideoFlow.State);

            // When
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");

            // then
            Assert.AreEqual(FlowState.Connected, m_audioVideoFlow.State);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task PlayPromptAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            await m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt"), m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task PlayPromptShouldThrowIfInputUriIsNull()
        {
            // Given
            // Setup

            // When
            await m_audioVideoFlow.PlayPromptAsync(null, m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task PlayPromptShouldMakeHttpRequest()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");
            TaskCompletionSource<bool> requestReceived = new TaskCompletionSource<bool>();
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if(args.Uri == new Uri(DataUrls.StartPrompt) && args.Method == HttpMethod.Post)
                {
                    requestReceived.SetResult(true);
                }
            };

            // When
            Task promptTask = m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt"), m_loggingContext);

            // Then
            await requestReceived.Task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task PlayPromptShouldReturnOnlyOnPromptCompletedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");
            TaskCompletionSource<bool> requestReceived = new TaskCompletionSource<bool>();
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if (args.Uri == new Uri(DataUrls.StartPrompt) && args.Method == HttpMethod.Post)
                {
                    requestReceived.SetResult(true);
                }
            };

            // When
            Task promptTask = m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt"), m_loggingContext);
            Assert.IsFalse(promptTask.IsCompleted);

            await requestReceived.Task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(promptTask.IsCompleted);

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_PromptStarted.json");
            Assert.IsFalse(promptTask.IsCompleted);

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_PromptCompleted.json");

            // Then
            Assert.IsTrue(promptTask.IsCompleted);
        }

        [TestMethod]
        public async Task PlayPromptShouldWorkWithNullLoggingContext()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");
            TaskCompletionSource<bool> requestReceived = new TaskCompletionSource<bool>();
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if (args.Uri == new Uri(DataUrls.StartPrompt) && args.Method == HttpMethod.Post)
                {
                    requestReceived.SetResult(true);
                }
            };

            // When
            Task promptTask = m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt"), null);

            // Then
            await requestReceived.Task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
        }

        [TestMethod]
        public void ShouldSupportPlayPromptWhenLinkIsAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");

            // When
            bool supported = m_audioVideoFlow.Supports(AudioVideoFlowCapability.PlayPrompt);

            // Then
            Assert.IsTrue(supported);
        }

        [TestMethod]
        public void ShouldNotSupportPlayPromptWhenLinkIsNotAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_audioVideoFlow.Supports(AudioVideoFlowCapability.PlayPrompt);

            // Then
            Assert.IsFalse(supported);
        }
    }
}
