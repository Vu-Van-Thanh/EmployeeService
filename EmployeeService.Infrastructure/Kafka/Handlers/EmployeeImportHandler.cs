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
            var fileContent = message.Filter.FileContent;
            var base64Content = Convert.FromBase64String(fileContent);
            var fileBytes = base64Content.ToArray();
            var fileName = $"EmployeeImport_{Guid.NewGuid()}.xlsx";
            IFormFile file = _fileService.CreateFormFile(fileBytes, fileName);
            EmployeeImportDTO result = await _employeeService.ImportProfileFromExcelAsync(file);
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
