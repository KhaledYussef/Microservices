using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MessageBus
{
    public class AzureMessageBus : IMessageBus
    {
        private const string connectionString = "";
        public async Task Publish<T>(T message, string queue_topic)
        {
            try
            {


                //var client = new ServiceBusAdministrationClient(connectionString);
                //if (!await client.QueueExistsAsync(queueName))
                //{
                //    await client.CreateQueueAsync(queueName);
                //}


                await using var client = new ServiceBusClient(connectionString);

                ServiceBusSender sender = client.CreateSender(queue_topic);
                var jsonMessage = JsonSerializer.Serialize(message);
                var serviceBusMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
                {
                    CorrelationId = Guid.NewGuid().ToString(),
                };

                await sender.SendMessageAsync(serviceBusMessage);
                await client.DisposeAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Subscribe<T>(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}
