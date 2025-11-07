using D.Notification.Application.Abstracts.Realtime;
using D.Notification.Dtos;

namespace D.Notification.ApplicationService.Implements.Realtime
{
    public class WebsocketNotification : IRealtimeNotification
    {
        public Task SendRealtimeAsync(NotificationMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
