using AutoMapper;
using d.Shared.Permission.Error;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
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
using System.Globalization;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiCaNhanService : ServiceBase, IKpiCaNhanService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IKpiLogStatusService _logKpiService;

        public KpiCaNhanService(
            ILogger<KpiRoleService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IKpiLogStatusService logKpiService
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _logKpiService = logKpiService;
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
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = entity.Id,
                OldStatus = 0,
                NewStatus = entity.Status,
                Description = "Tạo KPI cá nhân mới",
                CapKpi = 1
            });
        }

        public void DeleteKpiCaNhan(DeleteKpiCaNhanDto dto)
        {
            _logger.LogInformation($"{nameof(DeleteKpiCaNhan)} method called.");
            var kpi = _unitOfWork.iKpiCaNhanRepository.Table.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new Exception($"KPI cá nhân với Id = {dto.Id} không tồn tại hoặc đã bị xóa.");
            }
            var oldStatus = kpi.Status;
            kpi.Deleted = true;

            _unitOfWork.iKpiCaNhanRepository.Update(kpi);
            _unitOfWork.iKpiCaNhanRepository.SaveChange();
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = kpi.Id,
                OldStatus = oldStatus,
                NewStatus = null,
                Description = "Xóa KPI cá nhân",
                CapKpi = 1
            });
        }

        public async Task<PageResultDto<KpiCaNhanDto>> GetAllKpiCaNhan(FilterKpiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllKpiCaNhan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var isActive = GetKpiIsActive();

            var allowedUserIds = await GetAllowedUserIds(userId);

            List<int> nhanSuIdsByDonVi = new();

            if (dto.IdPhongBan.HasValue)
            {
                nhanSuIdsByDonVi = await _unitOfWork.iKpiRoleRepository
                    .TableNoTracking
                    .Where(x =>
                        x.IdDonVi == dto.IdPhongBan.Value &&
                        x.IdNhanSu != userId 
                    )
                    .Select(x => x.IdNhanSu)
                    .Distinct()
                    .ToListAsync();
            }

            var kpiQuery = _unitOfWork.iKpiCaNhanRepository
                .TableNoTracking
                .Where(kpi =>
                    !kpi.Deleted &&
                    allowedUserIds.Contains(kpi.IdNhanSu) &&     
                    kpi.IdNhanSu != userId &&                 
                    (!dto.IdPhongBan.HasValue ||
                        nhanSuIdsByDonVi.Contains(kpi.IdNhanSu)) &&
                    (string.IsNullOrEmpty(dto.Keyword) ||
                        kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim())) &&
                    (!dto.LoaiKpi.HasValue || kpi.LoaiKPI == dto.LoaiKpi) &&
                    (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc) &&
                    (!dto.TrangThai.HasValue || kpi.Status == dto.TrangThai) &&
                    (!dto.IdNhanSu.HasValue || kpi.IdNhanSu == dto.IdNhanSu) &&
                    kpi.Role != "TRUONG_DON_VI_CAP_2"
                );

            var total = await kpiQuery.CountAsync();

            var kpis = await kpiQuery
                .OrderBy(x => x.STT)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToListAsync();

            var nhanSuIds = kpis.Select(x => x.IdNhanSu).Distinct().ToList();

            var phongBans = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);

            var nhanSus = await _unitOfWork.iNsNhanSuRepository
                .TableNoTracking
                .Where(ns => nhanSuIds.Contains(ns.Id))
                .ToDictionaryAsync(
                    x => x.Id,
                    x => new
                    {
                        HoTenDayDu = (x.HoDem + " " + x.Ten).Trim(),
                        TenPhongBan = phongBans.Values.FirstOrDefault() ?? string.Empty
                    }
                );

            var summaryQuery = _unitOfWork.iKpiCaNhanRepository
                .TableNoTracking
                .Where(kpi =>
                    !kpi.Deleted &&
                    allowedUserIds.Contains(kpi.IdNhanSu) &&
                    kpi.IdNhanSu != userId &&
                    (!dto.IdPhongBan.HasValue || nhanSuIdsByDonVi.Contains(kpi.IdNhanSu)) &&
                    (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                );
            var summary = new KpiCaNhanSummaryDto
            {
                TongTuDanhGia = await summaryQuery.SumAsync(x => (decimal?)(x.DiemKpi ?? 0)) ?? 0,
                TongCapTren = await summaryQuery.SumAsync(x => (decimal?)(x.DiemKpiCapTren ?? 0)) ?? 0,
                ByLoaiKpi = await summaryQuery
                    .Where(x => x.LoaiKPI.HasValue)
                    .GroupBy(x => x.LoaiKPI!.Value)
                    .Select(g => new KpiCaNhanSummaryByLoaiDto
                    {
                        LoaiKpi = g.Key,
                        TuDanhGia = g.Sum(x => (decimal?)(x.DiemKpi ?? 0)) ?? 0,
                        CapTren = g.Sum(x => (decimal?)(x.DiemKpiCapTren ?? 0)) ?? 0
                    })
                    .ToListAsync()

            };

            var resultItems = kpis.Select(kpi => new KpiCaNhanDto
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
                IsActive = kpi.Status == KpiStatus.Evaluated ? 0 : isActive,
                CongThuc = kpi.CongThucTinh
            }).ToList();

            return new PageResultDto<KpiCaNhanDto>
            {
                Items = resultItems,
                TotalItem = total,
                Summary = summary
            };
        }




        public async Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhanKeKhai(FilterKpiKeKhaiCaNhanDto dto)
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
                    && (kpi.Role != "HIEU_TRUONG")
                     && (kpi.Role != "TRUONG_DON_VI_CAP_2")
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
                            kpi.Status == KpiStatus.Create
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
                        var oldStatus = kpi.Status;
                        var oldScore = kpi.DiemKpi;

                        kpi.KetQuaThucTe = item.KetQuaThucTe;
                        kpi.DiemKpi = item.DiemKpi;

                        kpi.DiemKpi = TinhDiemKPI.TinhDiemChung(
                            kpi.KetQuaThucTe,
                            kpi.MucTieu,
                            kpi.TrongSo,
                            kpi.IdCongThuc,
                            kpi.LoaiKetQua
                         );
                        kpi.Status = KpiStatus.Declared;

                        _unitOfWork.iKpiCaNhanRepository.Update(kpi);
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.Status,
                            Description = $"Cập nhật kết quả thực tế, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpi}",
                            CapKpi = 1
                        });
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
            var kpi = _unitOfWork.iKpiCaNhanRepository.Table.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.CodeNotFound, $"Không tìm thấy KPI cá nhân với Id={dto.Id}");
            }

            var oldStatus = kpi.Status;

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
            kpi.CongThucTinh = dto.CongThucTinh;
            kpi.IdCongThuc = dto.IdCongThuc;
            kpi.LoaiKetQua = dto.LoaiKetQua;
            kpi.Role = dto.Role;
            kpi.Status = KpiStatus.Edited;

            _unitOfWork.iKpiCaNhanRepository.Update(kpi);
            _unitOfWork.iKpiCaNhanRepository.SaveChange();

            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = kpi.Id,
                OldStatus = oldStatus,
                NewStatus = kpi.Status,
                Description = "Cập nhật KPI cá nhân",
                CapKpi = 1
            });
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
                var oldStatus = kpi.Status;

                kpi.Status = dto.TrangThai;
                _unitOfWork.iKpiCaNhanRepository.Update(kpi);
                _logKpiService.InsertLog(new InsertKpiLogStatusDto
                {
                    KpiId = kpi.Id,
                    OldStatus = oldStatus,
                    NewStatus = kpi.Status,
                    Description = dto.Note ?? "Thay đổi trạng thái KPI",
                    CapKpi = 1,
                    Reason = dto.Note
                });
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
                        var oldStatus = kpi.Status;
                        var oldScore = kpi.DiemKpiCapTren;
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
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.Status,
                            Description = $"Cập nhật kết quả cấp trên, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpiCapTren}",
                            CapKpi = 1
                        });
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

            // Nếu không có role quản lý → KHÔNG xem được gì
            if (!userRoles.Any(r =>
                r.Role == "TRUONG_DON_VI_CAP_2" ||
                r.Role == "TRUONG_DON_VI_CAP_3" ||
                r.Role == "HIEU_TRUONG" ||
                r.Role == "CHU_TICH_HOI_DONG_TRUONG"))
            {
                return new List<int>();
            }

            var allKpiRoles = await _unitOfWork.iKpiRoleRepository
                .TableNoTracking
                .ToListAsync();

            var allowedUserIds = new HashSet<int>();

            foreach (var role in userRoles)
            {
                switch (role.Role)
                {
                    // ===== TRƯỞNG ĐƠN VỊ =====
                    case "TRUONG_DON_VI_CAP_2":
                    case "TRUONG_DON_VI_CAP_3":

                        if (!role.IdDonVi.HasValue)
                            break;

                        var subordinates = allKpiRoles
                            .Where(r =>
                                r.IdDonVi == role.IdDonVi &&
                                r.Role != "TRUONG_DON_VI_CAP_2" &&
                                r.Role != "TRUONG_DON_VI_CAP_3" &&
                                r.Role != "HIEU_TRUONG" &&
                                r.Role != "PHO_HIEU_TRUONG" &&
                                r.Role != "CHU_TICH_HOI_DONG_TRUONG"
                            )
                            .Select(r => r.IdNhanSu)
                            .Distinct();

                        allowedUserIds.UnionWith(subordinates);
                        break;

                    // ===== HIỆU TRƯỞNG / CHỦ TỊCH =====
                    case "HIEU_TRUONG":
                    case "CHU_TICH_HOI_DONG_TRUONG":

                        var allStaff = allKpiRoles
                            .Where(r =>
                                r.Role != "HIEU_TRUONG" &&
                                r.Role != "PHO_HIEU_TRUONG" &&
                                r.Role != "CHU_TICH_HOI_DONG_TRUONG"
                            )
                            .Select(r => r.IdNhanSu)
                            .Distinct();

                        allowedUserIds.UnionWith(allStaff);
                        break;
                }
            }

            return allowedUserIds.ToList();
        }



    }
}
