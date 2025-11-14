using D.Notification.Domain.Common;
using D.Notification.Domain.Entities;
using D.Notification.Dtos;

namespace D.Notification.ApplicationService.Abstracts
{
    public interface INotificationService
    {
        Task SendAsync(NotificationMessage message);
        Task<NotiNotificationDetail?> GetByIdAsync(int notiId);
        Task<IEnumerable<NotiNotificationDetail>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notiId);
        Task MarkAllAsReadAsync(int userId);
    }
}
