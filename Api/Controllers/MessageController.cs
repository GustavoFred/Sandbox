using Infrastructure.RabbitMQ;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly RabbitMQProducer _rabbitmqProducer;
        private readonly RabbitMQConsumer _consumer;

        public MessageController(IMediator mediator, RabbitMQProducer rabbitmqProducer, RabbitMQConsumer consumer)
        {
            _mediator = mediator;
            _rabbitmqProducer = rabbitmqProducer;
            _consumer = consumer;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] string message)
        {
            await _rabbitmqProducer.Publish(message);
            return Ok("Mensagem enviada");
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            string[] messages = [];

            messages = _consumer.GetAllMessages().Result.ToArray();

            return Ok(messages);

        }
    }
}
