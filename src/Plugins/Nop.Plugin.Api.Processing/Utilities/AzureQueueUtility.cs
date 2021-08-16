using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Azure;
using Azure.Storage.Queues.Models;

namespace Nop.Plugin.Api.Processing.Utilities
{
    public class AzureQueueUtility
    {
        public static async Task<Response> DeleteQueueMessageAsync(string queueName, QueueMessage queueMessage)
        {
            var client = GetQueueClient(queueName);

            return await client.DeleteMessageAsync(queueMessage.MessageId, queueMessage.PopReceipt);
        }

        public static async Task<QueueMessage> GetQueueMessageAsync(string queueName)
        {
            var client = GetQueueClient(queueName);
            return await client.ReceiveMessageAsync();
        }

        public static async Task QueueMessageAsync(string queueName, string message)
        {
           var client = GetQueueClient(queueName);
            await client.SendMessageAsync(message);
        }

        private static QueueClient GetQueueClient(string queueName)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=odtll4s;AccountKey=yEyFAHqdLa5MKqAkT2SJqS6FV6jzgxiniMyhXjci7WAejgr97mv61yZQG++P/8TBD9O035Ht2SEHwtqM+QtlXg==;EndpointSuffix=core.windows.net";
            QueueClient client = new QueueClient(connectionString, queueName);

            return client;
        }
    }
}
