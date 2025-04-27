using EmployeeService.Core.Domain.Entities;
using EmployeeService.Core.DTO;
using EmployeeService.Core.RepositoryContracts;
using EmployeeService.API.Kafka.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Infrastructure.Kafka.KafkaEntity;

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    public class GetEmployeeHandler : IKafkaHandler<KafkaRequest<EmployeeFilterDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEventProducer _eventProducer;
        public GetEmployeeHandler(IEmployeeRepository employeeRepository, IEventProducer eventProducer)
        {
            _employeeRepository = employeeRepository;
            _eventProducer = eventProducer;
        }
        public async Task HandleAsync(KafkaRequest<EmployeeFilterDTO> message)
        {
            List<Employee> result = await _employeeRepository.GetEmployeesByFilter(message.Filter.ToExpression());
            KafkaResponse<List<Employee>> response = new KafkaResponse<List<Employee>>()
            {
                RequestType = message.RequestType,
                CorrelationId = message.CorrelationId,
                Timestamp = DateTime.UtcNow,
                Filter = result
            };
            Console.WriteLine($"GetEmployeeHandler: {result}");
            await _eventProducer.PublishAsync("EmployeeList", null, null, response);
            

        }
    }
}
