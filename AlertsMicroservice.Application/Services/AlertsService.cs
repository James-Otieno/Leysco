using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlertsMicroservice.Application.Command;
using AlertsMicroservice.Application.Settings;
using AlertsMicroservice.Domain.Entities;
using AlertsMicroservice.Domain.Repositories;
using MailKit.Security;
using MimeKit;



namespace AlertsMicroservice.Application.Services
{
    public class AlertsService: IAlertsService
    {
        private readonly IAlertsRepository _alertsRepository;
        private readonly MailSettings _mailSettings;

        public AlertsService(IAlertsRepository alertsRepository , MailSettings mailSettings)
        {
            _alertsRepository = alertsRepository;
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
                    sendSucessful ? "SENT" : "FAILED",
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
