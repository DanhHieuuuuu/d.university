using D.DomainBase.Dto;
using System.Net;
using System.Net.Mail;

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
