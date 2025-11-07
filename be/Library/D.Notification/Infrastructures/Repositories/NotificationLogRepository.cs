using D.Notification.Domain.Common;
using D.Notification.Domain.Entities;
using D.Notification.Domain.Repositories;
using D.Notification.Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace D.Notification.Infrastructures.Repositories
{
    public class NotificationLogRepository : INotificationLogRepository
    {
        private readonly NotificationDbContext _dbContext;

        public NotificationLogRepository(NotificationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(NotiNotificationLog log)
        {
            _dbContext.NotificationLogs.Add(log);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<NotiNotificationLog>> GetByNotificationIdAsync(
            int notificationId
        )
        {
            return await _dbContext
                .NotificationLogs.Where(x => x.NotificationId == notificationId)
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();
        }

        public async Task<IEnumerable<NotiNotificationLog>> FindPaging(PagingRequestDto dto)
        {
            return await _dbContext
                .NotificationLogs.OrderByDescending(x => x.Timestamp)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToListAsync();
        }
    }
}
