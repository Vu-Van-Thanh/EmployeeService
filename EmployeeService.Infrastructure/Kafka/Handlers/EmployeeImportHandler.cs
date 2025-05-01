using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.DTO;
using EmployeeService.Core.Services;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class EmployeeImportHandler : IKafkaHandler<KafkaRequest<StartImportEmployee>>
    {
        
        private readonly IFileService _fileService;
        private readonly IEmployeeService _employeeService;
        public EmployeeImportHandler(IFileService fileService, IEmployeeService employeeService)
        {
            _fileService = fileService;
            _employeeService = employeeService;
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
            await _kafkaProducer.ProduceAsync("ImportedEmployee",null,"ImportedEmployee",kafkaResponse);

        }


        
    }
}
