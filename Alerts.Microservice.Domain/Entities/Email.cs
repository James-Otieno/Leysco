using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alerts.Microservice.Domain.Entities
{
    public class Email
    {
        public Guid Id { get; set; }
        public string EmailTo { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate { get; set; }

        public Email(string emailTo,string message,string status,string subject)
        {
            Id = Guid.NewGuid();
            EmailTo = emailTo;
            Message = message;
            Status = status;
            Subject = subject;
            SendDate= DateTime.Now;
           
        }
        public static Email AddNewEmail(string emailTo, string message, string status, string subject)
        {
            return new Email( emailTo,  message,  status,  subject);
        }
    }
}
