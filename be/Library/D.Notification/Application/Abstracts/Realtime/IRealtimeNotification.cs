using D.Notification.Dtos;

namespace D.Notification.Application.Abstracts.Realtime
{
    public interface IRealtimeNotification
    {
        Task SendRealtimeAsync(NotificationMessage message);
    }
}
