using Microsoft.SfB.PlatformService.SDK.ClientModel;

namespace Microsoft.SfB.PlatformService.SDK.Tests
{
    internal class MockRestfulClientFactory : IRestfulClientFactory
    {
        private readonly IRestfulClient m_restfulClient;

        public MockRestfulClientFactory()
        {
            m_restfulClient = new MockRestfulClient();
        }

        public IRestfulClient GetRestfulClient(OAuthTokenIdentifier oauthIdentity, ITokenProvider tokenProvider)
        {
            return m_restfulClient;
        }
    }
}
