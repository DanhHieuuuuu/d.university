using D.Core.Domain.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Notification.Domain.Entities
{
    [Table(nameof(NotiNotificationLog), Schema = DbSchema.Notification)]
    public class NotiNotificationLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public string? Content { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
