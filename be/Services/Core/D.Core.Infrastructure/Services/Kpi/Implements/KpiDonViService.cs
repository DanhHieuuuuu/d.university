using AutoMapper;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
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

        public void CreateKpiDonVi(CreateKpiDonViDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiDonVi)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var donVi = _unitOfWork.iDmPhongBanRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.IdDonVi);
            if (donVi == null)
                throw new Exception("Không tìm thấy đơn vị");

            //var maxSTT = _unitOfWork.iKpiDonViRepository.TableNoTracking
            //        .Where(k =>
            //            k.IdDonVi == donVi.Id
            //            && k.LoaiKpi == dto.LoaiKpi
            //            && !k.Deleted
            //        )
            //        .Max(k => (int?)k.STT) ?? 0;
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
            var kpis = _unitOfWork.iKpiDonViRepository.TableNoTracking.ToList();
            var donvis = _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToDictionary(x => x.Id, x => x.TenPhongBan);
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var user = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x => x.Id == userId);


            var query =
                from kpi in kpis
                where
                    !kpi.Deleted &&
                    (
                    string.IsNullOrEmpty(dto.Keyword)
                    || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower().Trim())
                    )
                    && (dto.IdDonVi == null || kpi.IdDonVi == dto.IdDonVi)
                    && (dto.LoaiKpi == null || kpi.LoaiKpi == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.TrangThai == null || kpi.TrangThai == dto.TrangThai)
                    && kpi.IdDonVi == user?.HienTaiPhongBan
                select new KpiDonViDto
                {
                    Id = kpi.Id,
                    Kpi = kpi.Kpi,
                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    DonVi = kpi.IdDonVi != null && donvis.ContainsKey(kpi.IdDonVi.Value)
                            ? donvis[kpi.IdDonVi.Value]
                            : string.Empty,
                    LoaiKpi = kpi.LoaiKpi,
                    LoaiKpiText = kpi.LoaiKpi.HasValue && KpiTypes.Names.ContainsKey(kpi.LoaiKpi.Value)
                        ? KpiTypes.Names[kpi.LoaiKpi.Value]
                        : "Không xác định",
                    NamHoc = kpi.NamHoc,
                    TrangThai = kpi.TrangThai,
                    TrangThaiText = kpi.TrangThai.HasValue && KpiStatus.Names.ContainsKey(kpi.TrangThai.Value)
                            ? KpiStatus.Names[kpi.TrangThai.Value]
                            : "Không xác định",
                    KetQuaThucTe = kpi.KetQuaThucTe,
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
            var kpis = _unitOfWork.iKpiDonViRepository.TableNoTracking.ToList();
            var donvis = _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToDictionary(x => x.Id, x => x.TenPhongBan);


            var query =
                from kpi in kpis
                where
                    !kpi.Deleted &&
                    (
                    string.IsNullOrEmpty(dto.Keyword)
                    || kpi.Kpi!.ToLower().Contains(dto.Keyword.ToLower().Trim())
                    )
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
                    LoaiKpiText = kpi.LoaiKpi.HasValue && KpiTypes.Names.ContainsKey(kpi.LoaiKpi.Value)
                        ? KpiTypes.Names[kpi.LoaiKpi.Value]
                        : "Không xác định",
                    NamHoc = kpi.NamHoc,
                    TrangThai = kpi.TrangThai,
                    KetQuaThucTe = kpi.KetQuaThucTe
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

        public List<NhanSuDaGiaoDto> GetNhanSuByKpiDonVi(GetNhanSuFromKpiDonViDto dto)
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
                    TyLeThamGia = x.TyLeThamGia
                };
            }).ToList();

            return result;
        }

        public async void GiaoKpiDonVi(GiaoKpiDonViDto dto)
        {
            var duplicationId = dto.NhanSus
                .GroupBy(x => x.IdNhanSu)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key)
                .ToList();

            if (duplicationId.Any())
                throw new Exception("KPI cá nhân đã tồn tại");

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
                    if (existed.TrongSo != nsDto.TrongSo ||
                        existed.TyLeThamGia != nsDto.TyLeThamGia)
                    {
                        existed.TrongSo = nsDto.TrongSo;
                        existed.TyLeThamGia = nsDto.TyLeThamGia;
                        existed.ModifiedDate = DateTime.UtcNow;
                    }
                    continue;
                }

                _unitOfWork.iKpiCaNhanRepository.Add(new KpiCaNhan
                {
                    KPI = kpiDonVi.Kpi,
                    MucTieu = kpiDonVi.MucTieu ?? "0",
                    TrongSo = nsDto.TrongSo,
                    LoaiKPI = kpiDonVi.LoaiKpi ?? 0,
                    IdNhanSu = nsDto.IdNhanSu,
                    IdKpiDonVi = kpiDonVi.Id,
                    NamHoc = kpiDonVi.NamHoc,
                    TyLeThamGia = nsDto.TyLeThamGia,
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
                        kpi.TrangThai = KpiStatus.Declared;

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
            //if (kpi.IdDonVi != dto.IdDonVi)
            //{
            //    var maxSTT = _unitOfWork.iKpiDonViRepository.TableNoTracking
            //        .Where(k => k.IdDonVi == dto.IdDonVi && k.LoaiKpi == dto.LoaiKpi && !k.Deleted)
            //        .Select(k => (int?)k.STT)
            //        .Max() ?? 0;
            //    kpi.STT = maxSTT + 1;
            //}

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
    }
}
