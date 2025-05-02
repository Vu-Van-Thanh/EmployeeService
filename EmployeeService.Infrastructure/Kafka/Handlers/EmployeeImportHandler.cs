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
            var filepath = message.Filter.FilePath;
            var fileName = message.Filter.FileName;
            var sever = message.Filter.Sever;
            var fileUrl = $"{sever}/{filepath}".Replace("\\", "/");
            byte[] fileBytes;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(fileUrl);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"❌ Failed to download file from: {fileUrl}");
                    return;
                }

                fileBytes = await response.Content.ReadAsByteArrayAsync();
            }

            var formFile = _fileService.CreateFormFile(fileBytes, fileName);

            EmployeeImportDTO result = await _employeeService.ImportProfileFromExcelAsync(formFile);
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
