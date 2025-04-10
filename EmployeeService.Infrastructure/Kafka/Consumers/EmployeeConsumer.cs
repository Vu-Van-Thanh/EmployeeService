using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using EmployeeService.Core.DTO;
using EmployeeService.Core.MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace EmployeeService.Infrastructure.Kafka.Consumers
{
    public class EmployeeConsumer : BackgroundService
    {
        private readonly IConsumer<string, string> _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly KafkaSettings _kafkaSettings;

        public EmployeeConsumer(IConfiguration config, IServiceProvider serviceProvider, IOptions<KafkaSettings> kafkaOptions)
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
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = _consumer.Consume(stoppingToken);
                var topic = result.Topic;
                var message = result.Message.Value;
                using var scope = _serviceProvider.CreateScope();

                switch (topic)
                {
                    case "employee-import":
                        var importHandler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<EmployeeImportDTO>>();
                        var importData = JsonSerializer.Deserialize<EmployeeImportDTO>(message);
                        await importHandler.HandleAsync(importData);
                        break;

                    case "employee-update":
                        var updateHandler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<EmployeeUpdateRequest>>();
                        var updateData = JsonSerializer.Deserialize<EmployeeUpdateRequest>(message);
                        await updateHandler.HandleAsync(updateData);
                        break;

                        // thêm các topic khác nếu cần
                }
            }
        }
    }
}
