using D.Core.Domain.Entities.DaoTao;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.File;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Domain.Shared.SeedData;
using D.DomainBase.Entity;
using D.InfrastructureBase.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace D.Core.Domain
{
    public class CoreDBContext : DbContext, IDbContext
    {
        public CoreDBContext(DbContextOptions<CoreDBContext> options)
            : base(options) { }

        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : class => base.Set<TEntity>();

        public new EntityEntry Entry(object entity) => base.Entry(entity);

        public new ChangeTracker ChangeTracker => base.ChangeTracker;

        public new int SaveChanges() => base.SaveChanges();

        public new Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public new DatabaseFacade Database => base.Database;

        // DbSet

        #region hrm

        // Danh mục
        DbSet<DmChucVu> DmChucVus { get; set; }
        DbSet<DmDanToc> DmDanTocs { get; set; }
        DbSet<DmGioiTinh> DmGioiTinhs { get; set; }
        DbSet<DmLoaiHopDong> DmLoaiHopDongs { get; set; }
        DbSet<DmLoaiPhongBan> DmLoaiPhongBans { get; set; }
        DbSet<DmQuanHeGiaDinh> DmQuanHeGiaDinhs { get; set; }
        DbSet<DmPhongBan> DmPhongBans { get; set; }
        DbSet<DmQuocTich> DmQuocTiches { get; set; }
        DbSet<DmToBoMon> DmToBoMon { get; set; }
        DbSet<DmTonGiao> DmTonGiaos { get; set; }
        DbSet<DmKhoaHoc> DmKhoaHocs { get; set; }
        DbSet<DtMonHoc> DtMonHocs { get; set; }
        DbSet<DtChuongTrinhKhung> DtChuongTrinhKhungs { get; set; }
        DbSet<DtMonTienQuyet> DtMonTienQuyets { get; set; }
        DbSet<DtChuongTrinhKhungMon> DtChuongTrinhKhungMons { get; set; }

        // Nhân sự
        DbSet<NsNhanSu> NsNhanSus { get; set; }
        DbSet<NsHopDong> NsHopDongs { get; set; }
        DbSet<NsHopDongChiTiet> NsHopDongChiTiets { get; set; }
        DbSet<NsToBoMon> NsToBoMons { get; set; }
        DbSet<NsQuanHeGiaDinh> NsQuanHeGiaDinhs { get; set; }

        #endregion

        #region sv

        DbSet<SvSinhVien> SvSinhViens { get; set; }

        #endregion

        #region file
        DbSet<FileManagement> FileManagements { get; set; }
        #endregion

        #region Delegation
        public DbSet<DelegationIncoming> DelegationIncomings { get; set; }
        public DbSet<DepartmentSupport> DepartmentSupports { get; set; }
        public DbSet<DetailDelegationIncoming> DetailDelegationIncomings { get; set; }
        public DbSet<LogReceptionTime> LogReceptionTimes { get; set; }
        public DbSet<LogStatus> LogStatuses { get; set; }
        public DbSet<Prepare> Prepares { get; set; }
        public DbSet<ReceptionTime> ReceptionTimes { get; set; }
        public DbSet<Supporter> Supporters { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType)
                                .Property<DateTime>(nameof(EntityBase.CreatedDate))
                                .HasDefaultValueSql("GETDATE()");
                }
            }

            modelBuilder.Entity<DtChuongTrinhKhungMon>(entity =>
            {
                entity.HasKey(e => new { e.ChuongTrinhKhungId, e.MonHocId });

                entity.HasOne(e => e.ChuongTrinhKhung)
                      .WithMany(khung => khung.ChuongTrinhKhungMons)
                      .HasForeignKey(e => e.ChuongTrinhKhungId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(e => e.MonHoc)
                      .WithMany(monHoc => monHoc.ChuongTrinhKhungMons)
                      .HasForeignKey(e => e.MonHocId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DtMonTienQuyet>(entity =>
            {
                entity.HasKey(e => new { e.MonHocId, e.MonTienQuyetId });
            });

            modelBuilder.Entity<DtMonHoc>(entity =>
            {
                entity.HasMany(monHoc => monHoc.MonTienQuyet)
                      .WithOne(tienQuyet => tienQuyet.MonHoc)
                      .HasForeignKey(tienQuyet => tienQuyet.MonHocId)
                      .OnDelete(DeleteBehavior.Restrict);
                
                entity.HasMany(monHoc => monHoc.LaTienQuyetMon)
                      .WithOne(tienQuyet => tienQuyet.MonTienQuyet)
                      .HasForeignKey(tienQuyet => tienQuyet.MonTienQuyetId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.SeedDataHrm();
            base.OnModelCreating(modelBuilder);
        }
    }
}
