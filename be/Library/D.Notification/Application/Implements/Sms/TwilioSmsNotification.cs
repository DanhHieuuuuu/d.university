using D.Notification.Application.Abstracts.Sms;
using D.Notification.Dtos;

namespace D.Notification.ApplicationService.Implements.Sms
{
    public class TwilioSmsNotification : ISmsNotification
    {
        public Task SendSmsAsync(NotificationMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
