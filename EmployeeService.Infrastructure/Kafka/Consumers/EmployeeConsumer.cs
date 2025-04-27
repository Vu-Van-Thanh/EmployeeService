using System.Text.Json;
using Confluent.Kafka;
using EmployeeService.Core.DTO;
using EmployeeService.Infrastructure.Kafka.Handlers;
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
            /*ConsumerConfig consumerConfig = new ConsumerConfig
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
            _consumer.Subscribe(allTopics);*/
            try
            {
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

                // Khởi tạo Kafka consumer
                _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
                _consumer.Subscribe(allTopics);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu không thể khởi tạo hoặc subscribe Kafka consumer
                Console.WriteLine($"Lỗi khi khởi tạo Kafka consumer: {ex.Message}");
                throw; // Ném lại exception nếu không thể tiếp tục khởi tạo
            }

            _serviceProvider = serviceProvider;
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
                    case "get-all-employee":
                        Console.WriteLine("Receive : {0}", message);
                        var filterHandler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<KafkaRequest<EmployeeFilterDTO>>>();
                        var filterData = JsonSerializer.Deserialize<KafkaRequest<EmployeeFilterDTO>>(message);
                       

                        await filterHandler.HandleAsync(filterData);
                        break;
                        // thêm các topic khác nếu cần
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();    // Dừng Kafka consumer một cách "gracefully"
            _consumer.Dispose();  // Giải phóng tài nguyên
            return base.StopAsync(cancellationToken); // Gọi base nếu có thêm xử lý mặc định
        }

    }
}
