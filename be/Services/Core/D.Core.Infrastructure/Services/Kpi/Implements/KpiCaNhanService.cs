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
using D.Notification.ApplicationService.Abstracts;
using D.Notification.Domain.Enums;
using D.Notification.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiCaNhanService : ServiceBase, IKpiCaNhanService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IKpiLogStatusService _logKpiService;
        private readonly INotificationService _notificationService;

        public KpiCaNhanService(
            ILogger<KpiCaNhanService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IKpiLogStatusService logKpiService,
            INotificationService notificationService
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _contextAccessor = contextAccessor;
            _logKpiService = logKpiService;
            _notificationService = notificationService;

        }

        public void CreateKpiCaNhan(CreateKpiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiCaNhan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );
            var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.IdNhanSu);
            if (nhanSu == null)
                throw new Exception("Không tìm thấy nhân sự");

            var entity = _mapper.Map<KpiCaNhan>(dto);
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
            _notificationService.SendAsync(new NotificationMessage
            {
                Receiver = new Receiver
                {
                    UserId = entity.IdNhanSu
                },
                Title = "Bạn có KPI mới",
                Content = $"Cấp trên đã tạo một KPI cá nhân mới cho bạn: {entity.KPI}",
                AltContent = $"KPI: {entity.KPI} - Năm học: {entity.NamHoc} vừa được thiết lập.",
                Channel = NotificationChannel.Realtime
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
            _logger.LogInformation($"{nameof(GetAllKpiCaNhan)} method called. Dto: {JsonSerializer.Serialize(dto)}");
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var isActive = GetKpiIsActive();
            var allowedUserIds = await GetAllowedUserIds(userId);

            List<int> nhanSuIdsByDonVi = new();
            if (dto.IdPhongBan.HasValue)
            {
                nhanSuIdsByDonVi = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .Where(x => x.IdDonVi == dto.IdPhongBan.Value && x.IdNhanSu != userId)
                    .Select(x => x.IdNhanSu).Distinct().ToListAsync();
            }
            var baseQuery = _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                .Where(kpi => !kpi.Deleted && allowedUserIds.Contains(kpi.IdNhanSu) && kpi.IdNhanSu != userId &&
                    (!dto.IdPhongBan.HasValue || nhanSuIdsByDonVi.Contains(kpi.IdNhanSu)) &&
                    (string.IsNullOrEmpty(dto.Keyword) || kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim())) &&
                    (!dto.LoaiKpi.HasValue || kpi.LoaiKPI == dto.LoaiKpi) &&
                    (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc) &&
                    (!dto.TrangThai.HasValue || kpi.Status == dto.TrangThai) &&
                    (!dto.IdNhanSu.HasValue || kpi.IdNhanSu == dto.IdNhanSu) && 
                    kpi.Role != "TRUONG_DON_VI_CAP_2"
                );
            var total = await baseQuery.CountAsync();
            var kpis = await baseQuery.Skip(dto.SkipCount()).Take(dto.PageSize).ToListAsync();

            var nhanSuIds = kpis.Select(x => x.IdNhanSu).Distinct().ToList();
            var summaryData = await baseQuery
                .Select(x => new { x.DiemKpi, x.DiemKpiCapTren, x.LoaiKPI })
                .ToListAsync();

            var summary = new KpiCaNhanSummaryDto
            {
                TongTuDanhGia = summaryData.Sum(x => x.DiemKpi ?? 0),
                TongCapTren = summaryData.Sum(x => x.DiemKpiCapTren ?? 0),
                ByLoaiKpi = summaryData.Where(x => x.LoaiKPI.HasValue)
                    .GroupBy(x => x.LoaiKPI!.Value)
                    .Select(g => new KpiCaNhanSummaryByLoaiDto
                    {
                        LoaiKpi = g.Key,
                        TuDanhGia = g.Sum(x => x.DiemKpi ?? 0),
                        CapTren = g.Sum(x => x.DiemKpiCapTren ?? 0)
                    }).ToList()
            };

            var roleDonViMap = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => nhanSuIds.Contains(r.IdNhanSu))
                .Select(r => new { r.IdNhanSu, r.Role, r.IdDonVi })
                .ToListAsync();

            var phongBans = await _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);

            var nhanSus = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .Where(ns => nhanSuIds.Contains(ns.Id))
                .ToDictionaryAsync(x => x.Id, x => (x.HoDem + " " + x.Ten).Trim());

            var resultItems = kpis.Select(kpi => {
                var idDonVi = roleDonViMap.FirstOrDefault(m => m.IdNhanSu == kpi.IdNhanSu && m.Role == kpi.Role)?.IdDonVi;
                return new KpiCaNhanDto
                {
                    Id = kpi.Id,
                    KPI = kpi.KPI,
                    LoaiKpi = kpi.LoaiKPI,
                    IdNhanSu = kpi.IdNhanSu,
                    NhanSu = nhanSus.ContainsKey(kpi.IdNhanSu) ? nhanSus[kpi.IdNhanSu] : "",
                    IdPhongBan = idDonVi,
                    PhongBan = (idDonVi.HasValue && phongBans.ContainsKey(idDonVi.Value)) ? phongBans[idDonVi.Value] : "",
                    IdCongThuc = kpi.IdCongThuc,
                    CongThuc = kpi.CongThucTinh,
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
                    IsActive = kpi.Status == KpiStatus.Evaluated ?  isActive : 0
                };
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
            _logger.LogInformation($"{nameof(FindPagingKpiCaNhanKeKhai)} => dto = {JsonSerializer.Serialize(dto)}");

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var isActive = GetKpiIsActive();
            var user = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == userId);

            var userRoles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdNhanSu == userId)
                .Select(r => new { r.Role, r.IdDonVi, r.TiLe })
                .ToListAsync();

            await EnsureKpiCaNhanKyLuatExist(userId,  null);
            var listDonViIds = userRoles
                .Where(x => x.IdDonVi.HasValue)
                .Select(x => x.IdDonVi.Value)
                .Distinct()
                .ToList();
            foreach (var donViId in listDonViIds)
            {
                await EnsureKpiCaNhanKyLuatExist(userId, donViId);
            }

            var phongBans = await _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);
            var queryBase = _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                .Where(kpi => !kpi.Deleted && kpi.IdNhanSu == userId
                    && (string.IsNullOrEmpty(dto.Keyword) || kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim()))
                    && (dto.LoaiKpi == null || kpi.LoaiKPI == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.Status == dto.TrangThai)
                    && (string.IsNullOrEmpty(dto.Role) || kpi.Role == dto.Role)
                    && kpi.Role != "HIEU_TRUONG" && kpi.Role != "TRUONG_DON_VI_CAP_2"
                );

            var totalCount = await queryBase.CountAsync();
            var pagedItemsRaw = await queryBase
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToListAsync();

            var summaryData = await queryBase
                .Select(x => new { x.DiemKpi, x.DiemKpiCapTren, x.LoaiKPI, x.Role })
                .ToListAsync();

            decimal finalTuDanhGia = 0; 
            decimal finalCapTren = 0;
            var scoreByRoleMap = summaryData
                .Where(x => x.Role != null)
                .GroupBy(x => x.Role)
                .ToDictionary(
                    g => g.Key!,
                    g => new
                    {
                        RawTuCham = g.Sum(x => x.LoaiKPI == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0)),
                        RawCapTren = g.Sum(x => x.LoaiKPI == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0))
                    }
                );

            foreach (var roleConfig in userRoles)
            {
                if (!string.IsNullOrEmpty(roleConfig.Role) && scoreByRoleMap.TryGetValue(roleConfig.Role, out var rawScores))
                {
                    var tiLe = roleConfig.TiLe ?? 0;
                    finalTuDanhGia += rawScores.RawTuCham * (tiLe / 100m);
                    finalCapTren += rawScores.RawCapTren * (tiLe / 100m);
                }
            }
            var summaryDto = new KpiCaNhanSummaryDto
            {
                TongTuDanhGia = finalTuDanhGia,
                TongCapTren = finalCapTren    
            };

            var resultItems = pagedItemsRaw.Select(kpi => {
                var idDonVi = userRoles.FirstOrDefault(r => r.Role == kpi.Role)?.IdDonVi;

                return new KpiCaNhanDto
                {
                    Id = kpi.Id,
                    KPI = kpi.KPI,
                    LoaiKpi = kpi.LoaiKPI,
                    IdNhanSu = kpi.IdNhanSu,
                    NhanSu = user != null ? $"{user.HoDem} {user.Ten}".Trim() : "",
                    IdPhongBan = idDonVi,
                    PhongBan = (idDonVi.HasValue && phongBans.ContainsKey(idDonVi.Value)) ? phongBans[idDonVi.Value] : "",
                    IdCongThuc = kpi.IdCongThuc,
                    CongThuc = kpi.CongThucTinh,
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
                    IsActive = (kpi.Status == KpiStatus.Assigned || kpi.Status == KpiStatus.Edited || kpi.Status == KpiStatus.Declared || kpi.Status == KpiStatus.Create) ? isActive : 0
                };
            }).ToList();

            return new PageResultDto<KpiCaNhanDto>
            {
                Items = resultItems,
                TotalItem = totalCount,
                Summary = summaryDto
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

                        if (kpi.LoaiKPI == 3)
                        {
                            kpi.DiemKpi = TinhDiemKPI.GetPhanTramTruTuanThu(kpi.KPI, kpi.KetQuaThucTe.Value);
                        }
                        else
                        {
                            kpi.DiemKpi = TinhDiemKPI.TinhDiemChung(
                                kpi.KetQuaThucTe,
                                kpi.MucTieu,
                                kpi.TrongSo,
                                kpi.IdCongThuc,
                                kpi.LoaiKetQua
                            );
                        }
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

        public async Task UpdateTrangThaiKpiCaNhan(UpdateTrangThaiKpiDto dto)
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
                await SendKpiStatusNotification(kpi, dto.TrangThai, dto.Note);
            }

            await _unitOfWork.iKpiCaNhanRepository.SaveChangeAsync();
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
                        if (kpi.LoaiKPI == 3)
                        {
                            kpi.DiemKpiCapTren = TinhDiemKPI.GetPhanTramTruTuanThu(kpi.KPI, kpi.CapTrenDanhGia.Value);
                        }
                        else
                        {
                            kpi.DiemKpiCapTren = TinhDiemKPI.TinhDiemChung(
                            kpi.CapTrenDanhGia,
                            kpi.MucTieu,
                            kpi.TrongSo,
                            kpi.IdCongThuc,
                            kpi.LoaiKetQua
                        );
                        }
                        _unitOfWork.iKpiCaNhanRepository.Update(kpi);
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.Status,
                            Description = $"Cập nhật kết quả cấp trên, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpiCapTren}",
                            CapKpi = 1
                        });
                        _notificationService.SendAsync(new NotificationMessage
                        {
                            Receiver = new Receiver
                            {
                                UserId = kpi.IdNhanSu
                            },
                            Title = "KPI đã được đánh giá",
                            Content = "Cấp trên đã hoàn thành đánh giá KPI của bạn.",
                            AltContent = $"KPI {kpi.KPI} đạt điểm: {kpi.DiemKpiCapTren}",
                            Channel = NotificationChannel.Realtime
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
                .FirstOrDefault(); 

            return int.TryParse(sysVarValue, out var parsedValue) ? parsedValue : (int?)null;
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

                    case "HIEU_TRUONG":
                    case "CHU_TICH_HOI_DONG_TRUONG":

                        var allStaff = allKpiRoles
                            .Where(r =>
                                r.Role != "HIEU_TRUONG" &&
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

        public async Task<object> GetKpiCaNhanContextForAi(int userId, List<int> allowedUserIds)
        {
            var kpis = await _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                .Where(x => !x.Deleted && allowedUserIds.Contains(x.IdNhanSu))
                .ToListAsync();

            if (!kpis.Any()) return null;
            var nhanSuIds = kpis.Select(x => x.IdNhanSu).Distinct().ToList();
            var nhanSuDict = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .Where(ns => nhanSuIds.Contains(ns.Id))
                .ToDictionaryAsync(x => x.Id, x => (x.HoDem + " " + x.Ten).Trim());
            var result = kpis.GroupBy(x => x.IdNhanSu).Select(group =>
            {
                var tongTuCham = group.Sum(s => (s.LoaiKPI == 3 ? -1 : 1) * (s.DiemKpi ?? 0));
                var tongCapTren = group.Sum(s => (s.LoaiKPI == 3 ? -1 : 1) * (s.DiemKpiCapTren ?? 0));

                return new
                {
                    IdNhanSu = group.Key,
                    HoTen = nhanSuDict.GetValueOrDefault(group.Key, "UnKnown"),
                    TongDiemTuCham = tongTuCham,
                    TongDiemCapTren = tongCapTren,
                    ChiTietKPI = group.Select(k => {
                        double.TryParse(k.TrongSo?.Replace("%", "").Trim(), out double ts);
                        int modifier = k.LoaiKPI == 3 ? -1 : 1;

                        return new
                        {
                            TenKPI = k.KPI,
                            LoaiKPI = k.LoaiKPI switch
                            {
                                1 => "Chức năng",
                                2 => "Mục tiêu",
                                3 => "Tuân thủ",
                                _ => "Khác"
                            },
                            MucTieu = k.MucTieu ?? "Chưa xác định",
                            TrongSo = ts,
                            KetQuaThucTe = k.KetQuaThucTe ?? 0,
                            DiemTuCham = (k.DiemKpi ?? 0) * modifier,
                            DiemCapTren = (k.CapTrenDanhGia ?? k.DiemKpiCapTren ?? 0) * modifier,
                            CongThuc = k.CongThucTinh ?? "Không có công thức",
                            ChucVu = k.Role ?? "N/A"
                        };
                    }).ToList()
                };
            }).ToList();

            return result;
        }

        private async Task SendKpiStatusNotification(KpiCaNhan kpi, int newStatus, string? note)
        {
            if (newStatus == KpiStatus.Evaluated)
            {
                if (kpi.Role == "PHO_HIEU_TRUONG")
                {
                    return;
                }
                var donViRole = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .FirstOrDefault(r => r.IdNhanSu == kpi.IdNhanSu && r.Role == kpi.Role );

                if (donViRole?.IdDonVi != null)
                {
                    var truongDonViId = _unitOfWork.iKpiRoleRepository.TableNoTracking
                        .Where(r => r.IdDonVi == donViRole.IdDonVi && r.Role == "TRUONG_DON_VI_CAP_2")
                        .Select(r => r.IdNhanSu)
                        .FirstOrDefault();

                    if (truongDonViId != 0)
                    {
                        var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(u => u.Id == kpi.IdNhanSu);
                        var tenNhanSu = nhanSu != null ? $"{nhanSu.HoDem} {nhanSu.Ten}" : "Nhân sự";

                        await _notificationService.SendAsync(new NotificationMessage
                        {
                            Receiver = new Receiver { UserId = truongDonViId },
                            Title = "KPI gửi chấm từ nhân sự",
                            Content = $"Nhân sự {tenNhanSu} đã gửi chấm KPI.",
                            Channel = NotificationChannel.Realtime
                        });
                    }
                }
            }
            else if (newStatus == KpiStatus.Scored || newStatus == KpiStatus.PrincipalApprove)
            {
                string title = newStatus == KpiStatus.Evaluated ? "KPI đã được đánh giá" : "Cấp trên phê duyệt";
                string content = newStatus == KpiStatus.Evaluated
                    ? $"KPI '{kpi.KPI}' của bạn đã được trưởng đơn vị chấm. Đánh giá: {note ?? "Không đánh giá thêm"}."
                    : $"KPI '{kpi.KPI}' của bạn đã được Hiệu trưởng phê duyệt.";

                await _notificationService.SendAsync(new NotificationMessage
                {
                    Receiver = new Receiver { UserId = kpi.IdNhanSu },
                    Title = title,
                    Content = content,
                    Channel = NotificationChannel.Realtime
                });
            }
        }
        private async Task EnsureKpiCaNhanKyLuatExist(int userId,  int? idDonVi)
        {
            var userRoles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(x => x.IdNhanSu == userId).ToListAsync();
            if (userRoles.Any(r => r.Role == "CHU_TICH_HOI_DONG_TRUONG")) return;
            if (userRoles.Any(r => r.Role == "HIEU_TRUONG")) return;
            if (userRoles.Any(r => r.Role == "TRUONG_DON_VI_CAP_2")) return;
            if (userRoles.Any(r => r.Role == "PHO_HIEU_TRUONG"))
            {
                var hasVpData = await _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                    .AnyAsync(x => x.IdNhanSu == userId
                                && x.NamHoc == "2026"
                                && x.LoaiKPI == 3
                                && x.Role == "PHO_HIEU_TRUONG"
                                && !x.Deleted);
                if (!hasVpData)
                {
                    await CreateKpiData(userId, "PHO_HIEU_TRUONG", null);
                }
                return;
            }
            if (idDonVi.HasValue)
            {
                var roleAtUnit = userRoles.FirstOrDefault(x => x.IdDonVi == idDonVi.Value);
                if (roleAtUnit == null) return;

                var targetRole = roleAtUnit.Role;

                var hasUnitData = await _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                    .AnyAsync(x => x.IdNhanSu == userId
                                && x.LoaiKPI == 3
                                && x.Role == targetRole
                                && !x.Deleted);

                if (!hasUnitData)
                {
                    await CreateKpiData(userId, targetRole, idDonVi.Value);
                }
            }
        }

        private async Task CreateKpiData(int userId, string role, int? idDonVi)
        {
            var kpis = new List<KpiCaNhan>
            {
                CreateTemplate(userId, "2026", role, idDonVi, "Vi phạm về thời gian làm việc"),
                CreateTemplate(userId, "2026", role, idDonVi, "Vi phạm về quy định chấm công"),
                CreateTemplate(userId, "2026", role, idDonVi, "Vi phạm tuân thủ về trật tự và tác phong làm việc"),
                CreateTemplate(userId, "2026", role, idDonVi, "Vi phạm quy tắc ứng xử (với sinh viên/đồng nghiệp/khách)"),
                CreateTemplate(userId, "2026", role, idDonVi, "Vi phạm quy định sử dụng đồng phục,thẻ, nhân viên"),
                CreateTemplate(userId, "2026", role, idDonVi, "Vi phạm quy trình nghiệp vụ")
            };

            await _unitOfWork.iKpiCaNhanRepository.AddRangeAsync(kpis);
            await _unitOfWork.iKpiCaNhanRepository.SaveChangeAsync();
        }

        private KpiCaNhan CreateTemplate(int userId, string namHoc, string role, int? idDonVi, string name)
        {
            return new KpiCaNhan
            {
                IdNhanSu = userId,
                NamHoc = namHoc,
                Role = role,
                LoaiKPI = 3,
                KPI = name,
                MucTieu = "0%",
                CongThucTinh = "Xem phụ lục",
                LoaiKetQua = "NUMBER",
                Status = KpiStatus.Assigned,
                CreatedDate = DateTime.Now,
                Deleted = false
            };
        }
    }
}