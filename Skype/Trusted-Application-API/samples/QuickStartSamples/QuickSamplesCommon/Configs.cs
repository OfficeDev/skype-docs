using System.Configuration;

namespace QuickSamplesCommon
{
    public static class QuickSamplesConfig
    {
        public static string ApplicationEndpointId { get; }

        public static string AAD_ClientId { get; }

        public static string AAD_ClientSecret { get; }

        public static string MyCallbackUri { get; }

        public static string LocalServerListeningAddress { get; }

        static QuickSamplesConfig()
        {
            ApplicationEndpointId = ConfigurationManager.AppSettings["ApplicationEndpointId"];
            AAD_ClientId = ConfigurationManager.AppSettings["AAD_ClientId"];
            AAD_ClientSecret = ConfigurationManager.AppSettings["AAD_ClientSecret"];
            MyCallbackUri = ConfigurationManager.AppSettings["MyCallbackUri"];
            LocalServerListeningAddress = ConfigurationManager.AppSettings["LocalServerListeningAddress"];
        }
    }
}
