using D.Core.Domain.Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace D.Notification.Domain.Entities
{
    [Table(nameof(NotiNotificationDetail), Schema = DbSchema.Notification)]
    public class NotiNotificationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ReceiverId { get; set; }
        public string? Receiver { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
