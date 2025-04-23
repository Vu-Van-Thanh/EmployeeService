using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.RepositoryContracts;
using OrchestratorService.API.Kafka.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class GetEmployeeHandler : IKafkaHandler<EmployeeFilterDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEventProducer _eventProducer;
        public GetEmployeeHandler(IEmployeeRepository employeeRepository, IEventProducer eventProducer)
        {
            _employeeRepository = employeeRepository;
            _eventProducer = eventProducer;
        }
        public async Task HandleAsync(EmployeeFilterDTO message)
        {
            List<Employee> result = await _employeeRepository.GetEmployeesByFilter(message.ToExpression());
            await _eventProducer.PublishAsync("EmployeeList", null, null, result);
            

        }
    }
}
