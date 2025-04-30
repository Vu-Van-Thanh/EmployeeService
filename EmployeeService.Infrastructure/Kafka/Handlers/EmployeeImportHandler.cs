using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.DTO;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class EmployeeImportHandler : IKafkaHandler<KafkaRequest<StartImportEmployee>>
    {
        
        public Task HandleAsync(KafkaRequest<StartImportEmployee> message)
        {
            return Task.CompletedTask;
        }
    }
}
