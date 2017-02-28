using System;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{

    /// <summary>
    /// The AdhocMeetingResource Resource.
    /// </summary>
    public class AdhocMeetingToken
    {
        /// <summary>
        /// Gets or sets the discover uri.
        /// </summary>
        public string DiscoverUri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the anonymous token.
        /// </summary>
        public string OnlineMeetingUri
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the organizer uri
        /// </summary>
        public string OrganizerUri
        {
            get;
            set;
        }

        public string JoinUrl
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the expire time.
        /// </summary>
        public DateTime? ExpireTime
        {
            get;
            set;
        }
    }
}
