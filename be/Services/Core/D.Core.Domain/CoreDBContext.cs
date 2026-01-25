using D.Core.Domain.Entities.DaoTao;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Domain.Entities.File;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Domain.Entities.Survey;
using D.Core.Domain.Entities.Sysvar;
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

        // Nhân sự
        DbSet<NsNhanSu> NsNhanSus { get; set; }
        DbSet<NsHopDong> NsHopDongs { get; set; }
        DbSet<NsToBoMon> NsToBoMons { get; set; }
        DbSet<NsQuanHeGiaDinh> NsQuanHeGiaDinhs { get; set; }
        DbSet<NsQuyetDinh> NsQuyetDinhs { get; set; }
        DbSet<NsQuyetDinhLog> nsQuyetDinhLogs { get; set; }
        DbSet<NsQuaTrinhCongTac> NsQuaTrinhCongTacs { get; set; }

        #endregion

        #region DaoTao
        DbSet<DtKhoa> DtKhoas { get; set; }
        DbSet<DtNganh> DtNganhs { get; set; }
        DbSet<DtChuyenNganh> DtChuyenNganhs { get; set; }

        DbSet<DtMonHoc> DtMonHocs { get; set; }
        DbSet<DtChuongTrinhKhung> DtChuongTrinhKhungs { get; set; }
        DbSet<DtMonTienQuyet> DtMonTienQuyets { get; set; }
        DbSet<DtChuongTrinhKhungMon> DtChuongTrinhKhungMons { get; set; }
        DbSet<DtQuyDinhThangDiem> DtQuyDinhThangDiems { get; set; }
        #endregion

        #region sv

        DbSet<SvSinhVien> SvSinhViens { get; set; }
        DbSet<SvDiemMonHoc> SvDiemMonHocs { get; set; }
        DbSet<SvThongTinHocVu> SvThongTinHocVus { get; set; }
        DbSet<SvKetQuaHocKy> svKetQuaHocKys { get; set; }
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

        #region Kpi
        public DbSet<KpiCaNhan> KpiCaNhans { get; set; }
        public DbSet<KpiDonVi> KpiDonVis { get; set; }
        public DbSet<KpiLogStatus> KpiLogStatuses { get; set; }
        public DbSet<KpiRole> KpiRoles { get; set; }
        public DbSet<KpiTemplate> KpiTemplates { get; set; }
        public DbSet<KpiTruong> KpiTruongs { get; set; }
        public DbSet<KpiCongThuc> KpiCongThucs { get; set; }

        #endregion

        #region Sysvar
        public DbSet<SysVar> SysVars { get; set; }

        #endregion

        #region Survey
        public DbSet<KsSurvey> KsSurvey { get; set; }
        public DbSet<KsSurveyRequest> KsSurveyRequest { get; set; }
        public DbSet<KsSurveyQuestion> KsSurveyQuestion { get; set; }
        public DbSet<KsQuestionAnswer> KsQuestionAnswer { get; set; }
        public DbSet<KsSurveyTarget> KsSurveyTarget { get; set; }
        public DbSet<KsSurveyCriteria> KsSurveyCriteria { get; set; }
        public DbSet<KsSurveySubmission> KsSurveySubmission { get; set; }
        public DbSet<KsSurveySubmissionAnswer> KsSurveySubmissionAnswer { get; set; }
        public DbSet<KsSurveyReport> KsSurveyReport { get; set; }
        public DbSet<KsAIResponse> KsAIResponse { get; set; }
        public DbSet<KsSurveyLog> KsSurveyLog { get; set; }
        public DbSet<KsSurveySubmissionLog> KsSurveySubmissionLog { get; set; }
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

            modelBuilder.Entity<KsSurveySubmissionAnswer>()
                .HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.IdCauHoi)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<KsSurveySubmissionAnswer>()
                .HasOne(a => a.SelectedAnswer)
                .WithMany()
                .HasForeignKey(a => a.IdDapAnChon)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.SeedDataHrm();
            // Seed data đã được chạy qua SQL script: Scripts/seed_all_data.sql
            // modelBuilder.SeedDataDt();
            // modelBuilder.SeedDataSv();
            base.OnModelCreating(modelBuilder);
        }
    }
}
