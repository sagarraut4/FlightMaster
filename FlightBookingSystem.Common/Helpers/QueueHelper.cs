using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingSystem.Common.Helpers
{
    public class QueueHelper : IQueueHelper
    {
        private QueueClient _queueClient;
        private readonly string _connectionString = Environment.GetEnvironmentVariable(Constant.CommonConstants.StorageConnectionStringName);

        public async Task Insert(string queueName, string message)
        {
            _queueClient = new QueueClient(_connectionString, queueName);
            await _queueClient.CreateIfNotExistsAsync();
            if (await _queueClient.ExistsAsync())
            {
                // Send a message to the queue
                await _queueClient.SendMessageAsync(message);
            }
        }

        public async Task Delete(string queueName, string messageId, string popReceipt)
        {
            _queueClient = new QueueClient(_connectionString, queueName);
            await _queueClient.DeleteMessageAsync(messageId, popReceipt);
        }
        
        public async Task<QueueMessage> Read(string queueName)
        {
            _queueClient = new QueueClient(_connectionString, queueName);
            var response = await _queueClient.ReceiveMessageAsync();
            return new QueueMessage { Message = response.Value.MessageText, MessageId = response.Value.MessageId, PopReceipt = response.Value.PopReceipt };
        }

        public async Task Update(string queueName, QueueMessage queueMessage)
        {
            _queueClient = new QueueClient(_connectionString, queueName);
            await _queueClient.UpdateMessageAsync(queueMessage.MessageId, queueMessage.PopReceipt, queueMessage.Message);
        }

        public async Task ClearQueue(string queueName)
        {
            _queueClient = new QueueClient(_connectionString, queueName);

            if (_queueClient.Exists())
            {
                QueueProperties properties = _queueClient.GetProperties();

                // Retrieve the cached approximate message count.
                int cachedMessagesCount = properties.ApproximateMessagesCount;

                // Receive and process 20 messages
                var receivedMessages = _queueClient.ReceiveMessagesAsync(cachedMessagesCount, TimeSpan.FromMinutes(5)).GetAwaiter().GetResult().Value;

                foreach (Azure.Storage.Queues.Models.QueueMessage message in receivedMessages)
                {
                    // Delete the message
                    await _queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
                }
            }
        }
    }
}
