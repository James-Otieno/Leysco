using Alerts.Microservice.Application.Commands;
using EasyNetQ.AutoSubscribe;
using MediatR;
using Paymaster.Contracts;

namespace Alerts.Microservice.Api.Consumer
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
                email=new Application.Dto.EmailDto
                {
                    EmailTo = message.Email,
                    Subject="Welcome To Equity Bank Plc",
                    Message=$"Dear {message.FullNames}, we are happy to welcome you to equity bank.Your AccountNumber is {message.AccountNumber}.You will receive your Internet Banking details via sms and also your Ussd Pin via sms.Thank you"
                }
            };
            await _mediator.Send(newAlert);
        }
    }
}
