using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    /// <summary>
    /// Unit tests for <see cref="Transfer"/>
    /// </summary>
    [TestClass]
    public class TransferTests
    {
        private LoggingContext m_loggingContext;
        private Mock<IEventChannel> m_mockEventChannel;
        private MockRestfulClient m_restfulClient;
        private ITransfer m_transfer;
        private string m_transferOperationId;

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

            IConversation conversation = null;
            applicationEndpoint.HandleIncomingAudioVideoCall += (sender, args) => conversation = args.NewInvite.RelatedConversation;

            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_IncomingAudioCall.json");
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_ConversationConnected_WithAudio.json");
            TestHelper.RaiseEventsFromFile(m_mockEventChannel, "Event_AudioVideoConnected.json");

            m_restfulClient.HandleRequestProcessed +=
                (sender, args) => m_transferOperationId = TestHelper.RaiseEventsOnHttpRequest(args, DataUrls.Transfer, HttpMethod.Post, "Event_TransferStarted.json", m_mockEventChannel);

            m_transfer = await conversation.AudioVideoCall.TransferAsync(new SipUri("sip:user@example.com"), null, m_loggingContext).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task ShouldReturnTaskToWaitForTransferCompleteEvent()
        {
            // Given
            Task transferTask = m_transfer.WaitForTransferCompleteAsync();
            await Task.Delay(TimeSpan.FromMilliseconds(200)).ConfigureAwait(false);
            Assert.IsFalse(transferTask.IsCompleted);

            // When
            TestHelper.RaiseEventsFromFileWithOperationId(m_mockEventChannel, "Event_TransferCompleted.json", m_transferOperationId);

            // Then
            Assert.IsTrue(transferTask.IsCompleted);
        }
    }
}
