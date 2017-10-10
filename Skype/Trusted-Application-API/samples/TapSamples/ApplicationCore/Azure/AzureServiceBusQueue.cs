using Microsoft.SfB.PlatformService.SDK.Common;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// Definition of interface ICallbackMessageQueue
    /// </summary>
    public interface ICallbackMessageQueueManager
    {
        /// <summary>
        /// Save call back http message
        /// </summary>
        /// <param name="instanceId">the current process instance id</param>
        /// <param name="serialziedHttpMessage">searialized http message string</param>
        /// <returns></returns>
        Task SaveCallbackMessageAsync(string queueName, string serialziedHttpMessage);

        /// <summary>
        /// InitializeCallbackMessageQueue
        /// </summary>
        /// <param name="queueName"></param>
        void InitializeCallbackMessageQueue(string queueName);

        /// <summary>
        /// Event handler for processing callback message
        /// </summary>
        event EventHandler<CallbackQueueMessageReceivedEventArgs> OnCallbackMessageReceived;

        /// <summary>
        /// Delete callback message queue
        /// </summary>
        /// <param name="queueName"></param>
        void DeleteCallbackMessageQueue(string queueName);
    }

    public class AzureServiceBusCallbackMessageQueueManager : ICallbackMessageQueueManager
    {
        private readonly string m_serviceBusConnectionString;
        private QueueClient m_localServiceBusQueueClient;
        private ConcurrentDictionary<string, QueueClient> m_serviceBusQueueClients;

        /// <summary>
        /// The event handler for receiving call back message
        /// </summary>
        public event EventHandler<CallbackQueueMessageReceivedEventArgs> OnCallbackMessageReceived;

        public AzureServiceBusCallbackMessageQueueManager(string connectionString) {
            m_serviceBusConnectionString = connectionString;
            m_serviceBusQueueClients = new ConcurrentDictionary<string, QueueClient>();
        }

        private void OnServiceBusQueueMessage(BrokeredMessage message)
        {
            try
            {
                if (this.OnCallbackMessageReceived != null)
                {
                    //Log message received:
                    CallbackQueueMessageReceivedEventArgs args = new CallbackQueueMessageReceivedEventArgs
                    {
                        Message = message.GetBody<string>()
                    };
                    this.OnCallbackMessageReceived(this, args);
                }
            }
            catch (Exception ex)
            {
                //Log error here
                // Indicates a problem, unlock message in queue.
                Logger.Instance.Error(ex, "Error in processing incoming service bus queue message!");
                //message.Abandon();
            }
            finally
            {
                // Remove message from queue. Even if processing message fail, we do not want to block other messages
                try
                {
                    message.Complete();
                }
                catch (Exception ex)
                {
                    Logger.Instance.Warning(ex, "Error in finish processing incoming service bus queue message!");
                }
            }
        }

        #region interface implementation
        /// <summary>
        /// Create the service bus queue for call back messages
        /// </summary>
        /// <param name="queueName"></param>
        public void InitializeCallbackMessageQueue(string queueName)
        {
            QueueDescription qd = new QueueDescription(queueName);
            qd.MaxSizeInMegabytes = 5120;

            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(m_serviceBusConnectionString);

            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(qd);
            }

            m_localServiceBusQueueClient = QueueClient.CreateFromConnectionString(m_serviceBusConnectionString, queueName);
            m_localServiceBusQueueClient.OnMessage(OnServiceBusQueueMessage);
        }

        /// <summary>
        /// Save call back message
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="serializedHttpMessage"></param>
        /// <returns></returns>
        public async Task SaveCallbackMessageAsync(string instanceId, string serializedHttpMessage)
        {
            QueueClient client = m_serviceBusQueueClients.GetOrAdd(
                instanceId,
                (a) => QueueClient.CreateFromConnectionString(m_serviceBusConnectionString, instanceId)
            );

            if (client == null)
            {
                throw new PlatformServiceAzureStorageException("Service Bus Queue instance id " + instanceId + " for call back messages has not been initialized!");
            }
            BrokeredMessage message = new BrokeredMessage(serializedHttpMessage);
            try
            {
                await client.SendAsync(message).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Instance.Error(ex, "Error in saving message to service bus");
                throw new PlatformServiceAzureStorageException("Error in saving message to service bus", ex);
            }
        }

        /// <summary>
        /// Delete the service bus queue for callback message
        /// </summary>
        /// <param name="queueName"></param>
        public void DeleteCallbackMessageQueue(string queueName)
        {
            var namespaceManager =
            NamespaceManager.CreateFromConnectionString(m_serviceBusConnectionString);

            if (namespaceManager.QueueExists(queueName))
            {
                namespaceManager.DeleteQueue(queueName);
            }
        }

        #endregion

    }
}
