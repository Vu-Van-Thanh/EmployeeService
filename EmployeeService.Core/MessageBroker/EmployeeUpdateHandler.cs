using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Core.DTO;

namespace EmployeeService.Core.MessageBroker
{
    public class EmployeeUpdateHandler : IKafkaHandler<EmployeeUpdateRequest>
    {
        public Task HandleAsync(EmployeeUpdateRequest message)
        {
            return Task.CompletedTask;
        }
    }
}
