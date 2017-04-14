using Microsoft.SfB.PlatformService.SDK.ClientModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SfB.PlatformService.SDK.Common;
using System.Web;

namespace QuickSamplesCommon
{
    /// <summary>
    /// The implementation of <see cref="IEventChannel"/> simple event channel which take http request from callbackcontroller and raise events
    /// This is a sample of EventChannel, and assume your deployment only have one instance so all call back events land on the only one instance
    /// 
    /// If you have multiple instances in your deployment, you need to implement callback event proxy/redirect strategy. for example, proxy the request to another instance, or 
    /// put requests in service bus queue with different queue name space and let each instance to pick up their own
    /// 
    /// application is free to implement event channel in other ways 
    /// 
    /// </summary>
    public class SimpleEventChannel : IEventChannel
    {
        #region private members
        /// <summary>
        /// track the state of event channel
        /// </summary>
        private int m_isStarted = 0;

        /// <summary>
        /// Event to handle incoming events
        /// </summary>
        public event EventHandler<EventsChannelArgs> HandleIncomingEvents;
        #endregion

        /// <summary>
        /// constructor
        /// </summary>
        public SimpleEventChannel()
        {
        }

        /// <summary>
        /// implement the interface TryStartAsync
        /// </summary>
        /// <returns></returns>
        public Task TryStartAsync()
        {
            int currentValue = Interlocked.Exchange(ref m_isStarted, 1);
           
            return TaskHelpers.CompletedTask;
        }

        /// <summary>
        /// implement the interface TryStopAsync
        /// </summary>
        /// <returns></returns>
        public Task TryStopAsync()
        {
             Interlocked.Exchange(ref m_isStarted, 0);
            return TaskHelpers.CompletedTask;
        }

        public void ProcessCallbackMessage(SerializableHttpRequestMessage message)
        {
            if (message != null)
            {
                this.HandleIncomingEvents?.Invoke(this, new EventsChannelArgs(message));
            }
        }


    }
}