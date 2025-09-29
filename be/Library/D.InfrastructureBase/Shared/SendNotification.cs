using D.DomainBase.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace D.InfrastructureBase.Shared
{
    public static class SendNotification
    {
        public static async Task SendEmail(SendEmailDto dto)
        {
            var smtpClient = new SmtpClient(dto.Host, dto.Post);

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(dto.EmailFrom, dto.Password);

            var message = new MailMessage(dto.EmailFrom, dto.EmailTo, dto.Title, dto.Body)
            {
                IsBodyHtml = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}
