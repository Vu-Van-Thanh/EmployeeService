

using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EmployeeService.Infrastructure.Kafka.Consumers.Manual
{
    public class ManualEmployeeConsumer : IEventConsumer
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly KafkaSettings _kafkaSettings;

        public ManualEmployeeConsumer(IConfiguration config, IServiceProvider serviceProvider, IOptions<KafkaSettings> kafkaOptions)
        {
            _kafkaSettings = kafkaOptions.Value;
            ConsumerConfig consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings?.BootstrapServers,
                GroupId = _kafkaSettings?.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            List<string> allTopics = _kafkaSettings.ConsumeTopicNames
                            .SelectMany(entry => entry.Value)
                            .Distinct()
                            .ToList();
            _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            _consumer.Subscribe(allTopics);
            _serviceProvider = serviceProvider;
        }
        public Task<T> ConsumeAsync<T>(string topic, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
