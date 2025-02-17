using Alerts.Microservice.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Application.Commands
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IAlertsService _alertsService;
        public SendEmailCommandHandler(IAlertsService alertsService)
        {
            _alertsService = alertsService ?? throw new ArgumentNullException(nameof(alertsService));
        }
        public Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return _alertsService.SaveEmailAsync(request);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

