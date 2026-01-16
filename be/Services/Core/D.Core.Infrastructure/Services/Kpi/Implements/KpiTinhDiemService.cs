using AutoMapper;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiTinhDiemService : ServiceBase, IKpiTinhDiemService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiTinhDiemService(
            ILogger<KpiTinhDiemService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetDiemTongKetNhanSu(int nhanSuId, string namHoc)
        {
            var roles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdNhanSu == nhanSuId)
                .ToListAsync();

            if (roles == null || !roles.Any()) return 0;
            if (roles.Any(r => r.Role == "HIEU_TRUONG"))
            {
                var kpiTruongs = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                    .Where(k => k.NamHoc == namHoc && k.TrangThai == KpiStatus.PrincipalApprove && !k.Deleted)
                    .ToListAsync();
                return TinhCongThucChung(kpiTruongs.Select(x => (x.LoaiKpi, x.DiemKpiCapTren)));
            }

            decimal tongDiemTichHop = 0;
            foreach (var role in roles)
            {
                decimal diemTaiRoleNay = await TinhDiemChoMotRole(nhanSuId, namHoc, role);
                tongDiemTichHop += diemTaiRoleNay * (role.TiLe ?? 0) / 100;
            }

            return tongDiemTichHop;
        }

        public async Task<decimal> GetDiemTongKetNhanSuTrongPhamVi(int nhanSuId, string namHoc, int idDonViQuanLy)
        {
            var roles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdNhanSu == nhanSuId && r.IdDonVi == idDonViQuanLy)
                .ToListAsync();

            decimal tongDiem = 0;
            foreach (var role in roles)
            {
                if (role.Role == "HIEU_TRUONG" || role.Role == "TRUONG_DON_VI_CAP_2") continue;

                decimal diemTaiRole = await TinhDiemChoMotRole(nhanSuId, namHoc, role);
                tongDiem += diemTaiRole * (role.TiLe ?? 0) / 100;
            }
            return tongDiem;
        }

        private async Task<decimal> TinhDiemChoMotRole(int nhanSuId, string namHoc, KpiRole role)
        {
            if (role.Role == "TRUONG_DON_VI_CAP_2" || role.Role == "TRUONG_DON_VI_CAP_3")
            {
                var kpis = await _unitOfWork.iKpiDonViRepository.TableNoTracking
                    .Where(k => k.IdDonVi == role.IdDonVi && k.NamHoc == namHoc
                           && k.TrangThai == KpiStatus.PrincipalApprove && !k.Deleted)
                    .ToListAsync();
                return TinhCongThucChung(kpis.Select(x => (x.LoaiKpi, x.DiemKpiCapTren)));
            }
            else
            {
                var kpis = await _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                    .Where(k => k.IdNhanSu == nhanSuId && k.IdKpiDonVi == role.IdDonVi
                           && k.NamHoc == namHoc && k.Status == KpiStatus.PrincipalApprove && !k.Deleted)
                    .ToListAsync();
                return TinhCongThucChung(kpis.Select(x => (x.LoaiKPI, x.DiemKpiCapTren)));
            }
        }

        public decimal TinhCongThucChung(IEnumerable<(int? loai, decimal? diem)> dsKpi)
        {
            if (dsKpi == null || !dsKpi.Any()) return 0;
            var tongLoai12 = dsKpi.Where(x => x.loai == 1 || x.loai == 2).Sum(x => x.diem ?? 0);
            var tongLoai3 = dsKpi.Where(x => x.loai == 3).Sum(x => x.diem ?? 0);
            return tongLoai12 - tongLoai3;
        }
    }
}
