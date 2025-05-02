using EmployeeService.API.Kafka.Producer;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;
using EmployeeService.Infrastructure.Kafka.KafkaEntity;
using Microsoft.AspNetCore.Http;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class EmployeeImportHandler : IKafkaHandler<KafkaRequest<StartImportEmployee>>
    {
        
        private readonly IFileService _fileService;
        private readonly IEmployeeService _employeeService;
        private readonly IEventProducer _kafkaProducer;
        public EmployeeImportHandler(IFileService fileService, IEmployeeService employeeService, IEventProducer eventProducer)
        {
            _fileService = fileService;
            _employeeService = employeeService;
            _kafkaProducer = eventProducer;
        }
        public async Task HandleAsync(KafkaRequest<StartImportEmployee> message)
        {
            Console.WriteLine($"✅ Start Import Employee from file: {message.Filter.FilePath}");
            var filepath = message.Filter.FilePath;
            var fileName = message.Filter.FileName;
            var sever = message.Filter.Sever;
            var fileUrl = $"{sever}/{filepath}".Replace("\\", "/");
            byte[] fileBytes;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(fileUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"❌ Failed to download file from: {fileUrl}");
                        return;
                    }

                    fileBytes = await response.Content.ReadAsByteArrayAsync();
                    Console.WriteLine($"✅ File downloaded successfully from: {fileUrl}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error while processing file: {ex.Message}");
                return;
            }
            
            Guid employeeId = Guid.NewGuid();

            EmployeeImportDTO result = await _employeeService.ImportProfileFromExcelAsync(fileBytes, employeeId.ToString());
            KafkaResponse<EmployeeImportDTO> kafkaResponse = new KafkaResponse<EmployeeImportDTO>
            {
                RequestType = "ImportedEmployee",
                CorrelationId = message.CorrelationId,
                Timestamp = DateTime.UtcNow,
                Filter = result
            };
            await _kafkaProducer.PublishAsync("ImportedEmployee",null,"ImportedEmployee",kafkaResponse);

        }


        
    }
}
