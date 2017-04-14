using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.SfB.PlatformService.SDK.Tests.ClientModel
{
    [TestClass]
    public class ApplicationsTests
    {
        private MockRestfulClient m_restfulClient;
        private LoggingContext m_loggingContext;
        private IApplications m_applications;

        [TestInitialize]
        public async void TestSetup()
        {
            m_restfulClient = new MockRestfulClient();
            Logger.RegisterLogger(new ConsoleLogger());
            Logger.RegisterLogger(new ConsoleLogger());

            m_loggingContext = new LoggingContext(Guid.NewGuid());
            TestHelper.InitializeTokenMapper();

            Uri discoverUri = TestHelper.DiscoverUri;
            Uri baseUri = UriHelper.GetBaseUriFromAbsoluteUri(discoverUri.ToString());
            SipUri ApplicationEndpointId = TestHelper.ApplicationEndpointUri;

            var discover = new Discover(m_restfulClient, baseUri, discoverUri, this);
            await discover.RefreshAndInitializeAsync(m_loggingContext, ApplicationEndpointId.ToString()).ConfigureAwait(false);

            m_applications = discover.Applications;
        }

        [TestMethod]
        public async Task ShouldInitializeProperly()
        {
            // Given
            Assert.IsNull(m_applications.Application);
            Assert.IsFalse(m_restfulClient.RequestsProcessed("GET " + DataUrls.Applications));

            // When
            await m_applications.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            Assert.IsNotNull(m_applications.Application);
            Assert.IsTrue(m_restfulClient.RequestsProcessed("GET " + DataUrls.Applications));
        }

        [TestMethod]
        [ExpectedException(typeof(RemotePlatformServiceException))]
        public async Task ShouldFailInitializationIfApplicationResourceNotAvailable()
        {
            // Given
            m_restfulClient.OverrideResponse(new Uri(DataUrls.Applications), HttpMethod.Get, HttpStatusCode.OK, "Applications_NoApplication.json");

            // When
            await m_applications.RefreshAndInitializeAsync(m_loggingContext).ConfigureAwait(false);

            // Then
            // Exception is thrown
        }
    }
}