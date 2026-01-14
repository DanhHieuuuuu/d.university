using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.Core.Domain.Entities.Kpi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiRoleService : ServiceBase, IKpiRoleService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _contextAccessor;

        public KpiRoleService(
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

        public void CreateKpiRole(CreateKpiRoleDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiRole)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );
            decimal currentTotal = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(x => x.IdNhanSu == dto.IdNhanSu && !x.Deleted)
                .Sum(x => x.TiLe ?? 0);
            decimal newTotal = currentTotal + dto.TiLe;

            if (newTotal > 100 || dto.TiLe <= 0)
            {
                throw new Exception($"Tỉ lệ không hợp lệ hoặc vượt quá 100%");
            }
            else
            {
                var newKpiRole = _mapper.Map<KpiRole>(dto);

                _unitOfWork.iKpiRoleRepository.Add(newKpiRole);
                _unitOfWork.iKpiRoleRepository.SaveChange();
            }
        }

        public void Delete(DeleteKpiRoleDto dto)
        {
            _logger.LogInformation($"{nameof(Delete)} method called.");
            if (dto.Ids == null || dto.Ids.Count == 0) return;
            foreach (var id in dto.Ids)
            {
                var role = _unitOfWork.iKpiRoleRepository.Table
                    .FirstOrDefault(x => x.Id == id && !x.Deleted);

                if (role == null)
                    throw new Exception($"KPI Role ID {id} không tồn tại.");
                role.Deleted = true;
                _unitOfWork.iKpiRoleRepository.Update(role);
            }

            _unitOfWork.iKpiRoleRepository.SaveChange();
        }

        public PageResultDto<KpiRoleResponseDto> FindAllKpiRole(KpiRoleRequestDto dto)
        {
            _logger.LogInformation($"{nameof(FindAllKpiRole)} method called.");

            var nhanSus = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .Select(ns => new
                {
                    ns.Id,
                    ns.MaNhanSu,
                    HoVaTen = ns.HoDem + " " + ns.Ten,
                    ns.HienTaiPhongBan
                })
                .ToList();

            // Lấy danh sách phòng ban
            var phongBans = _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToList();

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                nhanSus = nhanSus
                    .Where(ns =>
                    {
                        var pb = phongBans.FirstOrDefault(p => p.Id == ns.HienTaiPhongBan);
                        return ns.HoVaTen.ToLower().Contains(dto.Keyword.ToLower())
                               || (pb != null && pb.TenPhongBan.ToLower().Contains(dto.Keyword.ToLower()));
                    })
                    .ToList();
            }

            // Lấy danh sách KPI Role
            var roles = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => !r.Deleted && nhanSus.Select(ns => ns.Id).Contains(r.IdNhanSu) && (string.IsNullOrEmpty(dto.Role) || r.Role == dto.Role))
                .ToList();

            var mappedRoles = roles.Select(role =>
            {
                var ns = nhanSus.FirstOrDefault(x => x.Id == role.IdNhanSu);
                var pb = phongBans.FirstOrDefault(p => p.Id == ns?.HienTaiPhongBan);
                var pbkn = phongBans.FirstOrDefault(p => p.Id == role.IdDonVi);

                return new KpiRoleResponseDto
                {
                    ID = role.Id,
                    IdNhanSu = role.IdNhanSu,
                    MaNhanSu = ns?.MaNhanSu,
                    TenNhanSu = ns?.HoVaTen,
                    TenPhongBan = pb?.TenPhongBan,
                    IdDonVi = role.IdDonVi,
                    TenDonViKiemNhiem = pbkn?.TenPhongBan,
                    TiLe = role.TiLe,
                    Role = role.Role
                };
            }).AsQueryable();

            var totalCount = mappedRoles.Count();
            var pagedItems = mappedRoles
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<KpiRoleResponseDto>
            {
                Items = pagedItems,
                TotalItem = totalCount
            };
        }

        public List<GetKpiRoleByUserResponseDto> GetRoleByUserId()
        {
            _logger.LogInformation($"{nameof(GetRoleByUserId)} ");
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var result = (
                from role in _unitOfWork.iKpiRoleRepository.TableNoTracking
                where role.IdNhanSu == userId && role.Role != "HIEU_TRUONG" && role.Role != "TRUONG_DON_VI_CAP_2"
                select new GetKpiRoleByUserResponseDto
                {
                    Role = role.Role,
                    TiLe = role.TiLe
                }
            )
            .Distinct()
            .OrderByDescending(x => x.Role)
            .ToList();

            return result;
        }
        public void UpdateKpiRole(UpdateKpiRoleDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateKpiRole)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var kpiRole = _unitOfWork.iKpiRoleRepository.Table
                .FirstOrDefault(x => x.Id == dto.Id && !x.Deleted);
            if (kpiRole == null)
                throw new Exception("KPI Role không tồn tại hoặc đã bị xóa.");

            decimal currentTotal = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(x => x.IdNhanSu == dto.IdNhanSu && !x.Deleted && x.Id != dto.Id)
                .Sum(x => x.TiLe ?? 0);

            decimal newTotal = currentTotal + dto.TiLe;
            if (newTotal > 100 || dto.TiLe <= 0)
                throw new Exception("Tỉ lệ không hợp lệ hoặc vượt quá 100%");

            kpiRole.IdNhanSu = dto.IdNhanSu;
            kpiRole.Role = dto.Role;
            kpiRole.TiLe = dto.TiLe;
            kpiRole.IdDonVi = dto.IdDonVi;

            _unitOfWork.iKpiRoleRepository.Update(kpiRole);
            _unitOfWork.iKpiRoleRepository.SaveChange();
        }
    }
}
