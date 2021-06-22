using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public interface IQueueHelper
    {
        Task Insert(string queueName, string message);
        Task<QueueMessage> Read(string queueName);
        Task Delete(string queueName, string messageId, string popReceipt);
        Task Update(string queueName, QueueMessage queueMessage);
        Task ClearQueue(string queueName);
    }
}
