using D.Notification.Application.Abstracts.Push;
using D.Notification.Dtos;

namespace D.Notification.ApplicationService.Implements.Push
{
    public class FcmPushNotification : IPushNotification
    {
        public Task SendPushAsync(NotificationMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
