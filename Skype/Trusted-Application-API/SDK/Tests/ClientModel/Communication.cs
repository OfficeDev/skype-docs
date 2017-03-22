using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.ClientModel.Internal;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class CommunicationTests
    {
        private LoggingContext m_loggingContext;
        private ICommunication m_communication;
        private MockRestfulClient m_restfulClient;
        private Mock<IEventChannel> m_eventChannel;
        private ClientPlatformSettings m_clientPlatformSettings;

        [TestInitialize]
        public async void TestSetup()
        {
            m_loggingContext = new LoggingContext(Guid.NewGuid());
            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;
            m_eventChannel = data.EventChannel;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);
            m_clientPlatformSettings = data.ClientPlatformSettings;

            m_communication = data.ApplicationEndpoint.Application.Communication;
        }

        [TestMethod]
        public async Task ShouldSupportStartMessagingIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessaging);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartMessagingIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessaging);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartMessagingShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartMessagingShouldWork()
        {
            // Given
            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.MessagingInvitations));
        }

        [TestMethod]
        public async Task StartMessagingShouldPassCallbackUrl()
        {
            // Given
            var callbackUrlPassed = false;
            const string callbackUrl = "https://example.com/callback";

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if(operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", callbackUrl).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingShouldPassNullCallbackUrlIfNullGiven()
        {
            // Given
            var callbackUrlPassed = false;
            const string callbackUrl = null;

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", callbackUrl).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingShouldPassNullCallbackContext()
        {
            // Given
            var callbackContextIsNull = false;
            const string callbackUrl = "https://example.com/callback";

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackContextIsNull = input.CallbackContext == null;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", callbackUrl).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackContextIsNull);
        }

        [TestMethod]
        public async Task StartMessagingShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartMessagingAsync("Test message", "sip:user@example.com", "https://example.com/callback");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_MessagingInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartMessagingShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartMessagingAsync("Test message", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartMessagingWithCallbackContextShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), "__callbackcontext__").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldWork()
        {
            // Given
            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), "__callbackcontext__").ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.MessagingInvitations));
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldPassCallbackUrlWhenSpecifiedInSettings()
        {
            // Given
            const string callbackContext = null;
            var callbackUrl = new Uri("https://example.com/callback");
            var callbackUrlPassed = false;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if(operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl.ToString();
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldNotPassCallbackUrlWhenNotSpecifiedInSettings()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackUrlPassed = true;

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl != null;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldPassCallbackContextWhenPassedInParameters()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackContextPassed = false;

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackContextPassed = input.CallbackContext == callbackContext;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackContextPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldNotPassCallbackContextWhenCallbackUrlSpecifiedInSettings()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackUrl = new Uri("https://example.com/callback");
            var callbackContextPassed = true;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackContextPassed = input.CallbackContext != null;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackContextPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldAppendCallbackContextToCallbackUrlWhenCallbackUrlSpecifiedInSettings()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackUrl = new Uri("https://example.com/callback");
            var callbackUrlContainsCallbackContext = false;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlContainsCallbackContext = input.CallbackUrl.Contains(callbackContext);
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlContainsCallbackContext);
        }

        [TestMethod]
        public async Task StartMessagingWithCallbackContextShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), "__callbackContext__");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_MessagingInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartMessagingWithCallbackContextShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartMessagingAsync("Test message", new SipUri("sip:user@example.com"), "__callbackContext__").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task ShouldSupportStartMessagingWithIdentityIfLinkAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessagingWithIdentity);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartMessagingWithIdentityIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartMessagingWithIdentity);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartMessagingWithIdentityShouldThrowIfLinkNotAvailable()
        {
            // Given
            // Setup

            // When
            await m_communication.StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", "https://example.com/callback", "Test user", "sip:user1@example.com").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartMessagingWithIdentityShouldWork()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);

            // When
            IMessagingInvitation invitation = await m_communication
                .StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", "https://example.com/callback", "Test user 1", "sip:user1@example.com")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.MessagingInvitations));
        }

        [TestMethod]
        public async Task StartMessagingWithIdentityShouldPassCallbackUrl()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            var callbackUrlPassed = false;
            const string callbackUrl = "https://example.com/callback";

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication
                .StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", callbackUrl, "Test user 1", "sip:user1@example.com")
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithIdentityShouldPassNullCallbackUrlIfNullGiven()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            var callbackUrlPassed = false;
            const string callbackUrl = null;

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication
                .StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", callbackUrl, "Test user 1", "sip:user1@example.com")
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithIdentityShouldPassNullCallbackContext()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            var callbackUrlPassed = false;
            const string callbackUrl = "https://example.com/callback";

            // If ClientModel starts a MessagingInvitation, deliver the corresponding invitation started event.
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, "Event_MessagingInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as MessagingInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IMessagingInvitation invitation = await m_communication
                .StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", callbackUrl, "Test user 1", "sip:user1@example.com")
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartMessagingWithIdentityShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.MessagingInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // Given
            Task invitationTask = m_communication.StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", "https://example.com/callback", "Test user 1", "sip:user1@example.com");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_MessagingInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartMessagingWithIdentityShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_WithStartMessagingWithIdentity.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartMessagingWithIdentityAsync("Test message", "sip:user@example.com", "https://example.com/callback", "Test user 1", "sip:user1@example.com").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public void ShouldSupportStartAudioIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudio);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartAudioIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudio.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudio);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAudioShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudio.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioShouldWork()
        {
            // Given
            m_restfulClient.HandleRequestProcessed += (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AudioInvitations));
        }

        [TestMethod]
        public async Task StartAudioShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, null, null);
                if(!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_AudioVideoInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAudioShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioShouldPassCallbackUrl()
        {
            // Given
            const string callbackUrl = "https://example.com/callback";
            var callbackUrlPassed = false;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if(operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", callbackUrl).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioShouldPassNullCallbackUrlIfNullGiven()
        {
            // Given
            const string callbackUrl = null;
            var callbackUrlPassed = false;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", callbackUrl).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioShouldPassNullCallbackContext()
        {
            // Given
            const string callbackUrl = "https://example.com/callback";
            var callbackContextPassed = true;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackContextPassed = input.CallbackContext != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication.StartAudioAsync("Test subject", "sip:user@example.com", callbackUrl).ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackContextPassed);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAudioWithCallbackContextShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudio.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldWork()
        {
            // Given
            m_restfulClient.HandleRequestProcessed += (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AudioInvitations));
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_AudioVideoInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAudioWithCallbackContextShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldPassCallbackUrlWhenSpecifiedInSettings()
        {
            // Given
            var callbackUrl = new Uri("https://example.com/callback");
            const string callbackContext = "__callbackcontext__";
            var callbackUrlPassed = false;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if(operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl.StartsWith(callbackUrl.ToString());
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldNotPassCallbackUrlWhenNotSpecifiedInSettings()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackUrlPassed = true;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldPassCallbackContextWhenPassedInParameters()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackContextPassed = false;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackContextPassed = input.CallbackContext != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackContextPassed);
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldNotPassCallbackContextWhenCallbackUrlSpecifiedInSettings()
        {
            // Given
            var callbackUrl = new Uri("https://example.com/callback");
            const string callbackContext = "__callbackcontext__";
            var callbackContextPassed = true;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackContextPassed = input.CallbackContext != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackContextPassed);
        }

        [TestMethod]
        public async Task StartAudioWithCallbackContextShouldAppendCallbackContextToCallbackUrlWhenCallbackUrlSpecifiedInSettings()
        {
            // Given
            var callbackUrl = new Uri("https://example.com/callback");
            const string callbackContext = "__callbackcontext__";
            var callbackUrlContainsCallbackContext = true;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlContainsCallbackContext = input.CallbackUrl.Contains(callbackContext);
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlContainsCallbackContext);
        }

        [TestMethod]
        public void ShouldSupportStartAudioVideoIfLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudioVideo);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportStartAudioVideoIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudioVideo.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            bool supports = m_communication.Supports(CommunicationCapability.StartAudioVideo);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAudioVideoShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudioVideo.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioVideoShouldWork()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AudioVideoInvitations));
        }

        [TestMethod]
        public async Task StartAudioVideoShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_AudioVideoInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAudioVideoShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartAudioVideoAsync("Test subject", "sip:user@example.com", "https://example.com/callback").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioVideoShouldPassCallbackUrl()
        {
            // Given
            const string callbackUrl = "https://example.com/callback";
            var callbackUrlPassed = false;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if(operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", "sip:user@example.com", callbackUrl)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioVideoShouldPassNullCallbackUrlIfNullGiven()
        {
            // Given
            const string callbackUrl = null;
            var callbackUrlPassed = false;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", "sip:user@example.com", callbackUrl)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioVideoShouldPassNullCallbackContext()
        {
            // Given
            const string callbackUrl = "https://example.com/callback";
            var callbackContextPassed = true;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackContextPassed = input.CallbackContext != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", "sip:user@example.com", callbackUrl)
                .ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackContextPassed);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task StartAudioVideoWithCallbackContextShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Communication), HttpMethod.Get, HttpStatusCode.OK, "Communication_NoStartAudioVideo.json");
            await m_communication.RefreshAsync(m_loggingContext).ConfigureAwait(false);

            // When
            await m_communication.StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldWork()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__")
                .ConfigureAwait(false);

            // Then
            Assert.IsNotNull(invitation);
            Assert.IsNotNull(invitation.RelatedConversation);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.AudioVideoInvitations));
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldReturnTaskToWaitForInvitationStartedEvent()
        {
            string invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, null, null);
                if (!string.IsNullOrEmpty(operationId))
                {
                    invitationOperationId = operationId;
                }
            };

            // Given
            Task invitationTask = m_communication.StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__");
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(invitationTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_eventChannel, "Event_AudioVideoInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(invitationTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task StartAudioVideoWithCallbackContextShouldThrowIfInvitationStartedEventNotReceived()
        {
            // Given
            ((Communication)m_communication).WaitForEvents = TimeSpan.FromMilliseconds(200);

            // When
            await m_communication.StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), "__callbackcontext__").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldPassCallbackUrlWhenSpecifiedInSettings()
        {
            // Given
            var callbackUrl = new Uri("https://example.com/callback");
            const string callbackContext = null;
            var callbackUrlPassed = false;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if(operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl == callbackUrl.ToString();
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldNotPassCallbackUrlWhenNotSpecifiedInSettings()
        {
            // Given
            const string callbackContext = null;
            var callbackUrlPassed = true;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlPassed = input.CallbackUrl != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackUrlPassed);
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldPassCallbackContextWhenPassedInParameters()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackContextPassed = false;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackContextPassed = input.CallbackContext == callbackContext;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackContextPassed);
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldNotPassCallbackContextWhenCallbackUrlSpecifiedInSettings()
        {
            // Given
            var callbackUrl = new Uri("https://example.com/callback");
            const string callbackContext = "__callbackcontext__";
            var callbackContextPassed = true;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackContextPassed = input.CallbackContext != null;
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsFalse(callbackContextPassed);
        }

        [TestMethod]
        public async Task StartAudioVideoWithCallbackContextShouldAppendCallbackContextToCallbackUrlWhenCallbackUrlSpecifiedInSettings()
        {
            // Given
            var callbackUrl = new Uri("https://example.com/callback");
            const string callbackContext = "__callbackcontext__";
            var callbackUrlContainsCallbackContext = false;

            m_clientPlatformSettings.SetCustomizedCallbackurl(callbackUrl);

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.AudioVideoInvitations, HttpMethod.Post, "Event_AudioVideoInvitationStarted.json", m_eventChannel);
                if (operationId != null)
                {
                    var input = args.Input as AudioVideoInvitationInput;
                    callbackUrlContainsCallbackContext = input.CallbackUrl.Contains(callbackContext);
                }
            };

            // When
            IAudioVideoInvitation invitation = await m_communication
                .StartAudioVideoAsync("Test subject", new SipUri("sip:user@example.com"), callbackContext)
                .ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackUrlContainsCallbackContext);
        }
    }
}
