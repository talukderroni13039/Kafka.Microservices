using Confluent.Kafka;
using KafkaConsumerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace KafkaConsumerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderConsumerController : ControllerBase
    {
        private readonly IConsumer<string, string> _consumer;

        public OrderConsumerController(IConsumer<string, string> consumer)
        {
            _consumer = consumer;
        }
        [HttpPost]
        public IActionResult GetOrder()
        {
            try
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(10));
                if (consumeResult == null)
                    return NotFound("No orders found in Kafka.");
                // Convert the ConsumeResult to JSON
                var jsonResult = consumeResult.Message.Value;

                Console.WriteLine(jsonResult);  

                return Ok(jsonResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to place the order: {ex.Message}");
            }
        }
    }
}

