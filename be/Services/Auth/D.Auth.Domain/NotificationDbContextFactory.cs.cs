using D.Notification.Infrastructures.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain
{
    public class NotificationDbContextFactory
        : IDesignTimeDbContextFactory<NotificationDbContext>
    {
        public NotificationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<NotificationDbContext>();

            optionsBuilder.UseSqlServer(
                "Data Source=203.210.148.167,8092;Initial Catalog=DO_AN;User ID=sa;Password=labDev@123;Trust Server Certificate=True;MultipleActiveResultSets=True;",
                b =>
                {
                    b.MigrationsAssembly("D.Auth.Domain");
                    b.MigrationsHistoryTable("__EFMigrationsHistory", "noti");
                });

            return new NotificationDbContext(optionsBuilder.Options);
        }
    }
}
