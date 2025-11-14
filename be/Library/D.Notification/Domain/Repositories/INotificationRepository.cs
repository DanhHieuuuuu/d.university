using D.Notification.Domain.Common;
using D.Notification.Domain.Entities;

namespace D.Notification.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task AddAsync(NotiNotificationDetail entity);
        Task<NotiNotificationDetail?> GetByIdAsync(int notificationId);
        Task<IEnumerable<NotiNotificationDetail>> GetUserNotificationsAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task MarkAllAsReadAsync(int userId);
        Task SaveChangesAsync();
    }
}
