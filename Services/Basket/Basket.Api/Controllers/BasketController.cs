using Basket.Application.Commands;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("[action]")]

        public async Task<IActionResult> GetBasket(string userName)  
        {
            var query = new GetBasketByUserNameQuery(userName);
            var result = await _mediator.Send(query); 

            if (result == null)
                return NotFound();

            return Ok(result);  
        }
        [HttpPut("[action]")]
        
        public async Task<IActionResult> UpdateBasketByUserName([FromBody] UpdateBasketCommand command) 
        {
            
            var res = await _mediator.Send(command);
            return Ok(res);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteBasketByUserName([FromQuery] string name)
        {
            var query = new DeletebasketCommand(name);
            var res = await _mediator.Send(query); 
            return Ok(res);
        }


    }
}
