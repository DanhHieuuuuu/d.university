using D.Notification.Domain.Entities;

namespace D.Notification.Domain.Repositories
{
    public interface INotificationLogRepository
    {
        Task AddAsync(NotiNotificationLog log);
        Task<IEnumerable<NotiNotificationLog>> GetByNotificationIdAsync(int notificationId);
        Task<IEnumerable<NotiNotificationLog>> GetListAsync();
        Task SaveChangesAsync();
    }
}
