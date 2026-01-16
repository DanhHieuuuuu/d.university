using AutoMapper;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiDonViService : ServiceBase, IKpiDonViService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IKpiLogStatusService _logKpiService;
        private readonly INotificationService _notificationService;

        public KpiDonViService(
            ILogger<KpiDonViService> logger,
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
            _unitOfWork.iKpiDonViRepository.Add(entity);
            _unitOfWork.iKpiDonViRepository.SaveChange();
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = entity.Id,
                OldStatus = 0,
                NewStatus = entity.TrangThai,
                Description = "Tạo KPI đơn vị mới",
                CapKpi = 2
            });
        }

        public void DeleteKpiDonVi(DeleteKpiDonViDto dto)
        {
            _logger.LogInformation($"{nameof(DeleteKpiDonVi)} method called.");
            var kpi = _unitOfWork.iKpiDonViRepository.Table.FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
            {
                throw new Exception($"KPI đơn vị với Id = {dto.Id} không tồn tại hoặc đã bị xóa.");
            }
            var oldStatus = kpi.TrangThai;
            kpi.Deleted = true;

            _unitOfWork.iKpiDonViRepository.Update(kpi);
            _unitOfWork.iKpiDonViRepository.SaveChange();
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = kpi.Id,
                OldStatus = oldStatus,
                NewStatus = null,
                Description = "Xóa KPI đơn vị",
                CapKpi = 2
            });
        }

        // 1. Hàm Kê khai cho Trưởng đơn vị
        public PageResultDto<KpiDonViDto> FindPagingKeKhai(FilterKpiDonViKeKhaiDto dto)
        {
            _logger.LogInformation($"{nameof(FindPagingKeKhai)} => dto = {JsonSerializer.Serialize(dto)}");

            var isActive = GetKpiIsActive();
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var donViIdsQuanLy = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(x => x.IdNhanSu == userId && x.Role == "TRUONG_DON_VI_CAP_2")
                .Select(x => x.IdDonVi)
                .Distinct()
                .ToList();

            if (!donViIdsQuanLy.Any())
            {
                return new PageResultDto<KpiDonViDto> { Items = new List<KpiDonViDto>(), TotalItem = 0 };
            }

            var donvis = _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToDictionary(x => x.Id, x => x.TenPhongBan);
            var queryBase = _unitOfWork.iKpiDonViRepository.TableNoTracking
                .Where(kpi => !kpi.Deleted
                    && kpi.IdDonVi.HasValue && donViIdsQuanLy.Contains(kpi.IdDonVi.Value)
                    && (string.IsNullOrEmpty(dto.Keyword) || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower().Trim()))
                    && (dto.IdDonVi == null || kpi.IdDonVi == dto.IdDonVi)
                    && (dto.LoaiKpi == null || kpi.LoaiKpi == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.TrangThai == dto.TrangThai)
                );

            var totalCount = queryBase.Count();
            var pagedItemsRaw = queryBase
                .OrderBy(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();
            var summaryData = queryBase
                .Select(x => new { x.DiemKpi, x.DiemKpiCapTren, x.LoaiKpi })
                .ToList();
            decimal tongTuDanhGia = summaryData.Sum(x =>
                x.LoaiKpi == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0)
            );

            decimal tongCapTren = summaryData.Sum(x =>
                x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0)
            );

            var summaryDto = new KpiDonViSummaryDto
            {
                TongTuDanhGia = tongTuDanhGia,
                TongCapTren = tongCapTren,
                ByLoaiKpi = summaryData
                    .Where(x => x.LoaiKpi.HasValue)
                    .GroupBy(x => x.LoaiKpi!.Value)
                    .Select(g => new KpiDonViSummaryByLoaiDto
                    {
                        LoaiKpi = g.Key,
                        TuDanhGia = g.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0)),
                        CapTren = g.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0))
                    }).ToList()
            };
            var resultItems = pagedItemsRaw.Select(kpi => new KpiDonViDto
            {
                Id = kpi.Id,
                Kpi = kpi.Kpi,
                MucTieu = kpi.MucTieu,
                TrongSo = kpi.TrongSo,
                IdDonVi = kpi.IdDonVi,
                DonVi = (kpi.IdDonVi.HasValue && donvis.ContainsKey(kpi.IdDonVi.Value)) ? donvis[kpi.IdDonVi.Value] : string.Empty,
                LoaiKpi = kpi.LoaiKpi,
                CongThuc = kpi.CongThucTinh,
                IdCongThuc = kpi.IdCongThuc,
                NamHoc = kpi.NamHoc,
                TrangThai = kpi.TrangThai,
                KetQuaThucTe = kpi.KetQuaThucTe,
                CapTrenDanhGia = kpi.CapTrenDanhGia,
                DiemKpiCapTren = kpi.DiemKpiCapTren,
                DiemKpi = kpi.DiemKpi,
                LoaiKetQua = kpi.LoaiKetQua,
                GhiChu = kpi.GhiChu,
                IsActive = (kpi.TrangThai == KpiStatus.Assigned || kpi.TrangThai == KpiStatus.NeedEdit || kpi.TrangThai == KpiStatus.Declared) ? isActive : 0
            }).ToList();

            return new PageResultDto<KpiDonViDto>
            {
                Items = resultItems,
                TotalItem = totalCount,
                Summary = summaryDto
            };
        }
        public PageResultDto<KpiDonViDto> GetAllKpiDonVi(FilterKpiDonViDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllKpiDonVi)} => dto = {JsonSerializer.Serialize(dto)}");

            var isActive = GetKpiIsActive();
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var userRoles = _unitOfWork.iKpiRoleRepository.TableNoTracking.Where(x => x.IdNhanSu == userId).ToList();
            List<int>? allowedDonViIds = null;

            if (userRoles.Any(r => r.Role == "HIEU_TRUONG")) allowedDonViIds = null;
            else if (userRoles.Any(r => r.Role == "PHO_HIEU_TRUONG"))
                allowedDonViIds = userRoles.Where(r => r.Role == "PHO_HIEU_TRUONG" && r.IdDonVi.HasValue).Select(r => r.IdDonVi!.Value).Distinct().ToList();
            else return new PageResultDto<KpiDonViDto> { Items = new List<KpiDonViDto>(), TotalItem = 0 };

            var donvis = _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToDictionary(x => x.Id, x => x.TenPhongBan);

            var queryBase = _unitOfWork.iKpiDonViRepository.TableNoTracking
                .Where(kpi => !kpi.Deleted && kpi.IdDonVi.HasValue
                    && (allowedDonViIds == null || allowedDonViIds.Contains(kpi.IdDonVi.Value))
                    && (string.IsNullOrEmpty(dto.Keyword) || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower()))
                    && (dto.IdDonVi == null || kpi.IdDonVi == dto.IdDonVi)
                    && (dto.LoaiKpi == null || kpi.LoaiKpi == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.TrangThai == dto.TrangThai)
                );

            var totalCount = queryBase.Count();
            var pagedItemsRaw = queryBase.OrderBy(x => x.Id).Skip(dto.SkipCount()).Take(dto.PageSize).ToList();
            var summaryData = queryBase.Select(x => new { x.DiemKpi, x.DiemKpiCapTren, x.LoaiKpi }).ToList();

            decimal tongTuDanhGia = summaryData.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0));
            decimal tongCapTren = summaryData.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0));

            var summaryDto = new KpiDonViSummaryDto
            {
                TongTuDanhGia = tongTuDanhGia,
                TongCapTren = tongCapTren,
                ByLoaiKpi = summaryData.Where(x => x.LoaiKpi.HasValue).GroupBy(x => x.LoaiKpi!.Value)
                    .Select(g => new KpiDonViSummaryByLoaiDto
                    {
                        LoaiKpi = g.Key,
                        TuDanhGia = g.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0)),
                        CapTren = g.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0))
                    }).ToList()
            };
            var resultItems = pagedItemsRaw.Select(kpi => new KpiDonViDto
            {
                Id = kpi.Id,
                Kpi = kpi.Kpi,
                MucTieu = kpi.MucTieu,
                TrongSo = kpi.TrongSo,
                IdDonVi = kpi.IdDonVi,
                DonVi = (kpi.IdDonVi.HasValue && donvis.ContainsKey(kpi.IdDonVi.Value)) ? donvis[kpi.IdDonVi.Value] : string.Empty,
                LoaiKpi = kpi.LoaiKpi,
                CongThuc = kpi.CongThucTinh,
                NamHoc = kpi.NamHoc,
                TrangThai = kpi.TrangThai,
                KetQuaThucTe = kpi.KetQuaThucTe,
                DiemKpi = kpi.DiemKpi,
                DiemKpiCapTren = kpi.DiemKpiCapTren,
                CapTrenDanhGia = kpi.CapTrenDanhGia,
                IdCongThuc = kpi.IdCongThuc,
                LoaiKetQua = kpi.LoaiKetQua,
                IsActive = kpi.TrangThai == KpiStatus.Evaluated ? 0 : isActive,
            }).ToList();

            return new PageResultDto<KpiDonViDto> { Items = resultItems, TotalItem = totalCount, Summary = summaryDto };
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
                _logKpiService.InsertLog(new InsertKpiLogStatusDto
                {
                    KpiId = item.Id,
                    OldStatus = item.Status,
                    NewStatus = 0,
                    Description = "Hủy giao KPI",
                    CapKpi = item.LoaiKPI
                });
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
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = existed.Id,
                            OldStatus = existed.Status,
                            NewStatus = existed.Status,
                            Description = $"Cập nhật trọng số KPI ({existed.TrongSo})",
                            CapKpi = 1
                        });
                    }
                    continue;
                }

                var kpiRole = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .FirstOrDefaultAsync(x =>
                        x.IdNhanSu == nsDto.IdNhanSu &&
                        x.IdDonVi == kpiDonVi.IdDonVi);

                if (kpiRole == null)
                    throw new Exception("Nhân sự không thuộc đơn vị của KPI");

                var newKpi = new KpiCaNhan
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
                };
                _unitOfWork.iKpiCaNhanRepository.Add(newKpi);
                _logKpiService.InsertLog(new InsertKpiLogStatusDto
                {
                    KpiId = newKpi.Id,
                    OldStatus = 0,
                    NewStatus = newKpi.Status,
                    Description = "Đã được giao KPI",
                    CapKpi = 1
                });
                _logKpiService.InsertLog(new InsertKpiLogStatusDto
                {
                    KpiId = kpiDonVi.Id,
                    OldStatus = 0,
                    NewStatus = 0,
                    Description = "Giao KPI đơn vị cho nhân sự",
                    CapKpi = 2
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
                        var oldStatus = kpi.TrangThai;
                        var oldScore = kpi.DiemKpi;
                        kpi.KetQuaThucTe = item.KetQuaThucTe;
                        kpi.DiemKpi = item.DiemKpi;
                        if (kpi.LoaiKpi == 3)
                        {
                            kpi.DiemKpi = TinhDiemKPI.GetPhanTramTruTuanThu(kpi.Kpi, kpi.KetQuaThucTe.Value);
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
                        kpi.TrangThai = KpiStatus.Declared;

                        _unitOfWork.iKpiDonViRepository.Update(kpi);
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.TrangThai,
                            Description = $"Cập nhật kết quả thực tế, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpi}",
                            CapKpi = 2
                        });
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
            var oldStatus = kpi.TrangThai;
            // Cập nhật thông tin
            kpi.Kpi = dto.Kpi;
            kpi.MucTieu = dto.MucTieu;
            kpi.TrongSo = dto.TrongSo;
            kpi.IdDonVi = dto.IdDonVi;
            kpi.LoaiKpi = dto.LoaiKpi;
            kpi.NamHoc = dto.NamHoc;
            kpi.IdKpiTruong = dto.IdKpiTruong;
            kpi.LoaiKetQua = dto.LoaiKetQua;
            kpi.IdCongThuc = dto.IdCongThuc;
            kpi.CongThucTinh = dto.CongThucTinh;
            kpi.TrangThai = KpiStatus.Edited;

            _unitOfWork.iKpiDonViRepository.Update(kpi);
            _unitOfWork.iKpiDonViRepository.SaveChange();
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = kpi.Id,
                OldStatus = oldStatus,
                NewStatus = kpi.TrangThai,
                Description = "Cập nhật KPI đơn vị",
                CapKpi = 2
            });
        }

        public async Task UpdateTrangThaiKpiDonVi(UpdateTrangThaiKpiDonViDto dto)
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
                var oldStatus = kpi.TrangThai;
                kpi.TrangThai = dto.TrangThai;
                _unitOfWork.iKpiDonViRepository.Update(kpi);
                _logKpiService.InsertLog(new InsertKpiLogStatusDto
                {
                    KpiId = kpi.Id,
                    OldStatus = oldStatus,
                    NewStatus = kpi.TrangThai,
                    Description = "Cập nhật trạng thái KPI đơn vị",
                    CapKpi = 2,
                    Reason = dto.Note
                });
                await SendKpiStatusNotification(kpi, dto.TrangThai, dto.Note);
            }

            await _unitOfWork.iKpiDonViRepository.SaveChangeAsync();
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
                        var oldStatus = kpi.TrangThai;
                        var oldScore = kpi.DiemKpi;
                        kpi.CapTrenDanhGia = item.KetQuaCapTren;
                        kpi.DiemKpiCapTren = item.DiemKpiCapTren;
                        if (kpi.LoaiKpi == 3)
                        {
                            kpi.DiemKpi = TinhDiemKPI.GetPhanTramTruTuanThu(kpi.Kpi, kpi.KetQuaThucTe.Value);
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
                        kpi.TrangThai = KpiStatus.Evaluated;

                        _unitOfWork.iKpiDonViRepository.Update(kpi);
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.TrangThai,
                            Description = $"Cập nhật kết quả cấp trên, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpi}",
                            CapKpi = 2
                        });
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

        public async Task<string> GetKpiDonViContextForAi(List<int>? allowedDonViIds)
        {
            var query = _unitOfWork.iKpiDonViRepository.TableNoTracking.Where(x => !x.Deleted);
            if (allowedDonViIds != null)
                query = query.Where(x => x.IdDonVi.HasValue && allowedDonViIds.Contains(x.IdDonVi.Value));

            var kpis = await query.ToListAsync();
            var donViDict = await _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);

            if (!kpis.Any()) return "### KPI Đơn vị: Không có dữ liệu.";

            var sb = new StringBuilder();
            sb.AppendLine("## [BÁO CÁO KPI CẤP ĐƠN VỊ]");

            var groupedByDonVi = kpis.GroupBy(x => x.IdDonVi);
            foreach (var group in groupedByDonVi)
            {
                var tenDV = donViDict.GetValueOrDefault(group.Key ?? 0, "Đơn vị ẩn danh");
                sb.AppendLine($"### Đơn vị: {tenDV}");
                sb.AppendLine($"- **Tổng điểm đơn vị tự chấm:** {group.Sum(s => s.DiemKpi ?? 0)}");
                sb.AppendLine("| KPI Đơn vị | Mục tiêu | Trọng số | Kết quả thực tế | Tự chấm | Công thức tính");
                sb.AppendLine("| :--- | :--- | :---: | :---: | :---: |:---:|");

                foreach (var k in group)
                {
                    double.TryParse(k.TrongSo, out double ts);
                    sb.AppendLine($"| {k.Kpi} | {k.MucTieu} | {ts}% | {k.KetQuaThucTe} | {k.DiemKpi} | {k.CongThucTinh}");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private async Task SendKpiStatusNotification(KpiDonVi kpi, int newStatus, string note)
        {
            if (newStatus == KpiStatus.Evaluated)
            {
                var donVi = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .FirstOrDefault(r => r.IdDonVi == kpi.IdDonVi);

                if (donVi?.IdDonVi != null)
                {
                    var truongDonViId = _unitOfWork.iKpiRoleRepository.TableNoTracking
                        .Where(r => r.IdDonVi == donVi.IdDonVi && r.Role == "PHO_HIEU_TRUONG")
                        .Select(r => r.IdNhanSu)
                        .FirstOrDefault();

                    if (truongDonViId != 0)
                    {
                        var donViName = _unitOfWork.iDmPhongBanRepository.TableNoTracking.FirstOrDefault(u => u.Id == kpi.IdDonVi);

                        await _notificationService.SendAsync(new NotificationMessage
                        {
                            Receiver = new Receiver { UserId = truongDonViId },
                            Title = $"KPI gửi chấm từ đơn vị {donViName}",
                            Content = $"Đơn vị đã gửi chấm KPI.",
                            Channel = NotificationChannel.Realtime
                        });
                    }
                }
            }
            else if (newStatus == KpiStatus.Scored || newStatus == KpiStatus.PrincipalApprove)
            {
                string title = newStatus == KpiStatus.Evaluated ? "KPI đã được đánh giá" : "Cấp trên phê duyệt";
                string content = newStatus == KpiStatus.Evaluated
                    ? $"KPI '{kpi.Kpi}' của bạn đã được Phó hiệu trưởng chấm. Đánh giá: {note}."
                    : $"KPI '{kpi.Kpi}' của bạn đã được Hiệu trưởng phê duyệt.";
                var truongDonVi = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .FirstOrDefault(r => r.IdDonVi == kpi.IdDonVi && r.Role == "TRUONG_DON_VI_CAP_2");

                await _notificationService.SendAsync(new NotificationMessage
                {
                    Receiver = new Receiver { UserId = truongDonVi.IdNhanSu },
                    Title = title,
                    Content = content,
                    Channel = NotificationChannel.Realtime
                });
            }
        }
    }
}
