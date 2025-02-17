using Alerts.Microservice.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Alerts.Microservice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Email : ControllerBase
    {
        private readonly IMediator _mediator;
        public Email(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
