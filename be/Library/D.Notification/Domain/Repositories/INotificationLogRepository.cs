using D.Notification.Domain.Common;
using D.Notification.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Notification.Domain.Repositories
{
    public interface INotificationLogRepository
    {
        Task AddAsync(NotiNotificationLog log);
        Task<IEnumerable<NotiNotificationLog>> GetByNotificationIdAsync(int notificationId);
        Task<IEnumerable<NotiNotificationLog>> FindPaging(PagingRequestDto dto);
        Task SaveChangesAsync();
    }
}
