using D.ControllerBase.Exceptions;

namespace D.Notification.Domain.Exceptions
{
    public class NotificationException : BaseException
    {
        public NotificationException(int errorCode)
            : base(errorCode) { }

        public NotificationException(int errorCode, string? message)
            : base(errorCode, message) { }
    }
}
