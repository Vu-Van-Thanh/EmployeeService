using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.DTO;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class EmployeeImportHandler : IKafkaHandler<EmployeeImportDTO>
    {
        public Task HandleAsync(EmployeeImportDTO message)
        {
            return Task.CompletedTask;
        }
    }
}
