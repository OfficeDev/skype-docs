using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.SfB.PlatformService.SDK.Common;
using System;
using System.Threading.Tasks;

namespace Microsoft.SfB.PlatformService.SDK.Samples.ApplicationCore
{
    /// <summary>
    /// Definition of interface IStorage
    /// </summary>
    public interface IStorage
    {
        /// <summary>
        /// Save job metadata
        /// </summary>
        /// <param name="jobMetadata"></param>
        /// <returns></returns>
        Task<bool> SaveJobMetadata(JobMetadata jobMetadata);
    }

    /// <summary>
    /// The azure storage class:
    /// Interact with azure stoage table to save job metadata
    /// Interact with azure service bus to save call back message
    /// </summary>
    public class AzureStorage : IStorage
    {
        #region private variables
        private readonly CloudQueueClient m_queueClient;
        private readonly CloudTableClient m_tableClient;
        #endregion

        private AzureStorage(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            m_queueClient = storageAccount.CreateCloudQueueClient();
            m_tableClient = storageAccount.CreateCloudTableClient();
        }

        /// <summary>
        /// Connect azure storage
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static AzureStorage Connect(string connectionString)
        {
            return new AzureStorage(connectionString);
        }

        private CloudTable GetJobMetadataTable()
        {
            return GetTable(Constants.JOB_METADATA_TABLE_NAME);
        }

        private CloudTable GetTable(string tableName)
        {
            var table = m_tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }

        #region interface implementation
        /// <summary>
        /// Saves or merges a JobMetadata object. If the object doesn't already exist in table, it will be added. If
        /// it already exists, it will be updated with the fields provided in the input object; any null field on input
        /// object will be ignored.
        /// </summary>
        /// <param name="jobMetadata"></param>
        /// <returns></returns>
        public Task<bool> SaveJobMetadata(JobMetadata jobMetadata)
        {
            return GetJobMetadataTable().ExecuteAsync(TableOperation.InsertOrMerge(jobMetadata))
                                        .ContinueWith(t => t.Result.HttpStatusCode == 200);
        }
        #endregion
    }

    /// <summary>
    /// Define the CallbackQueueMessageReceivedEventArgs class
    /// </summary>
    public class CallbackQueueMessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// The call back message
        /// </summary>
        public string Message { get; set; }
    }
}