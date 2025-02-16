using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesMicroservice.Application.Commands;
using SalesMicroservice.Application.Dtos;
using SalesMicroservice.Domain.Entities;

namespace SalesMicroService.Api.Controllers
{

    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        }
        //  Create Order
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand createOrderCommand)
        {
            try
            {
                var orderId = await _mediator.Send(createOrderCommand);
                return Ok(orderId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating order: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        // Update Order
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateOrder([FromBody] UpdateOrderCommand updateProductCommand)
        {
            try
            {
                var result = await _mediator.Send(updateProductCommand);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating order: {ex.Message}");
            }
        }

        //  Cancel Order
        [HttpDelete]
        public async Task<ActionResult<bool>> CancelOrder([FromBody] CancelOrderCommand cancelOrderCommand)
        {
            try
            {
                var result = await _mediator.Send(cancelOrderCommand);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error canceling order: {ex.Message}");
            }
        }


        //  Update Order Status

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] OrderStatus newStatus)
        {
            var command = new UpdateOrderStatusCommand(orderId, newStatus);
            var result = await _mediator.Send(command);

            if (!result)
            {
                return NotFound("Order not found");
            }

            return Ok("Order status updated successfully");
        }

    }
}
