using Confluent.Kafka;
using KafkaProducerApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace KafkaProducerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IProducer<string, string> _producer;

        public OrderController(IProducer<string, string> producer)
        {
            _producer = producer;
        }
     
        [HttpPost]
        public IActionResult PlaceOrder([FromBody] Order order)
        {
            try
            {
                var message = new Message<string, string>
                {
                    Key = Guid.NewGuid().ToString(),
                    Value =JsonConvert.SerializeObject(order)
                };
                _producer.Produce("order-topic", message);
                _producer.Flush(TimeSpan.FromSeconds(10));

                return Ok("Order placed successfully and sent to Kafka.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to place the order: {ex.Message}");
            }
        }
    }
}

