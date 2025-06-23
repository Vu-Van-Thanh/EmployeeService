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
        }


        // Phương thức để tạo consumer với CorrelationId làm GroupId
        private IConsumer<string, string> CreateConsumer(string correlationId)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _kafkaSettings?.BootstrapServers,
                GroupId = $"employee-consumer-{correlationId}",  
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            return new ConsumerBuilder<string, string>(consumerConfig).Build();
        }
        /*protected override async Task ExecuteAsync(CancellationToken stoppingToken)
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
                        Console.WriteLine("Receive : {0}", message);
                        var importHandler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<KafkaRequest<StartImportEmployee>>>();
                        var importData = JsonSerializer.Deserialize<KafkaRequest<StartImportEmployee>>(message);
                        await importHandler.HandleAsync(importData);
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
        }*/
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);
                        var topic = result.Topic;
                        var message = result.Message.Value;

                        using var scope = _serviceProvider.CreateScope();

                        switch (topic)
                        {
                            case "employee-import":
                                Console.WriteLine("Receive : {0}", message);
                                var importHandler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<KafkaRequest<StartImportEmployee>>>();
                                var importData = JsonSerializer.Deserialize<KafkaRequest<StartImportEmployee>>(message);
                                await importHandler.HandleAsync(importData);
                                break;

                            case "get-all-employee":
                                Console.WriteLine("Receive : {0}", message);
                                var filterHandler = scope.ServiceProvider.GetRequiredService<IKafkaHandler<KafkaRequest<EmployeeFilterDTO>>>();
                                var filterData = JsonSerializer.Deserialize<KafkaRequest<EmployeeFilterDTO>>(message);
                                await filterHandler.HandleAsync(filterData);
                                break;

                                // Thêm các topic khác nếu cần
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        Console.WriteLine($"Kafka consume error: {ex.Error.Reason}");
                    }
                    catch (OperationCanceledException)
                    {
                        // ignore khi stopping
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unhandled error in EmployeeConsumer: {ex.Message}");
                    }
                }
            }, stoppingToken);

            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();    // Dừng Kafka consumer một cách "gracefully"
            _consumer.Dispose();  // Giải phóng tài nguyên
            return base.StopAsync(cancellationToken); // Gọi base nếu có thêm xử lý mặc định
        }

    }
}
