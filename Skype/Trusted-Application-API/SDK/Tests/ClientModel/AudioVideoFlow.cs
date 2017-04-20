using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StopPromptsAsyncShouldThrowIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            await m_audioVideoFlow.StopPromptsAsync(m_loggingContext).ConfigureAwait(false);

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
        public async Task StopPromptsShouldReturnOnlyOnPromptCompletedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");
            TaskCompletionSource<bool> requestReceived = new TaskCompletionSource<bool>();
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if (args.Uri == new Uri(DataUrls.StopPrompts) && args.Method == HttpMethod.Post)
                {
                    requestReceived.SetResult(true);
                }
            };

            // When
            Task promptTask = m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt"), m_loggingContext);
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_PromptStarted.json");

            Task stopTask = m_audioVideoFlow.StopPromptsAsync(m_loggingContext);
            await requestReceived.Task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(stopTask.IsCompleted);
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_PromptStopped.json");
            await stopTask.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);

            // Then
            Assert.IsTrue(promptTask.IsCompleted);
        }

        [TestMethod]
        public async Task StopPromptsShouldReturnOnlyOnAllPromptsCompletedEvent()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");
            TaskCompletionSource<bool> requestReceived = new TaskCompletionSource<bool>();
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if (args.Uri == new Uri(DataUrls.StopPrompts) && args.Method == HttpMethod.Post)
                {
                    requestReceived.SetResult(true);
                }
            };

            const string prompt2Url = "https://example.com/prompt2";
            m_restfulClient.HandleRequestReceived += (sender, args) =>
            {
                PlayPromptInput input = args.Input as PlayPromptInput;
                if (input?.PromptUrl == prompt2Url)
                {
                    args.Response = new HttpResponseMessage(HttpStatusCode.Created);
                    args.Response.Headers.Add("Location",
                        "https://webpoolbl20r04.infra.lync.com/platformservice/tgt-8c81281c925a5c2ea02ec14ac1b492c6/v1/applications/1393347000/communication/conversations/869ce4f6-0076-483a-a7c1-968f6b935afe/audioVideo/audioVideoFlow/prompts/08b7bd6e-0e79-4961-8981-116ac9ddd152?endpointId=sip:monitoringaudio@0mcorp2cloudperf.onmicrosoft.com");
                }
            };

            // When
            #pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt"), m_loggingContext);
            m_audioVideoFlow.PlayPromptAsync(new Uri("https://example.com/prompt2"), m_loggingContext);
            #pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_PromptStarted.json");
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_Prompt2Started.json");

            Task stopTask = m_audioVideoFlow.StopPromptsAsync(m_loggingContext);
            await requestReceived.Task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);

            // none of the prompt tasks is stopped yet
            Assert.IsFalse(stopTask.IsCompleted);

            // receiving stopped event for promptTask
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_PromptStopped.json");

            // yet to receiving stopped event for promptTask2
            Assert.IsFalse(stopTask.IsCompleted);

            // receiving stopped event for promptTask2
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_Prompt2Stopped.json");

            // stop task must be completed
            await stopTask.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsTrue(stopTask.IsCompleted);
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
        public async Task StopPromptsShouldWorkWithNullLoggingContext()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");
            TaskCompletionSource<bool> requestReceived = new TaskCompletionSource<bool>();
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                if (args.Uri == new Uri(DataUrls.StopPrompts) && args.Method == HttpMethod.Post)
                {
                    requestReceived.SetResult(true);
                }
            };

            // When
            Task stopTask = m_audioVideoFlow.StopPromptsAsync(null);

            // Then
            await requestReceived.Task.TimeoutAfterAsync(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            await stopTask.ConfigureAwait(false);
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
        public void ShouldSupportStopPromptsWhenLinkIsAvailable()
        {
            // Given
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoFlowConnected.json");

            // When
            bool supported = m_audioVideoFlow.Supports(AudioVideoFlowCapability.StopPrompts);

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

        [TestMethod]
        public void ShouldNotSupportStopPromptsWhenLinkIsNotAvailable()
        {
            // Given
            // Setup

            // When
            bool supported = m_audioVideoFlow.Supports(AudioVideoFlowCapability.StopPrompts);

            // Then
            Assert.IsFalse(supported);
        }
    }
}
