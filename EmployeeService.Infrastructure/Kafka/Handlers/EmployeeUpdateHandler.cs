using EmployeeService.Core.DTO;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class EmployeeUpdateHandler : IKafkaHandler<EmployeeUpdateRequest>
    {
        public Task HandleAsync(EmployeeUpdateRequest message)
        {
            return Task.CompletedTask;
        }
    }
}
