namespace Catalog.Api.Controllers
{
    using Catalog.Application.Commands;
    using Catalog.Application.Queries;
    using Catalog.Core.Specs;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class CatalogController : BaseApiController
    {
        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet()]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var query = new GetProuductByIdQuery(id);


            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return Ok(result);
        }

        [HttpGet("[action]/{Name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var query = new GetProductsByNameQuery(name);


            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound($"Product with ID {name} not found.");
            }

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProduct([FromQuery] CatalogSpecParams prams)
        {
            var query = new GetAllProductsQuery(prams);


            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound($"Products not found.");
            }

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();


            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound($"Products not found.");
            }

            return Ok(result);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTypes()
        {
            var query = new GetAllTypesQuery();


            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound($"Products not found.");
            }

            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var result = await _mediator.Send(request);


            if (result == null)
            {
                return NotFound($"Products not found.");
            }

            return Ok(result);
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var query = new DeleteProductCommand(id);
            var result = await _mediator.Send(query);


            if (result == null)
            {
                return NotFound($"Products not found.");
            }

            return Ok(result);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand request)
        {
            ;
            var result = await _mediator.Send(request);


            if (result == null)
            {
                return NotFound($"Products not found.");
            }

            return Ok(result);
        }
    }
}
