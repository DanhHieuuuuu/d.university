using D.Notification.Dtos;

namespace D.Notification.Application.Abstracts.Push
{
    public interface IPushNotification
    {
        Task SendPushAsync(NotificationMessage message);
    }
}
