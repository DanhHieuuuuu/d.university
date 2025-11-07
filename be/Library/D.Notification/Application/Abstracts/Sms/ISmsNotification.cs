using D.Notification.Dtos;

namespace D.Notification.Application.Abstracts.Sms
{
    public interface ISmsNotification
    {
        Task SendSmsAsync(NotificationMessage message);
    }
}
