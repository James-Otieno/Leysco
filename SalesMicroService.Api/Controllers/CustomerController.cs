using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesMicroservice.Application.Commands;

namespace SalesMicroService.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // Create Customer
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            try
            {
                var customerId = await _mediator.Send(command);
                return Ok(customerId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating customer: {ex.InnerException?.Message ?? ex.Message}");
            }
        }

        // Update Customer
        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateCustomer([FromBody] UpdateCustomerCommand command)
        {
            try
            {
                var updatedCustomerId = await _mediator.Send(command);
                return Ok(updatedCustomerId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating customer: {ex.Message}");
            }
        }

        // ✅ Delete Customer
        [HttpDelete]
        public async Task<ActionResult<Guid>> DeleteCustomer([FromBody] DeleteCustomerCommand command)
        {
            try
            {
                var deletedCustomerId = await _mediator.Send(command);
                return Ok(deletedCustomerId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting customer: {ex.Message}");
            }
        }








    }




 }

