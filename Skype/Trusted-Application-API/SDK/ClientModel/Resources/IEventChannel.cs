using Microsoft.Rtc.Internal.RestAPI.ResourceModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;

namespace Microsoft.SfB.PlatformService.SDK.ClientModel
{
    /// <summary>
    /// The event channel args
    /// </summary>
    public class EventsChannelArgs : EventArgs
    {
        /// <summary>
        /// Gets the callback HTTP request.
        /// </summary>
        /// <value>The callback HTTP request.</value>
        public virtual SerializableHttpRequestMessage CallbackHttpRequest { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsChannelArgs"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public EventsChannelArgs(SerializableHttpRequestMessage request)
        {
            this.CallbackHttpRequest = request;
        }
    }

    /// <summary>
    /// The event channel context
    /// </summary>
    internal class EventsChannelContext
    {
        /// <summary>
        /// Gets the events entity.
        /// </summary>
        /// <value>The events entity.</value>
        public EventsEntity EventsEntity { get; }

        /// <summary>
        /// Gets the logging context.
        /// </summary>
        /// <value>The logging context.</value>
        public LoggingContext LoggingContext { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventsChannelContext"/> class.
        /// </summary>
        /// <param name="eventsEntity">The events entity.</param>
        /// <param name="loggingContext">The logging context.</param>
        public EventsChannelContext(EventsEntity eventsEntity, LoggingContext loggingContext = null)
        {
            this.EventsEntity = eventsEntity;
            this.LoggingContext = loggingContext;
        }
    }
}
