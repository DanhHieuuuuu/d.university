using D.Notification.Application.Abstracts.Logging;
using D.Notification.Domain.Entities;
using D.Notification.Domain.Exceptions;
using D.Notification.Domain.Repositories;

namespace D.Notification.ApplicationService.Implements.Logging
{
    public class NotificationLogService : INotificationLogService
    {
        private readonly INotificationLogRepository _repository;

        public NotificationLogService(INotificationLogRepository repository)
        {
            _repository = repository;
        }

        public async Task LogSuccessAsync(int notificationId, string receiver)
        {
            var content = $"Đã gửi đến {receiver}";

            var log = new NotiNotificationLog
            {
                NotificationId = notificationId,
                Success = true,
                Content = content,
                Timestamp = DateTime.Now,
            };

            await _repository.AddAsync(log);
            await _repository.SaveChangesAsync();
        }

        public async Task LogFailureAsync(int notificationId, string receiver, int errorCode)
        {
            var content = $"Chưa gửi được đến {receiver}";

            var log = new NotiNotificationLog
            {
                NotificationId = notificationId,
                Success = false,
                Error = NotificationErrorCode.Names[errorCode],
                Timestamp = DateTime.Now,
            };

            await _repository.AddAsync(log);
            await _repository.SaveChangesAsync();
        }
    }
}
