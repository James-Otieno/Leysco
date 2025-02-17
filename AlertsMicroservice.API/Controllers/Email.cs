using AlertsMicroservice.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlertsMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Email : ControllerBase
    {
        private readonly IMediator _mediator;

        public Email(IMediator mediator)
        {
            _mediator = mediator;

        }

        // POST api/<Email>
        [HttpPost]
        public async Task<bool> SendEmail([FromBody] SendEmailCommand command)
        {
            try
            {
                return await _mediator.Send(command);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
