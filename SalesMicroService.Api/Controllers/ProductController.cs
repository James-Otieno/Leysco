using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Application.Queries;

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

        [HttpPost("search-products")]
        public async Task<IActionResult> SearchProducts([FromBody] ProductSearchDto searchCriteria)
        {
            var query = new SearchProductsQuery(searchCriteria);
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPost("search-orders")]
        public async Task<IActionResult> SearchOrders([FromBody] OrderSearchDto searchCriteria)
        {
            var query = new SearchOrdersQuery(searchCriteria);
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost("search-customers")]
        public async Task<IActionResult> SearchCustomers([FromBody] CustomerSearchDto searchCriteria)
        {
            var query = new SearchCustomersQuery(searchCriteria);
            var customers = await _mediator.Send(query);
            return Ok(customers);
        }







    }
}
