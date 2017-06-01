using Newtonsoft.Json;
using Microsoft.SfB.PlatformService.SDK.ClientModel;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// The implementation of <see cref="IEventChannel"/> channel based on azure service bus
    /// This is a sample of EventChannel, application is free to implement event channel in other ways
    /// </summary>
    public class QueueBasedEventChannel : IEventChannel
    {
        #region private members
        /// <summary>
        /// track the state of event channel
        /// </summary>
        private int m_isStarted = 0;

        /// <summary>
        /// the storage client
        /// </summary>
        private ICallbackMessageQueueManager m_callbackQueue;

        #endregion

        /// <summary>
        /// Event to handle incoming events
        /// </summary>
        public event EventHandler<EventsChannelArgs> HandleIncomingEvents;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="eventQueueId"></param>
        public QueueBasedEventChannel(ICallbackMessageQueueManager callbackQueue)
        {
            m_callbackQueue = callbackQueue;
        }

        /// <summary>
        /// implement the interface TryStartAsync
        /// </summary>
        /// <returns></returns>
        public Task TryStartAsync()
        {
            int currentValue = Interlocked.Exchange(ref m_isStarted, 1);
            if (currentValue == 0)
            {
                m_callbackQueue.OnCallbackMessageReceived += this.OnFetchCallbackMessage;
            }
            return TaskHelpers.CompletedTask;
        }

        /// <summary>
        /// implement the interface TryStopAsync
        /// </summary>
        /// <returns></returns>
        public Task TryStopAsync()
        {
            m_callbackQueue.OnCallbackMessageReceived -= this.OnFetchCallbackMessage;
            return TaskHelpers.CompletedTask;
        }

        private void OnFetchCallbackMessage(object sender, CallbackQueueMessageReceivedEventArgs args)
        {
            string nextMessage = args.Message;

            if (!string.IsNullOrEmpty(nextMessage))
            {
                ProcessCallbackMessage(nextMessage);
            }
        }

        /// <summary>
        /// ProcessCallbackMessage
        /// </summary>
        /// <param name="message"></param>
        private void ProcessCallbackMessage(string message)
        {
            Logger.Instance.Information("ProcessCallbackMessage start!");
            SerializableHttpRequestMessage httpMessage = null;
            try
            {
                httpMessage = JsonConvert.DeserializeObject<SerializableHttpRequestMessage>(message);

                /*
                //this is a hack for jsonConvert, seems JsonConvert cannot deserialize logging context correctly
                JObject jObject = JObject.Parse(message);
                if (jObject["LoggingContext"] != null)
                {
                    string loggingContextString = jObject["LoggingContext"].ToString();
                    LoggingContext currentLoggingContext = JsonConvert.DeserializeObject<LoggingContext>(loggingContextString);
                    httpMessage.LoggingContext = currentLoggingContext;
                }
                */
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "exception happened in deserialzie http message");
                return;
            }

            if (httpMessage != null)
            {
                this.HandleIncomingEvents?.Invoke(this, new EventsChannelArgs(httpMessage));
            }
        }
    }
}