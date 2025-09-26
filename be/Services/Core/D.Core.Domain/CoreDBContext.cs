using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Domain.Shared.SeedData;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.SeedDataHrm();

            base.OnModelCreating(modelBuilder);
        }
    }
}
