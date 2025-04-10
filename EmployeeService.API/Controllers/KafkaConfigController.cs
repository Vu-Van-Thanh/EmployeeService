using EmployeeService.Infrastructure.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmployeeService.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class KafkaConfigController : ControllerBase
    {
        private readonly KafkaSettings _kafkaSettings;
        public KafkaConfigController(IOptions<KafkaSettings> kafkaOptions)
        {
            _kafkaSettings = kafkaOptions.Value;
        }
        [HttpGet("check")]
        public IActionResult CheckKafkaConfig()
        {
            var response = new
            {
                BootstrapServers = _kafkaSettings.BootstrapServers,
                GroupId = _kafkaSettings.GroupId,
                ConsumeTopics = _kafkaSettings.ConsumeTopicNames,
                ProducerTopic = _kafkaSettings.ProducerTopicNames
            };

            return Ok(response);
        }
    }

}
