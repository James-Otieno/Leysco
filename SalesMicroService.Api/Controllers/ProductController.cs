using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesMicroservice.Application.Commands;

namespace SalesMicroService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IMediator _mediator;


        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
                
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] CreateProductCommand createProductCommand)
        {
            try
            {
                var productId = await _mediator.Send(createProductCommand);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating product: {ex.Message}");
            }
        }



        // ✅ Update Product
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand createProductCommand)
        {
            try
            {
                var result = await _mediator.Send(createProductCommand);
                return result ? Ok("Product updated successfully") : BadRequest("Product update failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating product: {ex.Message}");
            }
        }


        // ✅ Delete Product
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct([FromBody] DeleteProductCommand createProductCommandcommand)
        {
            try
            {
                var result = await _mediator.Send(createProductCommandcommand);
                return result ? Ok("Product deleted successfully") : BadRequest("Product deletion failed");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting product: {ex.Message}");
            }
        }





    }
}
