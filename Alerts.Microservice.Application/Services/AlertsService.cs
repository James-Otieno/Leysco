using Alerts.Microservice.Application.Commands;
using Alerts.Microservice.Application.Settings;
using Alerts.Microservice.Domain.Entities;
using Alerts.Microservice.Domain.Repositories;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MailKit.Net.Smtp;

namespace Alerts.Microservice.Application.Services
{
    public class AlertsService : IAlertsService
    {
        private readonly IAlertsRepository _alertsRepository;
        private readonly MailSettings _mailSettings;
        public AlertsService(IAlertsRepository alertsRepository,
            MailSettings mailSettings)
        {
            _alertsRepository = alertsRepository ??
                throw new ArgumentNullException(nameof(alertsRepository));
            _mailSettings = mailSettings;
        }

        public async Task<bool> SaveEmailAsync(SendEmailCommand emailCommand)
        {
            try
            {
                var sendSucessful = await SendEmailAsync(emailCommand);

                var newAlert = Email
                    .AddNewEmail(emailCommand.email.EmailTo,
                    emailCommand.email.Message,
                    sendSucessful?"SENT":"FAILED",
                    emailCommand.email.Subject
                    );
                await _alertsRepository.SaveAlertAsync(newAlert);
                return sendSucessful;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SendEmailAsync(SendEmailCommand emailCommand)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(emailCommand.email.EmailTo));
                email.Subject = emailCommand.email.Subject;
                var builder = new BodyBuilder();
               
                builder.HtmlBody = emailCommand.email.Message;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
