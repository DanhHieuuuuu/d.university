namespace D.Notification.Application.Abstracts.Logging
{
    public interface INotificationLogService
    {
        Task LogSuccessAsync(int notificationId, string receiver);
        Task LogFailureAsync(int notificationId, string receiver, int errorCode);
    }
}
