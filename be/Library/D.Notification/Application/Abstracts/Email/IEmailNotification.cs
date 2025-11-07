using D.Notification.Dtos;

namespace D.Notification.Application.Abstracts.Email
{
    public interface IEmailNotification
    {
        Task SendEmailAsync(NotificationMessage message);
    }
}
