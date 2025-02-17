using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertsMicroservice.Application.Services;
using MediatR;

namespace AlertsMicroservice.Application.Command
{
    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, bool>
    {
        private readonly IAlertsService _alertsService;

        public SendEmailCommandHandler(IAlertsService alertsService)
        {
            _alertsService = alertsService;
        }

        public async Task<bool> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            return await  _alertsService.SaveEmailAsync(request);
        }
    }
}
