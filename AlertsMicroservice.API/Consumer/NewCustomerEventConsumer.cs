using AlertsMicroservice.Application.Command;
using EasyNetQ.AutoSubscribe;
using MediatR;
using Sales.Contracts;

namespace AlertsMicroservice.API.Consumer
{
    public class NewCustomerEventConsumer : IConsumeAsync<NewCustomerEvent>
    {
        private readonly IMediator _mediator;

        public NewCustomerEventConsumer(IMediator mediator)
        {
            _mediator = mediator;

        }

        public async Task ConsumeAsync(NewCustomerEvent message, CancellationToken cancellationToken = default)
        {
            var newAlert = new SendEmailCommand
            {
                email = new Application.Dto.EmailDto
                {
                    EmailTo = message.Email,
                    Subject = "Order Confirmation",
                    Message = $"Dear {message.FullNames}, you've succesfully placed an order."
                }
            };
            await _mediator.Send(newAlert);
        }
    }
}
