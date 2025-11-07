using D.Notification.Domain.Common;
using D.Notification.Domain.Entities;
using D.Notification.Domain.Repositories;
using D.Notification.Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;

namespace D.Notification.Infrastructures.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationDbContext _dbContext;

        public NotificationRepository(NotificationDbContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(NotiNotificationDetail entity)
        {
            await _dbContext.Notifications.AddAsync(entity);
        }

        public async Task<NotiNotificationDetail?> GetByIdAsync(int notificationId)
        {
            return await _dbContext.Notifications.FirstOrDefaultAsync(x => x.Id == notificationId);
        }

        public async Task<IEnumerable<NotiNotificationDetail>> GetUserNotificationsAsync(
            int userId,
            PagingRequestDto dto
        )
        {
            var query = _dbContext
                .Notifications.Where(x => x.ReceiverId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            return await query.Skip(dto.SkipCount()).Take(dto.PageSize).ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var noti = await _dbContext.Notifications.FirstOrDefaultAsync(x =>
                x.Id == notificationId
            );

            if (noti != null)
            {
                noti.IsRead = true;
            }
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            await _dbContext
                .Notifications.Where(x => x.ReceiverId == userId && !x.IsRead)
                .ExecuteUpdateAsync(setters => setters.SetProperty(n => n.IsRead, true));
        }
    }
}
