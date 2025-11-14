using System.Text.Json;
using D.Notification.Application.Abstracts.Email;
using D.Notification.Application.Abstracts.Push;
using D.Notification.Application.Abstracts.Realtime;
using D.Notification.Application.Abstracts.Sms;
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Common;
using D.Notification.Domain.Entities;
using D.Notification.Domain.Enums;
using D.Notification.Domain.Repositories;
using D.Notification.Dtos;
using Microsoft.Extensions.Logging;

namespace D.Notification.ApplicationService.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailNotification _email;
        private readonly ISmsNotification _sms;
        private readonly IPushNotification _push;
        private readonly IRealtimeNotification _realtime;
        private readonly INotificationRepository _repository;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(
            IEmailNotification email,
            ISmsNotification sms,
            IPushNotification push,
            IRealtimeNotification realtime,
            INotificationRepository repository,
            ILogger<NotificationService> logger
        )
        {
            _email = email;
            _sms = sms;
            _push = push;
            _realtime = realtime;
            _repository = repository;
            _logger = logger;
        }

        public async Task<NotiNotificationDetail?> GetByIdAsync(int notiId)
        {
            return await _repository.GetByIdAsync(notiId);
        }

        public async Task<IEnumerable<NotiNotificationDetail>> GetUserNotificationsAsync(int userId)
        {
            _logger.LogInformation($"{nameof(GetUserNotificationsAsync)} method called.");
            return await _repository.GetUserNotificationsAsync(userId);
        }

        public async Task MarkAllAsReadAsync(int userId)
        {
            await _repository.MarkAllAsReadAsync(userId);
        }

        public async Task MarkAsReadAsync(int id)
        {
            await _repository.MarkAsReadAsync(id);
        }

        public async Task SendAsync(NotificationMessage message)
        {
            switch (message.Channel)
            {
                case NotificationChannel.Email:
                    await _email.SendEmailAsync(message);
                    break;
                case NotificationChannel.Sms:
                    await _sms.SendSmsAsync(message);
                    break;
                case NotificationChannel.Push:
                    await _push.SendPushAsync(message);
                    break;
                case NotificationChannel.Realtime:
                    await _realtime.SendRealtimeAsync(message);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
