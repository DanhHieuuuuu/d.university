using D.Notification.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace D.Notification.Infrastructures.Persistence
{
    public class NotificationDbContext : DbContext
    {
        public DbSet<NotiNotificationDetail> Notifications { get; set; }
        public DbSet<NotiNotificationLog> NotificationLogs { get; set; }

        public NotificationDbContext(DbContextOptions<NotificationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
