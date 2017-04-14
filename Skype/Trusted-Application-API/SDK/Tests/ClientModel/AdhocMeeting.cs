using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rtc.Internal.Platform.ResourceContract;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class AdhocMeetingTests
    {
        private IApplication m_application;
        private LoggingContext m_loggingContext;
        private MockRestfulClient m_restfulClient;
        private IAdhocMeeting m_adhocMeeting;
        private ClientPlatformSettings m_clientPlatformSettings;
        private Mock<IEventChannel> m_mockEventChannel;

        [TestInitialize]
        public async void TestSetup()
        {
            TestHelper.InitializeTokenMapper();

            m_loggingContext = new LoggingContext(Guid.NewGuid());

            var data = TestHelper.CreateApplicationEndpoint();
            m_restfulClient = data.RestfulClient;
            m_clientPlatformSettings = data.ClientPlatformSettings;
            m_mockEventChannel = data.EventChannel;

            await data.ApplicationEndpoint.InitializeAsync(m_loggingContext).ConfigureAwait(false);
            await data.ApplicationEndpoint.InitializeApplicationAsync(m_loggingContext).ConfigureAwait(false);
            m_application = data.ApplicationEndpoint.Application;

            m_adhocMeeting = await m_application.CreateAdhocMeetingAsync(m_loggingContext, new AdhocMeetingCreationInput("subject")).ConfigureAwait(false);
        }

        [TestMethod]
        public void ShouldExposeJoinUrl()
        {
            // Given
            // Setup

            // Then
            Assert.IsNotNull(m_adhocMeeting.JoinUrl);
        }

        [TestMethod]
        public void ShouldExposeOnlineMeetingUri()
        {
            // Given
            // Setup

            // Then
            Assert.IsNotNull(m_adhocMeeting.OnlineMeetingUri);
        }

        [TestMethod]
        public void ShouldExposeSubject()
        {
            // Given
            // Setup

            // Then
            Assert.IsNotNull(m_adhocMeeting.Subject);
        }

        [TestMethod]
        public void ShouldSupportJoinAdhocMeetingWhenLinkAvailable()
        {
            // Given
            // Setup

            // When
            bool supports = m_adhocMeeting.Supports(AdhocMeetingCapability.JoinAdhocMeeting);

            // Then
            Assert.IsTrue(supports);
        }

        [TestMethod]
        public async Task ShouldNotSupportJoinAdhocMeetingWhenLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AdhocMeeting), HttpMethod.Post, HttpStatusCode.Created, "AdhocMeeting_NoJoinAdhocMeetingLink.json");
            m_adhocMeeting = await m_application.CreateAdhocMeetingAsync(m_loggingContext, new AdhocMeetingCreationInput("subject")).ConfigureAwait(false);

            // When
            bool supports = m_adhocMeeting.Supports(AdhocMeetingCapability.JoinAdhocMeeting);

            // Then
            Assert.IsFalse(supports);
        }

        [TestMethod]
        [ExpectedException(typeof(CapabilityNotAvailableException))]
        public async Task JoinAdhocMeetingShouldThrowIfLinkNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.AdhocMeeting), HttpMethod.Post, HttpStatusCode.Created, "AdhocMeeting_NoJoinAdhocMeetingLink.json");
            m_adhocMeeting = await m_application.CreateAdhocMeetingAsync(m_loggingContext, new AdhocMeetingCreationInput("subject")).ConfigureAwait(false);

            // When
            await m_adhocMeeting.JoinAdhocMeeting(m_loggingContext, "callbackcontext").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }

        [TestMethod]
        public async Task JoinAdhocMeetingShouldMakeHttpRequest()
        {
            // Given
            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.JoinAdhocMeeting, HttpMethod.Post, "Event_OnlineMeetingInvitationStarted.json", m_mockEventChannel);

            // When
            await m_adhocMeeting.JoinAdhocMeeting(m_loggingContext, "callbackcontext").ConfigureAwait(false);

            // Then
            Assert.IsTrue(m_restfulClient.RequestsProcessed("POST " + DataUrls.JoinAdhocMeeting));
        }

        [TestMethod]
        public async Task JoinAdhocMeetingShouldPassCustomizedCallbackUrlAndAppendCallbackContextToItInHttpRequest()
        {
            // Given
            const string callbackUrl = "https://example.com/customizedcallbackurl";
            const string callbackContext = "__callbackcontext__";
            m_clientPlatformSettings.CustomizedCallbackUrl = callbackUrl;

            var customizedCallbackUrlPassed = false;

            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.JoinAdhocMeeting, HttpMethod.Post, "Event_OnlineMeetingInvitationStarted.json", m_mockEventChannel);
                if (operationId != null)
                {
                    var passed = ((JoinMeetingInvitationInput)args.Input).CallbackUrl;
                    customizedCallbackUrlPassed = passed.StartsWith(callbackUrl) && passed.EndsWith(callbackContext);
                }
            };

            // When
            await m_adhocMeeting.JoinAdhocMeeting(m_loggingContext, callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(customizedCallbackUrlPassed);
        }

        [TestMethod]
        public async Task JoinAdhocMeetingShouldPassCallbackContextInHttpRequest()
        {
            // Given
            const string callbackContext = "__callbackcontext__";
            var callbackContextPassed = false;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                var operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.JoinAdhocMeeting, HttpMethod.Post, "Event_OnlineMeetingInvitationStarted.json", m_mockEventChannel);
                if (operationId != null && ((JoinMeetingInvitationInput)args.Input).CallbackContext == callbackContext)
                {
                    callbackContextPassed = true;
                }
            };

            // When
            await m_adhocMeeting.JoinAdhocMeeting(m_loggingContext, callbackContext).ConfigureAwait(false);

            // Then
            Assert.IsTrue(callbackContextPassed);
        }

        [TestMethod]
        public async Task JoinAdhocMeetingShouldReturnOnlyOnOnlineMeetingInvitationStartedEvent()
        {
            // Given
            var invitationOperationId = string.Empty;
            m_restfulClient.HandleRequestProcessed += (sender, args) =>
            {
                string operationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.JoinAdhocMeeting, HttpMethod.Post, null, null);
                if (operationId != null)
                {
                    invitationOperationId = operationId;
                }
            };

            Task joinTask = m_adhocMeeting.JoinAdhocMeeting(m_loggingContext, "callbackcontext");
            await Task.Delay(TimeSpan.FromMilliseconds(100)).ConfigureAwait(false);
            Assert.IsFalse(joinTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_OnlineMeetingInvitationStarted.json", invitationOperationId);

            // Then
            Assert.IsTrue(joinTask.IsCompleted);
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task JoinAdhocMeetingShouldThrowIfOnlineMeetingInvitationStartedEventNotReceived()
        {
            // Given
            // Set wait time to 300 milliseconds so that the test doesn't run for too long
            ((AdhocMeeting)m_adhocMeeting).WaitForEvents = TimeSpan.FromMilliseconds(300);

            // When
            await m_adhocMeeting.JoinAdhocMeeting(m_loggingContext, "callbackcontext").ConfigureAwait(false);

            // Then
            // Exception is thrown
        }
    }
}
