using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// Every event which goes through Eventchannel has an <see cref="EventContext"/> which acts like a key to the event.
    /// </summary>
    public class EventContext
    {
        internal EventContext()
        {
        }

        /// <summary>
        /// Gets or sets the SenderResourceName.
        /// </summary>
        public string SenderResourceName { get; internal set; }

        /// <summary>
        /// Gets or sets the SenderHref.
        /// </summary>
        public string SenderHref { get; internal set; }

        /// <summary>
        /// Gets or sets the BaseUri.
        /// </summary>
        public Uri BaseUri { get; internal set; }

        /// <summary>
        /// Gets or sets the Event Resource Name.
        /// </summary>
        public string EventResourceName { get; internal set; }

        /// <summary>
        /// Gets or sets the EventFullHref.
        /// </summary>
        public Uri EventFullHref { get; internal set; }

        /// <summary>
        /// Gets or sets the EventEntity.
        /// </summary>
        public EventEntity EventEntity { get; internal set; }

        /// <summary>
        /// Gets or sets the LoggingContext.
        /// </summary>
        public LoggingContext LoggingContext { get; internal set; }
    }
}
