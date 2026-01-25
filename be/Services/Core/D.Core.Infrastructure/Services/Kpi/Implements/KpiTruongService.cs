using AutoMapper;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Kpi.KpiLogStatus;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.Core.Infrastructure.Services.Kpi.Common;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
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
    public class KpiTruongService : ServiceBase, IKpiTruongService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IKpiCaNhanService _kpiCaNhanService;
        private readonly IKpiLogStatusService _logKpiService;
        private readonly INotificationService _notificationService;

        public KpiTruongService(
            ILogger<KpiTruongService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IKpiCaNhanService kpiCaNhanService,
            IKpiLogStatusService kpiLogStatusService,
            INotificationService notificationService
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _kpiCaNhanService = kpiCaNhanService;
            _logKpiService = kpiLogStatusService;
            _notificationService = notificationService;
        }

        public async Task CreateKpiTruong(CreateKpiTruongDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiTruong)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );


            var entity = _mapper.Map<KpiTruong>(dto);
            await _unitOfWork.iKpiTruongRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = entity.Id,
                OldStatus = 0,
                NewStatus = entity.TrangThai,
                Description = "Tạo KPI trường mới",
                CapKpi = 3
            });

        }

        public async Task DeleteKpi(DeleteKpiTruongDto dto)
        {
            _logger.LogInformation($"{nameof(DeleteKpi)} method called.");

            var kpi = await _unitOfWork.iKpiTruongRepository
                .Table
                .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
                throw new Exception($"KPI trường với Id = {dto.Id} không tồn tại hoặc đã bị xóa.");
            var oldStatus = kpi.TrangThai;
            kpi.Deleted = true;

            _unitOfWork.iKpiTruongRepository.Update(kpi);
            await _unitOfWork.SaveChangesAsync();
            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = kpi.Id,
                OldStatus = oldStatus,
                NewStatus = null,
                Description = "Xóa KPI đơn vị",
                CapKpi = 3
            });
        }

        public PageResultDto<KpiTruongDto> GetAllKpiTruong(FilterKpiTruongDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllKpiTruong)} => dto = {JsonSerializer.Serialize(dto)}");

            var isActive = _kpiCaNhanService.GetKpiIsActive();

            EnsureKpiTruongKyLuatExist().Wait();

            var queryBase = _unitOfWork.iKpiTruongRepository.TableNoTracking
                .Where(kpi => !kpi.Deleted
                    && (string.IsNullOrEmpty(dto.Keyword) || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower().Trim()))
                    && (dto.LoaiKpi == null || kpi.LoaiKpi == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.TrangThai == dto.TrangThai)
                );
            var summaryData = queryBase
                .Select(x => new { x.DiemKpi, x.DiemKpiCapTren, x.LoaiKpi }) 
                .ToList();
            decimal tongTuDanhGia = summaryData.Sum(x =>
                x.LoaiKpi == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0)
            );

            decimal tongCapTren = summaryData.Sum(x =>
                x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0)
            );
            var summaryDto = new KpiTruongSummaryDto
            {
                TongTuDanhGia = tongTuDanhGia,
                TongCapTren = tongCapTren,
                ByLoaiKpi = summaryData
                    .Where(x => x.LoaiKpi.HasValue)
                    .GroupBy(x => x.LoaiKpi!.Value)
                    .Select(g => new KpiTruongSummaryByLoaiDto
                    {
                        LoaiKpi = g.Key,
                        TuDanhGia = g.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpi ?? 0) : (x.DiemKpi ?? 0)),
                        CapTren = g.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0))
                    }).ToList()
            };

            var totalCount = queryBase.Count();
            var pagedItems = queryBase
                .OrderBy(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .Select(kpi => new KpiTruongDto
                {
                    Id = kpi.Id,
                    LinhVuc = kpi.LinhVuc,
                    ChienLuoc = kpi.ChienLuoc,
                    Kpi = kpi.Kpi,
                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    LoaiKpi = kpi.LoaiKpi,
                    NamHoc = kpi.NamHoc,
                    TrangThai = kpi.TrangThai,
                    KetQuaThucTe = kpi.KetQuaThucTe,
                    LoaiKetQua = kpi.LoaiKetQua,
                    DiemKpiCapTren = kpi.DiemKpiCapTren,
                    CapTrenDanhGia = kpi.CapTrenDanhGia,
                    DiemKpi = kpi.DiemKpi,
                    CongThuc = kpi.CongThucTinh,
                    IdCongThuc = kpi.IdCongThuc,
                    IsActive = ( kpi.TrangThai == KpiStatus.Assigned || kpi.TrangThai == KpiStatus.NeedEdit || kpi.TrangThai == KpiStatus.Declared) ? isActive : 0,
                })
                .ToList();

            return new PageResultDto<KpiTruongDto>
            {
                Items = pagedItems,
                TotalItem = totalCount,
                Summary = summaryDto
            };
        }

        public List<GetListKpiTruongResponseDto> GetListKpiTruong()
        {
            _logger.LogInformation(
                $"{nameof(GetListKpiTruong)}"
            );

            var query =
                from kpi in _unitOfWork.iKpiTruongRepository.TableNoTracking
                where !kpi.Deleted && kpi.LoaiKpi != 3
                select new GetListKpiTruongResponseDto
                {
                    Id = kpi.Id,
                    Kpi = kpi.Kpi,
                };
            return query.ToList();
        }

        public List<TrangThaiKpiTruongResponseDto> GetListTrangThai()
        {
            _logger.LogInformation($"{nameof(GetListTrangThai)}");


            var trangThaiExist = _unitOfWork.iKpiTruongRepository
                .TableNoTracking
                .Where(x => x.TrangThai != null)
                .Select(x => x.TrangThai!.Value)
                .Distinct()
                .OrderBy(x => x)
                .Select(x => new TrangThaiKpiTruongResponseDto
                {
                    Value = x,
                    Label = KpiStatus.Names.ContainsKey(x)
                        ? KpiStatus.Names[x]
                        : "Không xác định"
                })
                .ToList();

            return trangThaiExist;
        }

        public List<GetListYearKpiTruongDto> GetListYear()
        {
            _logger.LogInformation($"{nameof(GetListYear)} ");
            var years = _unitOfWork.iKpiTruongRepository
                        .TableNoTracking
                        .Where(x => !string.IsNullOrEmpty(x.NamHoc))
                        .Select(x => x.NamHoc)
                        .Distinct()
                        .OrderByDescending(x => x)
                        .ToList();
            var result = years.Select(y => new GetListYearKpiTruongDto
            {
                NamHoc = y
            }).ToList();

            return result;
        }

        public async Task GiaoKpiHieuTruong(GiaoKpiHieuTruongDto dto)
        {
            _logger.LogInformation($"{nameof(GiaoKpiHieuTruong)} dto={JsonSerializer.Serialize(dto)}");

            var kpiTruongs = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                .Where(x => x.NamHoc == dto.NamHoc && !x.Deleted)
                .ToListAsync();

            if (!kpiTruongs.Any())
                throw new Exception("Không tồn tại Kpi trường");

            var idNhanSus = await _unitOfWork.iKpiRoleRepository.Table
                .Where(x => x.Role == "HIEU_TRUONG" && !x.Deleted)
                .Select(x => x.IdNhanSu)
                .ToListAsync();

            foreach (var idNhanSu in idNhanSus)
            {
                var nhanSu = await _unitOfWork.iNsNhanSuRepository.Table
                    .FirstOrDefaultAsync(x => x.Id == idNhanSu);

                if (nhanSu == null) continue;
                foreach (var kpiTruong in kpiTruongs)
                {
                    // Kiểm tra tồn tại KPI cá nhân
                    var existed = await _unitOfWork.iKpiCaNhanRepository.TableNoTracking.AnyAsync(x =>
                            x.KPI == kpiTruong.Kpi &&
                            x.IdNhanSu == nhanSu.Id &&
                            !x.Deleted);

                    if (existed)
                        continue; 

                    var kpiCaNhan = new KpiCaNhan
                    {
                        KPI = kpiTruong.Kpi,
                        MucTieu = kpiTruong.MucTieu ?? "0",
                        TrongSo = kpiTruong.TrongSo,
                        LoaiKPI = kpiTruong.LoaiKpi ?? 0,
                        IdNhanSu = nhanSu.Id,
                        NamHoc = kpiTruong.NamHoc,
                        Status = KpiStatus.Assigned
                    };

                    await _unitOfWork.iKpiCaNhanRepository.AddAsync(kpiCaNhan);

                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateKetQuaThucTe(UpdateKpiThucTeKpiTruongListDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateKetQuaThucTe)} dto={JsonSerializer.Serialize(dto)}");
            using var transaction = _unitOfWork.Database.BeginTransaction();
            try
            {
                foreach (var item in dto.Items)
                {
                    var kpi = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                        .FirstOrDefaultAsync(x => x.Id == item.Id && !x.Deleted);

                    if (kpi == null)
                        throw new Exception("Không tìm thấy KPI");

                    if (item.KetQuaThucTe.HasValue)
                    {
                        var oldStatus = kpi.TrangThai;
                        var oldScore = kpi.DiemKpi;
                        kpi.KetQuaThucTe = item.KetQuaThucTe;
                        kpi.DiemKpi = item.DiemKpi;
                        kpi.TrangThai = KpiStatus.Declared;
                        if (kpi.LoaiKpi == 3)
                        {
                            kpi.DiemKpi = TinhDiemKPI.GetDiemTruTuanThuDonVi(kpi.Kpi, kpi.KetQuaThucTe.Value);
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
                        _unitOfWork.iKpiTruongRepository.Update(kpi);
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.TrangThai,
                            Description = $"Cập nhật kết quả thực tế, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpi}",
                            CapKpi = 3
                        });
                    }
                }

                await _unitOfWork.iKpiTruongRepository.SaveChangeAsync();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task UpdateKpiTruong(UpdateKpiTruongDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateKpiTruong)} dto={JsonSerializer.Serialize(dto)}");
            // Tìm KPI cập nhật
            var kpiTruong = await _unitOfWork.iKpiTruongRepository.Table.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.Deleted);

            if (kpiTruong == null)
            {
                throw new Exception($"Không tìm thấy KPI cá nhân với Id={dto.Id}");
            }

            var oldStatus = kpiTruong.TrangThai;
            // Cập nhật thông tin
            kpiTruong.LinhVuc = dto.LinhVuc;
            kpiTruong.ChienLuoc = dto.ChienLuoc;
            kpiTruong.Kpi = dto.Kpi;
            kpiTruong.MucTieu = dto.MucTieu;
            kpiTruong.TrongSo = dto.TrongSo;
            kpiTruong.LoaiKpi = dto.LoaiKpi;
            kpiTruong.NamHoc = dto.NamHoc;
            kpiTruong.KetQuaThucTe = dto.KetQuaThucTe;
            kpiTruong.IdCongThuc = dto.IdCongThuc;
            kpiTruong.CongThucTinh = dto.CongThucTinh;
            kpiTruong.LoaiKetQua = dto.LoaiKetQua;
            kpiTruong.TrangThai = KpiStatus.Edited;

            _unitOfWork.iKpiTruongRepository.Update(kpiTruong);
            await _unitOfWork.iKpiTruongRepository.SaveChangeAsync();

            _logKpiService.InsertLog(new InsertKpiLogStatusDto
            {
                KpiId = kpiTruong.Id,
                OldStatus = oldStatus,
                NewStatus = kpiTruong.TrangThai,
                Description = $"Cập nhật Kpi trường",
                CapKpi = 3
            });
        }

        public async Task UpdateTrangThaiKpiTruong(UpdateTrangThaiKpiTruongDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateTrangThaiKpiTruong)} => dto = {JsonSerializer.Serialize(dto)}");

            if (dto.Ids == null || !dto.Ids.Any())
                throw new UserFriendlyException(ErrorCode.BadRequest, "Danh sách ID không được để trống.");

            var kpiList = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                .Where(s => dto.Ids.Contains(s.Id))
                .ToListAsync();

            if (!kpiList.Any())
                throw new Exception("Không tìm thấy KPI nào để cập nhật.");

            foreach (var kpi in kpiList)
            {
                var oldStatus = kpi.TrangThai;
                kpi.TrangThai = dto.TrangThai;
                _unitOfWork.iKpiTruongRepository.Update(kpi);
                _logKpiService.InsertLog(new InsertKpiLogStatusDto
                {
                    KpiId = kpi.Id,
                    OldStatus = oldStatus,
                    NewStatus = kpi.TrangThai,
                    Description = "Cập nhật trạng thái KPI trường",
                    CapKpi = 3,
                    Reason = dto.Note
                });
                await SendKpiStatusNotification(kpi, dto.TrangThai, dto.Note);
            }

            await _unitOfWork.iKpiTruongRepository.SaveChangeAsync();
        }

        public void UpdateKetQuaCapTren(UpdateKetQuaCapTrenKpiTruongListDto dto)
        {
            using var transaction = _unitOfWork.Database.BeginTransaction();
            try
            {
                foreach (var item in dto.Items)
                {
                    var kpi = _unitOfWork.iKpiTruongRepository.Table
                        .FirstOrDefault(x => x.Id == item.Id && !x.Deleted);

                    if (kpi == null)
                        throw new Exception("Không tìm thấy KPI cá nhân");

                    if (item.KetQuaCapTren.HasValue)
                    {
                        var oldStatus = kpi.TrangThai;
                        var oldScore = kpi.DiemKpi;
                        kpi.CapTrenDanhGia = item.KetQuaCapTren;
                        kpi.DiemKpiCapTren = item.DiemKpiCapTren;
                        kpi.TrangThai = KpiStatus.Evaluated;
                        if (kpi.LoaiKpi == 3)
                        {
                            kpi.DiemKpiCapTren = TinhDiemKPI.GetDiemTruTuanThuDonVi(kpi.Kpi, kpi.CapTrenDanhGia.Value);
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
                        _unitOfWork.iKpiTruongRepository.Update(kpi);
                        _logKpiService.InsertLog(new InsertKpiLogStatusDto
                        {
                            KpiId = kpi.Id,
                            OldStatus = oldStatus,
                            NewStatus = kpi.TrangThai,
                            Description = $"Cập nhật kết quả cấp trên, điểm cũ: {oldScore}, điểm mới: {kpi.DiemKpi}",
                            CapKpi = 3
                        });
                    }
                }

                _unitOfWork.iKpiTruongRepository.SaveChange();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<object> GetKpiTruongContextForAi()
        {
            var kpis = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                .Where(x => !x.Deleted).ToListAsync();

            if (!kpis.Any()) return null;

            return kpis.Select(k =>
            {
                double.TryParse(k.TrongSo?.Replace("%", "").Trim(), out double ts);
                int modifier = k.LoaiKpi == 3 ? -1 : 1;

                return new
                {
                    NamHoc = k.NamHoc,
                    LinhVuc = k.LinhVuc ?? "Chung",
                    ChienLuoc = k.ChienLuoc ?? "N/A",
                    TenKPI = k.Kpi,
                    LoaiKPI = k.LoaiKpi switch
                    {
                        1 => "Chức năng",
                        2 => "Mục tiêu",
                        3 => "Tuân thủ",
                        _ => "Khác"
                    },
                    MucTieu = k.MucTieu ?? "N/A",
                    TrongSo = ts,
                    KetQua = k.KetQuaThucTe ?? 0,
                    Diem = (k.DiemKpiCapTren ?? 0) * modifier,
                    CongThuc = k.CongThucTinh
                };
            }).ToList();
        }
        private async Task SendKpiStatusNotification(KpiTruong kpi, int newStatus, string? note)
        {
            if (newStatus == KpiStatus.Evaluated)
            {
                var truongDonViId = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .Where(r => r.Role == "CHU_TICH_HOI_DONG_TRUONG")
                    .Select(r => r.IdNhanSu)
                    .FirstOrDefault();

                if (truongDonViId != 0)
                {
                    await _notificationService.SendAsync(new NotificationMessage
                    {
                        Receiver = new Receiver { UserId = truongDonViId },
                        Title = $"KPI gửi chấm từ Hiệu trưởng",
                        Content = $"Hiệu trưởng đã gửi chấm KPI trường.",
                        Channel = NotificationChannel.Realtime
                    });
                }
            }
            else if (newStatus == KpiStatus.Scored || newStatus == KpiStatus.PrincipalApprove)
            {
                string title = newStatus == KpiStatus.Evaluated ? "KPI đã được đánh giá" : "Hội đồng phê duyệt";
                string content = newStatus == KpiStatus.Evaluated
                    ? $"KPI '{kpi.Kpi}' của bạn đã được Hội đồng đánh giá. Đánh giá: {note ?? "Không đánh giá gì thêm"}."
                    : $"KPI '{kpi.Kpi}' của bạn đã được hội đồng phê duyệt.";
                var hieuTruong = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .FirstOrDefault(r =>  r.Role == "HIEU_TRUONG");

                await _notificationService.SendAsync(new NotificationMessage
                {
                    Receiver = new Receiver { UserId = hieuTruong.IdNhanSu },
                    Title = title,
                    Content = content,
                    Channel = NotificationChannel.Realtime
                });
            }
        }

        private async Task EnsureKpiTruongKyLuatExist()
        {
            var hasData = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                .AnyAsync(x =>  x.LoaiKpi == 3
                            && x.Kpi.Contains("thời gian làm việc")
                            && !x.Deleted);
            if (!hasData)
            {
                await CreateTemplateKpiTruong();
            }
        }

        private async Task CreateTemplateKpiTruong()
        {
            var kpis = new List<KpiTruong>
            {
                CreateTemplateItem("2026",
                    "Tỷ lệ trung bình toàn trường về vi phạm thời gian làm việc",
                    "<= 5%",
                    "Nếu tỷ lệ vi phạm > 5% -> Cứ mỗi 1% vượt quá, trừ 2% điểm KPI"),

                CreateTemplateItem("2026",
                    "Tỷ lệ trung bình toàn trường về vi phạm quy định chấm công",
                    "<= 5%",
                    "Nếu tỷ lệ vi phạm > 5% -> Cứ mỗi 1% vượt quá, trừ 2% điểm KPI"),

                CreateTemplateItem("2026",
                    "Tỷ lệ trung bình toàn trường về vi phạm tuân thủ trật tự và tác phong làm việc",
                    "<= 2%",
                    "Nếu tỷ lệ vi phạm > 2% -> Cứ mỗi 1% vượt quá, trừ 5% điểm KPI"),

                CreateTemplateItem("2026",
                    "Tỷ lệ trung bình toàn trường về vi phạm quy tắc ứng xử (với sinh viên/ đồng nghiệp/ khách)",
                    "<= 2%",
                    "Nếu tỷ lệ vi phạm > 2% -> Cứ mỗi 1% vượt quá, trừ 5% điểm KPI"),

                CreateTemplateItem("2026",
                    "Tỷ lệ trung bình toàn trường về vi phạm quy định sử dụng đồng phục, thẻ nhân viên",
                    "<= 2%",
                    "Nếu tỷ lệ vi phạm > 2% -> Cứ mỗi 1% vượt quá, trừ 5% điểm KPI"),

                CreateTemplateItem("2026",
                    "Tỷ lệ trung bình toàn trường về vi phạm quy trình nghiệp vụ",
                    "<= 2%",
                    "Nếu tỷ lệ vi phạm > 2% -> Cứ mỗi 1% vượt quá, trừ 5% điểm KPI")
            };

            await _unitOfWork.iKpiTruongRepository.AddRangeAsync(kpis);
            await _unitOfWork.iKpiTruongRepository.SaveChangeAsync();
        }

        private KpiTruong CreateTemplateItem(string namHoc, string name, string mucTieu, string congThuc)
        {
            return new KpiTruong
            {
                NamHoc = namHoc,
                LoaiKpi = 3,
                Kpi = name,
                MucTieu = mucTieu,
                TrongSo = "0",
                CongThucTinh = congThuc,
                LoaiKetQua = "PERCENT",
                TrangThai = KpiStatus.Assigned,
                CreatedDate = DateTime.Now,
                Deleted = false
            };
        }
    }
}
