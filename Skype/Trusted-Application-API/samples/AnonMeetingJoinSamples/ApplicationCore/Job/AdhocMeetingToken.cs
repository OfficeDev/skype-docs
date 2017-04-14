using System;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{

    /// <summary>
    /// The AdhocMeetingResource Resource.
    /// </summary>
    public class AdhocMeetingToken
    {
        /// <summary>
        /// Gets or sets the anonymous token.
        /// </summary>
        public string OnlineMeetingUri
        {
            get;
            set;
        }

        public string JoinUrl
        {
            get;
            set;
        }
    }
}
