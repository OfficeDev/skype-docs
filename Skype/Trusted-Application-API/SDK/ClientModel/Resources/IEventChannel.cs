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
        public virtual SerializableHttpRequestMessage CallbackHttpRequest { get; }

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
        public EventsEntity EventsEntity { get; }

        public LoggingContext LoggingContext { get; }

        public EventsChannelContext(EventsEntity eventsEntity, LoggingContext loggingContext = null)
        {
            this.EventsEntity = eventsEntity;
            this.LoggingContext = loggingContext;
        }
    }
}
