using Inventory.Application.Features.Commands;
using Inventory.Application.Features.Queries;
using Inventory.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var product = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery ( id );
            var product = await _mediator.Send(query);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updatedProduct = await _mediator.Send(command);
            if (updatedProduct == null) return NotFound();
            return Ok(updatedProduct);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var success = await _mediator.Send(new DeleteProductCommand { Id = id });
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
