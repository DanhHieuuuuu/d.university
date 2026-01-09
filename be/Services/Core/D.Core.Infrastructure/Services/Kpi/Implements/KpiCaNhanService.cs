using AutoMapper;
using d.Shared.Permission.Error;
using D.Auth.Domain;
using D.ControllerBase.Exceptions;
using D.Core.Domain;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.Core.Infrastructure.Services.Kpi.Common;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using System.Globalization;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiCaNhanService : ServiceBase, IKpiCaNhanService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public KpiCaNhanService(
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

        public void CreateKpiCaNhan(CreateKpiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiCaNhan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );
            var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.IdNhanSu);
            if (nhanSu == null)
                throw new Exception("Không tìm thấy nhân sự");

            var maxSTT = _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                    .Where(k =>
                        k.IdNhanSu == nhanSu.Id
                        && k.LoaiKPI == dto.LoaiKPI
                        && !k.Deleted
                    )
                    .Max(k => (int?)k.STT) ?? 0;
            var entity = _mapper.Map<KpiCaNhan>(dto);
            entity.STT = maxSTT + 1;
            _unitOfWork.iKpiCaNhanRepository.Add(entity);
            _unitOfWork.iKpiCaNhanRepository.SaveChange();
        }

        public void DeleteKpiCaNhan(DeleteKpiCaNhanDto dto)
        {
            _logger.LogInformation($"{nameof(DeleteKpiCaNhan)} method called.");
            var kpi = _unitOfWork.iKpiCaNhanRepository.Table.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new Exception($"KPI cá nhân với Id = {dto.Id} không tồn tại hoặc đã bị xóa.");
            }

            kpi.Deleted = true;

            _unitOfWork.iKpiCaNhanRepository.Update(kpi);
            _unitOfWork.iKpiCaNhanRepository.SaveChange();
        }

        public async Task<PageResultDto<KpiCaNhanDto>> GetAllKpiCaNhan(FilterKpiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllKpiCaNhan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var allowedUserIds = await GetAllowedUserIds(userId);
            var isActive = GetKpiIsActive();
            List<int> nhanSuIdsByDonVi = new();

            if (dto.IdPhongBan.HasValue)
            {
                nhanSuIdsByDonVi = await _unitOfWork.iKpiRoleRepository
                    .TableNoTracking
                    .Where(x => x.IdDonVi == dto.IdPhongBan)
                    .Select(x => x.IdNhanSu)
                    .Distinct()
                    .ToListAsync();
            }

            var phongBans = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);

            var nhanSus = await _unitOfWork.iNsNhanSuRepository
                .TableNoTracking
                .Where(ns =>
                    (!dto.IdNhanSu.HasValue || ns.Id == dto.IdNhanSu) &&
                    (!dto.IdPhongBan.HasValue || nhanSuIdsByDonVi.Contains(ns.Id))
                )
                .ToDictionaryAsync(
                    x => x.Id,
                    x => new
                    {
                        HoTenDayDu = (x.HoDem + " " + x.Ten).Trim(),
                        TenPhongBan = dto.IdPhongBan.HasValue && phongBans.ContainsKey(dto.IdPhongBan.Value)
                            ? phongBans[dto.IdPhongBan.Value]
                            : string.Empty
                    }
                );
            var kpis = await _unitOfWork.iKpiCaNhanRepository
                .TableNoTracking
                .Where(kpi =>
                    !kpi.Deleted &&
                    allowedUserIds.Contains(kpi.IdNhanSu) &&
                    (string.IsNullOrEmpty(dto.Keyword) ||
                        kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim())) &&
                    (!dto.LoaiKpi.HasValue || kpi.LoaiKPI == dto.LoaiKpi) &&
                    (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc) &&
                    (!dto.TrangThai.HasValue || kpi.Status == dto.TrangThai)
                )
                .ToListAsync();
            var filteredKpis = kpis.Where(kpi => nhanSus.ContainsKey(kpi.IdNhanSu));
            var total = filteredKpis.Count();

            var resultItems = kpis
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .Select(kpi => new KpiCaNhanDto
                {
                    Id = kpi.Id,
                    STT = kpi.STT,
                    KPI = kpi.KPI,
                    LoaiKpi = kpi.LoaiKPI,
                    LinhVuc = kpi.LinhVuc,
                    ChienLuoc = kpi.ChienLuoc,

                    IdNhanSu = kpi.IdNhanSu,
                    NhanSu = nhanSus[kpi.IdNhanSu].HoTenDayDu,
                    PhongBan = nhanSus[kpi.IdNhanSu].TenPhongBan,

                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    Role = kpi.Role,
                    NamHoc = kpi.NamHoc,
                    TrangThai = kpi.Status,
                    LoaiKetQua = kpi.LoaiKetQua,
                    KetQuaThucTe = kpi.KetQuaThucTe,
                    CapTrenDanhGia = kpi.CapTrenDanhGia,
                    DiemKpiCapTren = kpi.DiemKpiCapTren,
                    DiemKpi = kpi.DiemKpi,
                    IsActive = kpi.Status == KpiStatus.Scored ? 0 : isActive,
                    CongThuc = kpi.CongThucTinh
                })
                .ToList();

            return new PageResultDto<KpiCaNhanDto>
            {
                Items = resultItems,
                TotalItem = total
            };
        }



        public async Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhanKeKhai(
            FilterKpiKeKhaiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingKpiCaNhanKeKhai)} => dto = {JsonSerializer.Serialize(dto)}"
            );

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var isActive = GetKpiIsActive();
            var user = await _unitOfWork.iNsNhanSuRepository
                .TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == userId);

            var phongBans = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);


            var query = _unitOfWork.iKpiCaNhanRepository
                .TableNoTracking
                .Where(kpi =>
                    !kpi.Deleted
                    && kpi.IdNhanSu == user.Id
                    && (
                        string.IsNullOrEmpty(dto.Keyword)
                        || kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim())
                    )
                    && (dto.LoaiKpi == null || kpi.LoaiKPI == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.Status == dto.TrangThai)
                    && (string.IsNullOrEmpty(dto.Role) || kpi.Role == dto.Role)
                )
                .Select(kpi => new KpiCaNhanDto
                {
                    Id = kpi.Id,
                    STT = kpi.STT,
                    KPI = kpi.KPI,
                    LoaiKpi = kpi.LoaiKPI,
                    LinhVuc = kpi.LinhVuc,
                    ChienLuoc = kpi.ChienLuoc,
                    IdNhanSu = kpi.IdNhanSu,
                    NhanSu = user.HoDem + " " + user.Ten,
                    PhongBan = user.HienTaiPhongBan != null &&
                               phongBans.ContainsKey(user.HienTaiPhongBan.Value)
                        ? phongBans[user.HienTaiPhongBan.Value]
                        : string.Empty,
                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    Role = kpi.Role,
                    NamHoc = kpi.NamHoc,
                    LoaiKetQua = kpi.LoaiKetQua,
                    TrangThai = kpi.Status,
                    KetQuaThucTe = kpi.KetQuaThucTe,
                    CapTrenDanhGia = kpi.CapTrenDanhGia,
                    DiemKpiCapTren = kpi.DiemKpiCapTren,
                    DiemKpi = kpi.DiemKpi,
                    IsCaNhanKeKhai = kpi.IsCaNhanKeKhai,
                    CongThuc = kpi.CongThucTinh,
                    GhiChu = kpi.GhiChu,
                    IsActive = (
                            kpi.Status == KpiStatus.Evaluating
                            || kpi.Status == KpiStatus.NeedEdit
                            || kpi.Status == KpiStatus.Declared
                        )
                            ? isActive
                            : 0,
                });

            var totalCount = await query.CountAsync();

            var pagedItems = await query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToListAsync();

            return new PageResultDto<KpiCaNhanDto>
            {
                Items = pagedItems,
                TotalItem = totalCount
            };
        }

        public List<TrangThaiResponseDto> GetListTrangThai()
        {
            _logger.LogInformation($"{nameof(GetListTrangThai)}");

            var trangThaiExist = _unitOfWork.iKpiCaNhanRepository
                .TableNoTracking
                .Where(x => x.Status != null)
                .Select(x => x.Status!.Value)
                .Distinct()
                .OrderBy(x => x)
                .Select(x => new TrangThaiResponseDto
                {
                    Value = x,
                    Label = KpiStatus.Names.ContainsKey(x)
                        ? KpiStatus.Names[x]
                        : "Không xác định"
                })
                .ToList();

            return trangThaiExist;
        }

        public void UpdateKetQuaThucTe(UpdateKpiThucTeKpiCaNhanListDto dto)
        {
            using var transaction = _unitOfWork.Database.BeginTransaction();
            try
            {
                foreach (var item in dto.Items)
                {
                    var kpi = _unitOfWork.iKpiCaNhanRepository.Table
                        .FirstOrDefault(x => x.Id == item.Id && !x.Deleted);

                    if (kpi == null)
                        throw new Exception("Không tìm thấy KPI cá nhân");

                    if (item.KetQuaThucTe.HasValue)
                    {
                        kpi.KetQuaThucTe = item.KetQuaThucTe;
                        kpi.DiemKpi = item.DiemKpi;
                        kpi.Status = KpiStatus.Declared;

                        kpi.DiemKpi = TinhDiemKPI.TinhDiemChung(
                            kpi.KetQuaThucTe,
                            kpi.MucTieu,
                            kpi.TrongSo,
                            kpi.IdCongThuc,
                            kpi.LoaiKetQua
                         );

                        _unitOfWork.iKpiCaNhanRepository.Update(kpi);
                    }
                }

                _unitOfWork.iKpiCaNhanRepository.SaveChange();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }





        public void UpdateKpiCaNhan(UpdateKpiCaNhanDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateKpiCaNhan)} dto={JsonSerializer.Serialize(dto)}");
            // Tìm KPI cập nhật
            var kpi = _unitOfWork.iKpiCaNhanRepository.Table.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.CodeNotFound, $"Không tìm thấy KPI cá nhân với Id={dto.Id}");
            }

            if (kpi.IdNhanSu != dto.IdNhanSu)
            {
                var maxSTT = _unitOfWork.iKpiCaNhanRepository.Table
                    .Where(k => k.IdNhanSu == dto.IdNhanSu && k.LoaiKPI == dto.LoaiKPI && !k.Deleted)
                    .Select(k => (int?)k.STT)
                    .Max() ?? 0;
                kpi.STT = maxSTT + 1;
            }

            // Cập nhật dữ liệu
            kpi.KPI = dto.KPI;
            kpi.MucTieu = dto.MucTieu;
            kpi.TrongSo = dto.TrongSo;
            kpi.IdNhanSu = dto.IdNhanSu;
            kpi.LoaiKPI = dto.LoaiKPI;
            kpi.NamHoc = dto.NamHoc;
            kpi.Status = KpiStatus.Edited;

            _unitOfWork.iKpiCaNhanRepository.Update(kpi);
            _unitOfWork.iKpiCaNhanRepository.SaveChange();
        }

        public void UpdateTrangThaiKpiCaNhan(UpdateTrangThaiKpiDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateTrangThaiKpiCaNhan)} => dto = {JsonSerializer.Serialize(dto)}");

            if (dto.Ids == null || !dto.Ids.Any())
                throw new UserFriendlyException(ErrorCode.BadRequest, "Danh sách ID không được để trống.");

            var kpiList = _unitOfWork.iKpiCaNhanRepository.Table
                .Where(s => dto.Ids.Contains(s.Id))
                .ToList();

            if (!kpiList.Any())
                throw new Exception("Không tìm thấy KPI cá nhân nào để cập nhật.");

            foreach (var kpi in kpiList)
            {
                kpi.Status = dto.TrangThai;
                _unitOfWork.iKpiCaNhanRepository.Update(kpi);
            }

            _unitOfWork.iKpiCaNhanRepository.SaveChange();
        }


        public void UpdateKetQuaCapTren(UpdateKetQuaCapTrenKpiCaNhanListDto dto)
        {
            using var transaction = _unitOfWork.Database.BeginTransaction();
            try
            {
                foreach (var item in dto.Items)
                {
                    var kpi = _unitOfWork.iKpiCaNhanRepository.Table
                        .FirstOrDefault(x => x.Id == item.Id && !x.Deleted);

                    if (kpi == null)
                        throw new Exception("Không tìm thấy KPI cá nhân");

                    if (item.KetQuaCapTren.HasValue)
                    {
                        kpi.CapTrenDanhGia = item.KetQuaCapTren;
                        kpi.DiemKpiCapTren = item.DiemKpiCapTren;
                        kpi.Status = KpiStatus.Evaluated;
                        kpi.DiemKpiCapTren = TinhDiemKPI.TinhDiemChung(
                            kpi.CapTrenDanhGia,
                            kpi.MucTieu,
                            kpi.TrongSo,
                            kpi.IdCongThuc,
                            kpi.LoaiKetQua
                        );

                        _unitOfWork.iKpiCaNhanRepository.Update(kpi);
                    }
                }

                _unitOfWork.iKpiCaNhanRepository.SaveChange();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public int? GetKpiIsActive()
        {
            var sysVarValue = _unitOfWork.iSysVarRepository
                .TableNoTracking.Where(x => x.GrName == "KPI_SETTING_ISACTIVE")
                .Select(x => x.VarName)
                .FirstOrDefault(); // đồng bộ

            return int.TryParse(sysVarValue, out var parsedValue) ? parsedValue : (int?)null;
        }

        public KpiKeKhaiTimeCaNhanDto GetKpiKeKhaiTime()
        {
            _logger.LogInformation($"{nameof(GetKpiKeKhaiTime)}");

            // Lấy Start / End từ SysVar
            var sysVars = _unitOfWork.iSysVarRepository
                .Table
                .Where(x =>
                    x.GrName == "KPI_KeKhai_Time" &&
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
                    _logger.LogWarning("SysVar KPI_KeKhai_Time - {VarName} có giá trị không hợp lệ: {VarValue}", item.VarName, item.VarValue);
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

            return new KpiKeKhaiTimeCaNhanDto
            {
                IsKeKhaiTime = isKeKhaiTime,
                StartDate = startDate ?? DateTime.MinValue,
                EndDate = endDate ?? DateTime.MinValue
            };
        }

        public List<GetAllNhanSuKiemNhiemResponseDto> GetAllNhanSuKiemNhiem(GetAllNhanSuKiemNhiemRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllNhanSuKiemNhiem)} ");

            var nhanSuRoleIds = _unitOfWork.iKpiRoleRepository.Table
                .Where(x => x.IdDonVi == dto.idPhongBan)
                .Select(x => x.IdNhanSu);

            var query =
                from ns in _unitOfWork.iNsNhanSuRepository.TableNoTracking
                join pb in _unitOfWork.iDmPhongBanRepository.TableNoTracking
                    on ns.HienTaiPhongBan equals pb.Id into pbJoin
                from pb in pbJoin.DefaultIfEmpty() 
                where
                    ns.DaVeHuu == false
                    && ns.IsThoiViec == false
                    && ns.MaNhanSu != null
                    && (
                        (ns.HienTaiPhongBan == dto.idPhongBan && !ns.MaNhanSu.StartsWith("TG"))
                        || nhanSuRoleIds.Contains(ns.Id)
                    )
                select new GetAllNhanSuKiemNhiemResponseDto
                {
                    IdNhanSu = ns.Id,
                    TenPhongBan = pb != null ? pb.TenPhongBan : null,
                    TenNhanSu = ns.HoDem + ns.Ten,
                    HienTaiPhongBan = ns.HienTaiPhongBan,
                    MaNhanSu = ns.MaNhanSu,
                    TenHienThi =
                        ns.MaNhanSu
                        + " - "
                        + ns.HoDem + ns.Ten
                        + (pb != null ? " - " + pb.TenPhongBan : "")
                };

            return query.ToList();
        }
        public async Task<List<int>> GetAllowedUserIds(int currentUserId)
        {
            var userRoles = await _unitOfWork.iKpiRoleRepository
                .TableNoTracking
                .Where(r => r.IdNhanSu == currentUserId)
                .ToListAsync();

            var allKpiRoles = await _unitOfWork.iKpiRoleRepository
                .TableNoTracking
                .ToListAsync();

            var allowedUserIds = new HashSet<int>();

            foreach (var role in userRoles)
            {
                switch (role.Role)
                {
                    // Nhân viên bình thường: chỉ thấy chính mình
                    case "GIANG_VIEN":
                    case "KY_SU":
                    case "CHUYEN_VIEN":
                    case "GIANG_VIEN_MOI_VAO":
                    case "CHUYEN_VIEN_VP_KHOA_KHONG_CO_CN":
                    case "CHUYEN_VIEN_VP_KHOA_CO_CN":
                    case "KY_SU_HUONG_DAN_TN":
                    case "GIANG_VIEN_KIEM_NHIEM":
                        allowedUserIds.Add(currentUserId);
                        break;

                    // Trưởng đơn vị: thấy nhân viên + phó trưởng
                    case "TRUONG_DON_VI_CAP_2":
                    case "TRUONG_DON_VI_CAP_3":
                        if (role.IdDonVi.HasValue)
                        {
                            var membersInUnit = allKpiRoles
                                .Where(r =>
                                    r.IdDonVi == role.IdDonVi &&
                                    r.Role != role.Role &&   
                                    r.Role != "HIEU_TRUONG" &&
                                    r.Role != "CHU_TICH_HOI_DONG_TRUONG" &&
                                    r.Role != "PHO_HIEU_TRUONG"
                                )
                                .Select(r => r.IdNhanSu)
                                .ToList();

                            allowedUserIds.UnionWith(membersInUnit);

                            // Thêm phó trưởng trong đơn vị
                            var phoTruong = allKpiRoles
                                .Where(r => r.IdDonVi == role.IdDonVi && r.Role == "PHO_DON_VI_CAP_2")
                                .Select(r => r.IdNhanSu)
                                .ToList();

                            allowedUserIds.UnionWith(phoTruong);
                        }
                        break;

                    case "PHO_DON_VI_CAP_2":
                        allowedUserIds.Add(currentUserId);
                        break;

                    // Hiệu trưởng / Chủ tịch: thấy tất cả nhân viên + trưởng/phó đơn vị
                    case "HIEU_TRUONG":
                    case "CHU_TICH_HOI_DONG_TRUONG":
                        // KPI cá nhân: chỉ nhân viên + phó trưởng (không lấy trưởng đơn vị)
                        var staffAndPhoTruong = allKpiRoles
                            .Where(r =>
                                r.Role != "TRUONG_DON_VI_CAP_2" &&
                                r.Role != "TRUONG_DON_VI_CAP_3" &&
                                r.Role != "HIEU_TRUONG" &&
                                r.Role != "CHU_TICH_HOI_DONG_TRUONG"
                            )
                            .Select(r => r.IdNhanSu)
                            .Distinct()
                            .ToList();

                        allowedUserIds.UnionWith(staffAndPhoTruong);
                        break;

                    default:
                        // Role khác mặc định chỉ thấy mình
                        allowedUserIds.Add(currentUserId);
                        break;
                }
            }

            return allowedUserIds.ToList();
        }


    }
}
