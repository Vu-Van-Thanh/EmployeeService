using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace EmployeeService.Infrastructure.MessageBroker
{
    // Core/Application/Common/Interfaces/IMessageProducer.cs
    public interface IMessageProducer
    {
        Task SendMessageAsync<T>(T message);
    }

}
