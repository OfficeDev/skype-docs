using System;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class DiscoverTests
    {
        private MockRestfulClient m_restfulClient;
        private LoggingContext m_loggingContext;
        private Discover m_discover;

        private const string c_discoverUrl = "https://noammeetings.resources.lync.com/platformservice/discover?deploymentpreference=Weekly";

        [TestInitialize]
        public void TestSetup()
        {
            m_restfulClient = new MockRestfulClient();
            Logger.RegisterLogger(new ConsoleLogger());
            m_loggingContext = new LoggingContext(Guid.NewGuid());

            Uri discoverUri = TestHelper.DiscoverUri;
            Uri baseUri = UriHelper.GetBaseUriFromAbsoluteUri(discoverUri.ToString());

            m_discover = new Discover(m_restfulClient, baseUri, discoverUri, this);
            TestHelper.InitializeTokenMapper();
        }

        [TestMethod]
        public async Task RefreshAndInitializeShouldPopulateApplication()
        {
            // Given
            Assert.IsNull(m_discover.Application);

            // When
            await m_discover.RefreshAndInitializeAsync(TestHelper.ApplicationEndpointUri.ToString(), m_loggingContext).ConfigureAwait(false);

            // Given
            Assert.IsNotNull(m_discover.Application);
        }

        [TestMethod]
        public async Task RefreshAndInitializeShouldMakeHttpCall()
        {
            // Given
            Assert.IsFalse(m_restfulClient.RequestsProcessed("GET " + c_discoverUrl));

            // When
            await m_discover.RefreshAndInitializeAsync(TestHelper.ApplicationEndpointUri.ToString(), m_loggingContext).ConfigureAwait(false);

            // Given
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET " + c_discoverUrl));
        }
    }
}
