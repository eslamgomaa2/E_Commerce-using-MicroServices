using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;

namespace Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("Orders/{Username}")]
        public async Task<IActionResult> GetOrdersByUserName([FromRoute]string Username)
        {
           var orders= await _mediator.Send(new GetOrdersByUserName(Username));
            return Ok(orders);
        }

        [HttpPost("CreateOrders")]
        public async Task<IActionResult> CreateOrdersByUserName([FromBody]AddOrderCommand order)
        {
           var orders= await _mediator.Send(order);
            return Ok(orders);
        }

        [HttpPut("UpdateOrders")]
        public async Task<IActionResult> UpdateOrders( [FromBody]UpdateOrderCommand request)
        {
           var order= await _mediator.Send(request);
            return Ok(order);
        }

        [HttpDelete("DeleteOrders/{id}")]
        public async Task<IActionResult> DeleteOrders([FromRoute] int id)
        {
           var order= await _mediator.Send(new DeleteOrderCommand(id));
            return Ok(order);
        }
    }
}
