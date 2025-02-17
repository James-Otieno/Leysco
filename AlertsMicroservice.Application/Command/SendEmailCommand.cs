using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertsMicroservice.Application.Dto;
using MediatR;

namespace AlertsMicroservice.Application.Command
{
    public class SendEmailCommand:IRequest<bool>
    {

        public EmailDto email { get; set; }
    }
}
