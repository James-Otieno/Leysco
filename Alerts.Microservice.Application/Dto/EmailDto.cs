using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Application.Dto
{
    public record EmailDto
    {
        public string EmailTo { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
