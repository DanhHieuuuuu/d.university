using D.Auth.Domain.Entities;
using D.InfrastructureBase.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain
{
    public class AuthDBContext : DbContext, IDbContext
    {
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
        {
        }
        public new DbSet<TEntity> Set<TEntity>()
                    where TEntity : class => base.Set<TEntity>();

        public new EntityEntry  Entry(object entity) => base.Entry(entity);

        public new ChangeTracker ChangeTracker => base.ChangeTracker;

        public new int SaveChanges() => base.SaveChanges();

        public new Task<int> SaveChangesAsync() => base.SaveChangesAsync();
        public new DatabaseFacade Database => base.Database;

        public DbSet<NsNhanSu> NsNhanSus { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<NsNhanSu>(entity =>
            {
                entity.ToTable("NsNhanSu", "hrm");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);
                entity.Property(e => e.DeletedBy).HasMaxLength(255);
                entity.Property(e => e.ModifiedBy).HasMaxLength(255);
                entity.Property(e => e.NoiOhienTai).HasColumnName("NoiOHienTai");
            });
        }
    }
}
