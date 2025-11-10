using D.Notification.Domain.Enums;

namespace D.Notification.Dtos
{
    public class NotificationMessage
    {
        /// <summary>
        /// Người nhận
        /// </summary>
        public Receiver Receiver { get; set; }

        /// <summary>
        /// Tiêu đề
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Nội dung thông báo
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Nội dung thông báo (lưu vào db)
        /// </summary>
        public string AltContent { get; set; }

        /// <summary>
        /// Kênh truyền tin
        /// </summary>
        public NotificationChannel Channel { get; set; }
    }

    public class Receiver
    {
        /// <summary>
        /// Send notification to user with id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Send Email to this mail
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Send SMS to this phone
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Send PushNotification to these FcmTokens via Firebase
        /// </summary>
        public List<string>? FcmTokens { get; set; }

        /// <summary>
        /// Send PushNotification to these APNs via APN
        /// </summary>
        public List<string>? APNs { get; set; }

        /// <summary>
        /// Send to these socketId
        /// </summary>
        public List<string>? WebSocketIds { get; set; }
    }
}
