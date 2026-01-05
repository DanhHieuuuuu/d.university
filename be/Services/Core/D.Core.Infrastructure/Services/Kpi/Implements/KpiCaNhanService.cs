using AutoMapper;
using d.Shared.Permission.Error;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
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

            var kpis = await _unitOfWork.iKpiCaNhanRepository
                .TableNoTracking
                .ToListAsync();
            var phongBans = await _unitOfWork.iDmPhongBanRepository
                .TableNoTracking
                .ToDictionaryAsync(x => x.Id, x => x.TenPhongBan);
            var nhanSus = await _unitOfWork.iNsNhanSuRepository
                .TableNoTracking
                .Where(ns =>
                    (!dto.IdNhanSu.HasValue || ns.Id == dto.IdNhanSu) &&
                    (!dto.IdPhongBan.HasValue || ns.HienTaiPhongBan == dto.IdPhongBan)
                )
                .ToDictionaryAsync(
                    x => x.Id,
                    x => new
                    {
                        HoTenDayDu = x.HoDem + " " + x.Ten,
                        TenPhongBan = x.HienTaiPhongBan != null &&
                                      phongBans.ContainsKey(x.HienTaiPhongBan.Value)
                                        ? phongBans[x.HienTaiPhongBan.Value]
                                        : string.Empty
                    }
                );
            var query =
                from kpi in kpis
                where
                    !kpi.Deleted &&
                    (string.IsNullOrEmpty(dto.Keyword) || kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim())) &&
                    (!dto.LoaiKpi.HasValue || kpi.LoaiKPI == dto.LoaiKpi) &&
                    (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc) &&
                    (!dto.TrangThai.HasValue || kpi.Status == dto.TrangThai) &&
                    nhanSus.ContainsKey(kpi.IdNhanSu)
                select new KpiCaNhanDto
                {
                    Id = kpi.Id,
                    STT = kpi.STT,
                    KPI = kpi.KPI,
                    LoaiKpi = kpi.LoaiKPI,
                    LinhVuc = kpi.LinhVuc,
                    NhanSu = nhanSus[kpi.IdNhanSu].HoTenDayDu,
                    IdNhanSu = kpi.IdNhanSu,
                    PhongBan = nhanSus[kpi.IdNhanSu].TenPhongBan,
                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    Role = kpi.Role,
                    NamHoc = kpi.NamHoc,
                    TrangThai = kpi.Status,
                    LoaiCongThuc = kpi.LoaiCongThuc,
                    KetQuaThucTe = kpi.KetQuaThucTe,
                    CapTrenDanhGia = kpi.CapTrenDanhGia,
                    DiemKpiCapTren = kpi.DiemKpiCapTren,
                    DiemKpi = kpi.DiemKpi
                };

            var totalCount = query.Count();
            var pagedItems = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<KpiCaNhanDto>
            {
                Items = pagedItems,
                TotalItem = totalCount
            };
        }


        public async Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhanKeKhai(
            FilterKpiKeKhaiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingKpiCaNhanKeKhai)} => dto = {JsonSerializer.Serialize(dto)}"
            );

            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
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
                    LoaiCongThuc = kpi.LoaiCongThuc,
                    TrangThai = kpi.Status,
                    KetQuaThucTe = kpi.KetQuaThucTe,
                    CapTrenDanhGia = kpi.CapTrenDanhGia,
                    DiemKpiCapTren = kpi.DiemKpiCapTren,
                    DiemKpi = kpi.DiemKpi
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
    }
}
