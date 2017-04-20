using System;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    [Serializable]
    public class PlatformserviceApplicationException : Exception
    {
        public PlatformserviceApplicationException(string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
        {
        }
    }

    [Serializable]
    public class PlatformServiceAzureStorageException : PlatformserviceApplicationException
    {
        public PlatformServiceAzureStorageException(string errorMessage, Exception innerException = null) : base(errorMessage, innerException)
        {
        }
    }
}
