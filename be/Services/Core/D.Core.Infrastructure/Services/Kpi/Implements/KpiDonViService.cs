using AutoMapper;
using D.Auth.Domain;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.Core.Infrastructure.Services.Kpi.Common;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiDonViService : ServiceBase, IKpiDonViService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public KpiDonViService(
            ILogger<KpiRoleService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;

        }
        public int? GetKpiIsActive()
        {
            var sysVarValue = _unitOfWork.iSysVarRepository
                .TableNoTracking.Where(x => x.GrName == "KPI_SETTING_ISACTIVE")
                .Select(x => x.VarName)
                .FirstOrDefault();

            return int.TryParse(sysVarValue, out var parsedValue) ? parsedValue : (int?)null;
        }

        public void CreateKpiDonVi(CreateKpiDonViDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiDonVi)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var donVi = _unitOfWork.iDmPhongBanRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.IdDonVi);
            if (donVi == null)
                throw new Exception("Không tìm thấy đơn vị");

            var entity = _mapper.Map<KpiDonVi>(dto);
            //entity.STT = maxSTT + 1;
            _unitOfWork.iKpiDonViRepository.Add(entity);
            _unitOfWork.iKpiDonViRepository.SaveChange();
        }

        public void DeleteKpiDonVi(DeleteKpiDonViDto dto)
        {
            _logger.LogInformation($"{nameof(DeleteKpiDonVi)} method called.");
            var kpi = _unitOfWork.iKpiDonViRepository.Table.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new Exception($"KPI đơn vị với Id = {dto.Id} không tồn tại hoặc đã bị xóa.");
            }

            kpi.Deleted = true;

            _unitOfWork.iKpiDonViRepository.Update(kpi);
            _unitOfWork.iKpiDonViRepository.SaveChange();
        }

        public PageResultDto<KpiDonViDto> FindPagingKeKhai(FilterKpiDonViKeKhaiDto dto)
        {
            _logger.LogInformation(
        $"{nameof(FindPagingKeKhai)} => dto = {JsonSerializer.Serialize(dto)}"
    );

            var isActive = GetKpiIsActive();
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);

            var donViIdsQuanLy = _unitOfWork.iKpiRoleRepository
                .TableNoTracking
                .Where(x =>
                    x.IdNhanSu == userId &&
                    x.Role == "TRUONG_DON_VI_CAP_2"
                )
                .Select(x => x.IdDonVi)
                .Distinct()
                .ToList();

            if (!donViIdsQuanLy.Any())
            {
                return new PageResultDto<KpiDonViDto>
                {
                    Items = new List<KpiDonViDto>(),
                    TotalItem = 0
                };
            }

            var kpis = _unitOfWork.iKpiDonViRepository.TableNoTracking.ToList();
            var donvis = _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .ToDictionary(x => x.Id, x => x.TenPhongBan);

            var query =
                from kpi in kpis
                where
                    !kpi.Deleted
                    && donViIdsQuanLy.Contains(kpi.IdDonVi)
                    && (string.IsNullOrEmpty(dto.Keyword)
                        || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower().Trim()))
                    && (dto.IdDonVi == null || kpi.IdDonVi == dto.IdDonVi)
                    && (dto.LoaiKpi == null || kpi.LoaiKpi == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.TrangThai == dto.TrangThai)
                select new KpiDonViDto
                {
                    Id = kpi.Id,
                    Kpi = kpi.Kpi,
                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    IdDonVi = kpi.IdDonVi,
                    DonVi = kpi.IdDonVi != null && donvis.ContainsKey(kpi.IdDonVi.Value)
                        ? donvis[kpi.IdDonVi.Value]
                        : string.Empty,
                    LoaiKpi = kpi.LoaiKpi,
                    CongThuc = kpi.CongThucTinh,
                    NamHoc = kpi.NamHoc,
                    TrangThai = kpi.TrangThai,
                    KetQuaThucTe = kpi.KetQuaThucTe,
                    CapTrenDanhGia = kpi.CapTrenDanhGia,
                    DiemKpiCapTren = kpi.DiemKpiCapTren,
                    DiemKpi = kpi.DiemKpi,
                    LoaiKetQua = kpi.LoaiKetQua,
                    GhiChu = kpi.GhiChu,
                    IsActive =
                        (kpi.TrangThai == KpiStatus.Assigned
                         || kpi.TrangThai == KpiStatus.NeedEdit
                         || kpi.TrangThai == KpiStatus.Declared)
                            ? isActive
                            : 0
                };

            var totalCount = query.Count();
            var pagedItems = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<KpiDonViDto>
            {
                Items = pagedItems,
                TotalItem = totalCount
            };
        }

        public PageResultDto<KpiDonViDto> GetAllKpiDonVi(FilterKpiDonViDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllKpiDonVi)} => dto = {JsonSerializer.Serialize(dto)}"
            );
            var isActive = GetKpiIsActive();
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var userRoles = _unitOfWork.iKpiRoleRepository
                .TableNoTracking
                .Where(x => x.IdNhanSu == userId)
                .ToList();
            List<int>? allowedDonViIds = null;
            if (userRoles.Any(r => r.Role == "HIEU_TRUONG"))
            {
                allowedDonViIds = null;
            }
            else if (userRoles.Any(r => r.Role == "PHO_HIEU_TRUONG"))
            {
                allowedDonViIds = userRoles
                    .Where(r => r.Role == "PHO_HIEU_TRUONG" && r.IdDonVi.HasValue)
                    .Select(r => r.IdDonVi!.Value)
                    .Distinct()
                    .ToList();
            }
            else
            {
                return new PageResultDto<KpiDonViDto>
                {
                    Items = new List<KpiDonViDto>(),
                    TotalItem = 0
                };
            }
            //var kpis = _unitOfWork.iKpiDonViRepository.TableNoTracking.ToList();
            var donvis = _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToDictionary(x => x.Id, x => x.TenPhongBan);
            var kpiQuery = _unitOfWork.iKpiDonViRepository
                    .TableNoTracking
                    .Where(kpi =>
                        !kpi.Deleted
                        && kpi.IdDonVi.HasValue
                        && (allowedDonViIds == null || allowedDonViIds.Contains(kpi.IdDonVi.Value))
                        && (string.IsNullOrEmpty(dto.Keyword) || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower()))
                        && (dto.IdDonVi == null || kpi.IdDonVi == dto.IdDonVi)
                        && (dto.LoaiKpi == null || kpi.LoaiKpi == dto.LoaiKpi)
                        && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                        && (dto.TrangThai == null || kpi.TrangThai == dto.TrangThai)
                    );
            var summary = new KpiDonViSummaryDto
            {
                TongTuDanhGia = kpiQuery.Sum(x => (decimal?)(x.DiemKpi ?? 0)) ?? 0,
                TongCapTren = kpiQuery.Sum(x => (decimal?)(x.DiemKpiCapTren ?? 0)) ?? 0,

                ByLoaiKpi = kpiQuery
                    .Where(x => x.LoaiKpi.HasValue)
                    .GroupBy(x => x.LoaiKpi!.Value)
                    .Select(g => new KpiDonViSummaryByLoaiDto
                    {
                        LoaiKpi = g.Key,
                        TuDanhGia = g.Sum(x => (decimal?)(x.DiemKpi ?? 0)) ?? 0,
                        CapTren = g.Sum(x => (decimal?)(x.DiemKpiCapTren ?? 0)) ?? 0
                    })
                    .ToList()
            };
            var totalCount = kpiQuery.Count();

            var pagedItems = kpiQuery
                    .OrderBy(x => x.Id)
                    .Skip(dto.SkipCount())
                    .Take(dto.PageSize)
                    .Select(kpi => new KpiDonViDto
                    {
                        Id = kpi.Id,
                        Kpi = kpi.Kpi,
                        MucTieu = kpi.MucTieu,
                        TrongSo = kpi.TrongSo,
                        IdDonVi = kpi.IdDonVi,
                        DonVi = kpi.IdDonVi.HasValue && donvis.ContainsKey(kpi.IdDonVi.Value)
                            ? donvis[kpi.IdDonVi.Value]
                            : string.Empty,
                        LoaiKpi = kpi.LoaiKpi,
                        CongThuc = kpi.CongThucTinh,
                        NamHoc = kpi.NamHoc,
                        TrangThai = kpi.TrangThai,
                        KetQuaThucTe = kpi.KetQuaThucTe,
                        DiemKpi = kpi.DiemKpi,
                        DiemKpiCapTren = kpi.DiemKpiCapTren,
                        CapTrenDanhGia = kpi.CapTrenDanhGia,
                        LoaiKetQua = kpi.LoaiKetQua,
                        IsActive = kpi.TrangThai == KpiStatus.Evaluated ? 0 : isActive,
                    })
                    .ToList();

            //var totalCount = query.Count();
            //var pagedItems = query
            //    .Skip(dto.SkipCount())
            //    .Take(dto.PageSize)
            //    .ToList();

            return new PageResultDto<KpiDonViDto>
            {
                Items = pagedItems,
                TotalItem = totalCount,
                Summary = summary
            };
        }

        public List<TrangThaiKpiDonViResponseDto> GetListTrangThai()
        {
            _logger.LogInformation($"{nameof(GetListTrangThai)}");


            var trangThaiExist = _unitOfWork.iKpiDonViRepository
                .TableNoTracking
                .Where(x => x.TrangThai != null)
                .Select(x => x.TrangThai!.Value)
                .Distinct()
                .OrderBy(x => x)
                .Select(x => new TrangThaiKpiDonViResponseDto
                {
                    Value = x,
                    Label = KpiStatus.Names.ContainsKey(x)
                        ? KpiStatus.Names[x]
                        : "Không xác định"
                })
                .ToList();

            return trangThaiExist;
        }

        public List<GetListYearKpiDonViDto> GetListYear()
        {
            _logger.LogInformation($"{nameof(GetListYear)} ");
            var years = _unitOfWork.iKpiDonViRepository
                        .TableNoTracking
                        .Where(x => !string.IsNullOrEmpty(x.NamHoc))
                        .Select(x => x.NamHoc)
                        .Distinct()
                        .OrderByDescending(x => x)
                        .ToList();
            var result = years.Select(y => new GetListYearKpiDonViDto
            {
                NamHoc = y
            }).ToList();

            return result;
        }

        public List<NhanSuDaGiaoDto> GetNhanSuByKpiDonVi([FromQuery] GetNhanSuFromKpiDonViDto dto)
        {
            var kpiCaNhans = _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                .Where(x => x.IdKpiDonVi == dto.IdKpiDonVi && !x.Deleted)
                .ToList();

            if (!kpiCaNhans.Any())
                return new List<NhanSuDaGiaoDto>();
            var result = kpiCaNhans.Select(x =>
            {
                var ns = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(n => n.Id == x.IdNhanSu);
                return new NhanSuDaGiaoDto
                {
                    Id = x.Id,
                    IdNhanSu = x.IdNhanSu,
                    HoTen = $"{ns?.HoDem} {ns?.Ten}",
                    TrongSo = x.TrongSo,
                    IdKpiDonVi = x.IdKpiDonVi,
                };
            }).ToList();

            return result;
        }

        public async Task GiaoKpiDonVi(GiaoKpiDonViDto dto)
        {

            var kpiDonVi = await _unitOfWork.iKpiDonViRepository.Table
                .FirstOrDefaultAsync(x => x.Id == dto.IdKpiDonVi && !x.Deleted);

            if (kpiDonVi == null)
                throw new Exception("Không tìm thấy KPI đơn vị");

            if (dto.NhanSus == null || !dto.NhanSus.Any())
                throw new Exception("Danh sách nhân sự không được trống");
            var listKpiCaNhan = await _unitOfWork.iKpiCaNhanRepository.Table
                .Where(x => x.IdKpiDonVi == dto.IdKpiDonVi && !x.Deleted)
                .ToListAsync();

            var listNhanSuDtoIds = dto.NhanSus.Select(n => n.IdNhanSu).ToList();

            foreach (var item in listKpiCaNhan.Where(x => !listNhanSuDtoIds.Contains(x.IdNhanSu)))
            {
                item.Deleted = true;
                item.ModifiedDate = DateTime.UtcNow;
            }

            foreach (var nsDto in dto.NhanSus)
            {
                var nhanSuExists = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                    .AnyAsync(x => x.Id == nsDto.IdNhanSu);

                if (!nhanSuExists) continue;

                var existed = listKpiCaNhan
                    .FirstOrDefault(x => x.IdNhanSu == nsDto.IdNhanSu);

                if (existed != null)
                {
                    if (existed.TrongSo != nsDto.TrongSo)
                    {
                        existed.TrongSo = nsDto.TrongSo;
                        existed.ModifiedDate = DateTime.UtcNow;
                    }
                    continue;
                }

                var kpiRole = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .FirstOrDefaultAsync(x =>
                        x.IdNhanSu == nsDto.IdNhanSu &&
                        x.IdDonVi == kpiDonVi.IdDonVi);

                if (kpiRole == null)
                    throw new Exception("Nhân sự không thuộc đơn vị của KPI");

                _unitOfWork.iKpiCaNhanRepository.Add(new KpiCaNhan
                {
                    KPI = kpiDonVi.Kpi,
                    MucTieu = kpiDonVi.MucTieu ?? "0",
                    TrongSo = nsDto.TrongSo,
                    LoaiKPI = kpiDonVi.LoaiKpi ?? 0,
                    IdNhanSu = nsDto.IdNhanSu,
                    IdKpiDonVi = kpiDonVi.Id,
                    NamHoc = kpiDonVi.NamHoc,
                    Role = kpiRole.Role,
                    CongThucTinh = kpiDonVi.CongThucTinh ?? "Chưa có công thức tính",
                    LoaiKetQua = kpiDonVi.LoaiKetQua,
                    IdCongThuc = kpiDonVi.IdCongThuc ?? 0,
                    Status = KpiStatus.Assigned
                });
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public void UpdateKetQuaThucTe(UpdateKpiThucTeKpiDonViListDto dto)
        {
            using var transaction = _unitOfWork.Database.BeginTransaction();
            try
            {
                foreach (var item in dto.Items)
                {
                    var kpi = _unitOfWork.iKpiDonViRepository.TableNoTracking
                        .FirstOrDefault(x => x.Id == item.Id && !x.Deleted);

                    if (kpi == null)
                        throw new Exception("Không tìm thấy KPI cá nhân");

                    if (item.KetQuaThucTe.HasValue)
                    {
                        kpi.KetQuaThucTe = item.KetQuaThucTe;
                        kpi.DiemKpi = item.DiemKpi;
                        kpi.TrangThai = KpiStatus.Declared;

                        kpi.DiemKpi = TinhDiemKPI.TinhDiemChung(
                            kpi.KetQuaThucTe,
                            kpi.MucTieu,
                            kpi.TrongSo,
                            kpi.IdCongThuc,
                            kpi.LoaiKetQua
                         );

                        _unitOfWork.iKpiDonViRepository.Update(kpi);
                    }
                }

                _unitOfWork.iKpiDonViRepository.SaveChange();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void UpdateKpiDonVi(UpdateKpiDonViDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateKpiDonVi)} dto={JsonSerializer.Serialize(dto)}");
            // Tìm KPI cập nhật
            var kpi = _unitOfWork.iKpiDonViRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new Exception($"Không tìm thấy KPI cá nhân với Id={dto.Id}");
            }

            // Cập nhật thông tin
            kpi.Kpi = dto.Kpi;
            kpi.MucTieu = dto.MucTieu;
            kpi.TrongSo = dto.TrongSo;
            kpi.IdDonVi = dto.IdDonVi;
            kpi.LoaiKpi = dto.LoaiKpi;
            kpi.NamHoc = dto.NamHoc;
            kpi.IdKpiTruong = dto.IdKpiTruong;

            _unitOfWork.iKpiDonViRepository.Update(kpi);
            _unitOfWork.iKpiDonViRepository.SaveChange();
        }

        public void UpdateTrangThaiKpiDonVi(UpdateTrangThaiKpiDonViDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateTrangThaiKpiDonVi)} => dto = {JsonSerializer.Serialize(dto)}");

            if (dto.Ids == null || !dto.Ids.Any())
                throw new UserFriendlyException(ErrorCode.BadRequest, "Danh sách ID không được để trống.");

            var kpiList = _unitOfWork.iKpiDonViRepository.TableNoTracking
                .Where(s => dto.Ids.Contains(s.Id))
                .ToList();

            if (!kpiList.Any())
                throw new Exception("Không tìm thấy KPI cá nhân nào để cập nhật.");

            foreach (var kpi in kpiList)
            {
                kpi.TrangThai = dto.TrangThai;
                _unitOfWork.iKpiDonViRepository.Update(kpi);
            }

            _unitOfWork.iKpiDonViRepository.SaveChange();
        }

        public KpiKeKhaiTimeDonViDto GetKpiKeKhaiTime()
        {
            _logger.LogInformation($"{nameof(GetKpiKeKhaiTime)}");

            // Lấy Start / End từ SysVar
            var sysVars = _unitOfWork.iSysVarRepository
                .Table
                .Where(x =>
                    x.GrName == "KPI_KeKhaiDonVi_Time" &&
                    (x.VarName == "START_DATE" || x.VarName == "END_DATE"))
                .Select(x => new
                {
                    x.VarName,
                    x.VarValue
                })
                .ToList();

            DateTime? startDate = null;
            DateTime? endDate = null;

            foreach (var item in sysVars)
            {
                // Bỏ qua nếu giá trị cấu hình rỗng hoặc chưa được nhập
                if (string.IsNullOrWhiteSpace(item.VarValue))
                    continue;

                if (!DateTime.TryParseExact(
                    item.VarValue,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate))
                {
                    _logger.LogWarning("SysVar KPI_KeKhaiDonVi_Time - {VarName} có giá trị không hợp lệ: {VarValue}", item.VarName, item.VarValue);
                    continue;
                }

                if (item.VarName == "START_DATE")
                    startDate = parsedDate;

                if (item.VarName == "END_DATE")
                    endDate = parsedDate;
            }

            var today = DateTime.Now.Date;

            var isKeKhaiTime = startDate.HasValue
                && endDate.HasValue
                && today >= startDate.Value.Date
                && today <= endDate.Value.Date;

            return new KpiKeKhaiTimeDonViDto
            {
                IsKeKhaiTime = isKeKhaiTime,
                StartDate = startDate ?? DateTime.MinValue,
                EndDate = endDate ?? DateTime.MinValue
            };
        }

        public void UpdateKetQuaCapTren(UpdateKetQuaCapTrenKpiDonViListDto dto)
        {
            using var transaction = _unitOfWork.Database.BeginTransaction();
            try
            {
                foreach (var item in dto.Items)
                {
                    var kpi = _unitOfWork.iKpiDonViRepository.Table
                        .FirstOrDefault(x => x.Id == item.Id && !x.Deleted);

                    if (kpi == null)
                        throw new Exception("Không tìm thấy KPI cá nhân");

                    if (item.KetQuaCapTren.HasValue)
                    {
                        kpi.CapTrenDanhGia = item.KetQuaCapTren;
                        kpi.DiemKpiCapTren = item.DiemKpiCapTren;
                        kpi.TrangThai = KpiStatus.Evaluated;
                        kpi.DiemKpiCapTren = TinhDiemKPI.TinhDiemChung(
                            kpi.CapTrenDanhGia,
                            kpi.MucTieu,
                            kpi.TrongSo,
                            kpi.IdCongThuc,
                            kpi.LoaiKetQua
                        );

                        _unitOfWork.iKpiDonViRepository.Update(kpi);
                    }
                }

                _unitOfWork.iKpiDonViRepository.SaveChange();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<GetTrangThaiKpiTruongByKpiDonViResponseDto> GetTrangThaiKpiTruongByKpiDonViAsync(GetTrangThaiKpiTruongByKpiDonViRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetTrangThaiKpiTruongByKpiDonViAsync)} => idKpiDonVi = {dto.idKpiDonVi}");

            var kpiDonVi = await _unitOfWork.iKpiDonViRepository.Table
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == dto.idKpiDonVi && !x.Deleted);

            if (kpiDonVi == null)
                throw new Exception("Khong tìm thấy KPi Đơn vị");

            var kpiTruong = await _unitOfWork.iKpiTruongRepository.Table
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == kpiDonVi.IdKpiTruong && !x.Deleted
                );

            if (kpiTruong == null)
                throw new Exception("Khong tìm thấy KPi Trường");

            return new GetTrangThaiKpiTruongByKpiDonViResponseDto
            {
                IdKpiTruong = kpiTruong.Id,
                TrangThai = kpiTruong.TrangThai
            };
        }
    }
}
