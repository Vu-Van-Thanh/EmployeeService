using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EmployeeService.Infrastructure.MessageBroker
{
    public class KafkaProducerService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;
        public KafkaProducerService(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
        }


        public async Task SendMessageAsync<T>(T message)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var messageJson = JsonSerializer.Serialize(message);

            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = messageJson });

            Console.WriteLine($"Sended message: {messageJson}");
        }
    }
}
