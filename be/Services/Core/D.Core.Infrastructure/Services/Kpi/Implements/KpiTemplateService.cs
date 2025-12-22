using AutoMapper;
using D.Auth.Domain;
using D.ControllerBase.Exceptions;
using D.Core.Domain;
using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.Core.Domain.Dtos.Kpi.KpiTruong;
using D.Core.Domain.Entities.Kpi;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Domain.Migrations;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiTemplateService : ServiceBase, IKpiTemplateService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiTemplateService(
            ILogger<KpiRoleService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<KpiTemplateDto> CreateKpiTemplate(CreateKpiTemplateDto dto)
        {
            _logger.LogInformation(
               $"{nameof(CreateKpiTemplate)} method called. Dto: {JsonSerializer.Serialize(dto)}"
           );


            var entity = _mapper.Map<KpiTemplate>(dto);
            await _unitOfWork.iKpiTemplateRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            var result = _mapper.Map<KpiTemplateDto>(entity);
            return result;

        }

        public async Task DeleteKpiTemplate(DeleteKpiTemplateDto dto)
        {
            _logger.LogInformation($"{nameof(DeleteKpiTemplate)} method called.");

            var kpi = await _unitOfWork.iKpiTemplateRepository
                .TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == dto.Id && !x.Deleted);

            if (kpi == null)
                throw new Exception($"KPI trường với Id = {dto.Id} không tồn tại hoặc đã bị xóa.");

            kpi.Deleted = true;

            _unitOfWork.iKpiTemplateRepository.Update(kpi);
            await _unitOfWork.SaveChangesAsync();
        }

        public PageResultDto<KpiTemplateDto> GetAllKpiTemplate(FilterKpiTemplateDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllKpiTemplate)} => dto = {JsonSerializer.Serialize(dto)}"
            );
            var kpis = _unitOfWork.iKpiTemplateRepository.TableNoTracking.ToList();

            var query =
                from kpi in kpis
                where
                    !kpi.Deleted &&
                    (
                    string.IsNullOrEmpty(dto.Keyword)
                    || kpi.KPI!.ToLower().Contains(dto.Keyword.ToLower().Trim())
                    )
                    && (dto.LoaiKpi == null || kpi.LoaiKPI == dto.LoaiKpi)
                    && (string.IsNullOrEmpty(dto.NamHoc) || kpi.NamHoc == dto.NamHoc)
                    && (dto.LoaiTemplate == null || kpi.LoaiTemplate == dto.LoaiTemplate)
                select new KpiTemplateDto
                {
                    Id = kpi.Id,
                    KPI = kpi.KPI,
                    MucTieu = kpi.MucTieu,
                    TrongSo = kpi.TrongSo,
                    LoaiKPI = kpi.LoaiKPI,
                    LoaiTemplate = kpi.LoaiTemplate,
                    NamHoc = kpi.NamHoc,
                };

            var totalCount = query.Count();
            var pagedItems = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            return new PageResultDto<KpiTemplateDto>
            {
                Items = pagedItems,
                TotalItem = totalCount
            };
        }

        public Task<List<TemplateTypeDto>> GetTemplateTypesAsync()
        {
            return Task.FromResult(KpiTemplateTypes.All.ToList());
        }

        public async Task<List<SyncKpiTemplateResponseDto>> SyncKpiTemplate(SyncKpiTemplateRequestDto dto)
        {
            var userId = (CommonUntil.GetCurrentUserId(_contextAccessor)).ToString();
            if (dto.TemplateIds == null || !dto.TemplateIds.Any())
                throw new Exception( "Chưa chọn template để đồng bộ.");

            if (dto.Roles == null || !dto.Roles.Any())
                throw new UserFriendlyException(ErrorCode.BadRequest, "Chưa chọn Role để đồng bộ.");

            // Lấy danh sách template hợp lệ
            var templates = _unitOfWork.iKpiTemplateRepository.TableNoTracking
                .Where(t => dto.TemplateIds.Contains(t.Id) && !t.Deleted)
                .ToList();

            if (!templates.Any())
                throw new UserFriendlyException(ErrorCode.NotFound, "Không tìm thấy template hợp lệ.");

            var templateTypes = KpiTemplateTypes.All;
            var selectedRoles = dto.Roles
                .Select(r => r.Trim())
                .Distinct()
                .ToList();

            var templatesWithRole = templates
                .Select(t =>
                {
                    var type = templateTypes.FirstOrDefault(x => x.Value == t.LoaiTemplate.ToString());
                    if (type == null) return null;

                    // FE gửi role = Name
                    if (!selectedRoles.Contains(type.Name))
                        return null;

                    return new
                    {
                        Template = t,
                        RoleKey = type.Name,
                        RoleName = type.Name
                    };
                })
                .Where(x => x != null)
                .ToList();

            if (!templatesWithRole.Any())
                return new List<SyncKpiTemplateResponseDto>();

            // Nhân sự theo role
            var nhanSus = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => selectedRoles.Contains(r.Role) && !r.Deleted)
                .ToList();

            var result = new List<SyncKpiTemplateResponseDto>();
            var kpiCaNhanList = new List<KpiCaNhan>();

            foreach (var tr in templatesWithRole)
            {
                var nhanSusForRole = nhanSus.Where(r => r.Role == tr.RoleKey).ToList();
                if (!nhanSusForRole.Any()) continue;

                var nhanSuNames = new List<string>();

                foreach (var ns in nhanSusForRole)
                {
                    var exists = _unitOfWork.iKpiCaNhanRepository.Table.Any(k =>
                        k.IdNhanSu == ns.IdNhanSu &&
                        k.KPI == tr.Template.KPI &&
                        k.NamHoc == tr.Template.NamHoc &&
                        k.Role == ns.Role &&
                        !k.Deleted);

                    if (exists) continue;

                    kpiCaNhanList.Add(new KpiCaNhan
                    {
                        KPI = tr.Template.KPI,
                        MucTieu = tr.Template.MucTieu,
                        TrongSo = tr.Template.TrongSo,
                        LoaiKPI = tr.Template.LoaiKPI,
                        NamHoc = tr.Template.NamHoc,
                        IdNhanSu = ns.IdNhanSu,
                        Role = ns.Role,
                        Status = KpiStatus.Create,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now
                    });

                    var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                        .FirstOrDefault(x => x.Id == ns.IdNhanSu);

                    if (nhanSu != null)
                        nhanSuNames.Add($"{nhanSu.HoDem} {nhanSu.Ten}");
                }

                if (nhanSuNames.Any())
                {
                    result.Add(new SyncKpiTemplateResponseDto
                    {
                        LoaiTemplate = tr.RoleName,
                        Role = tr.RoleName,
                        KPI = tr.Template.KPI,
                        NhanSu = nhanSuNames
                    });
                }
            }

            if (kpiCaNhanList.Any())
            {
                _unitOfWork.iKpiCaNhanRepository.AddRange(kpiCaNhanList);
                await _unitOfWork.SaveChangesAsync();
            }

            return result;
        }

        public async Task<KpiTemplateDto> UpdateKpiTemplate(UpdateKpiTemplateDto dto)
        {
            _logger.LogInformation($"{nameof(UpdateKpiTemplate)} dto={JsonSerializer.Serialize(dto)}");
            // Tìm KPI cập nhật
            var kpiTemplate = await _unitOfWork.iKpiTemplateRepository.Table.FirstOrDefaultAsync(x => x.Id == dto.Id && !x.Deleted);

            if (kpiTemplate == null)
            {
                throw new Exception($"Không tìm thấy KPI cá nhân với Id={dto.Id}");
            }


            // Cập nhật thông tin
            kpiTemplate.KPI = dto.KPI;
            kpiTemplate.MucTieu = dto.MucTieu;
            kpiTemplate.TrongSo = dto.TrongSo;
            kpiTemplate.LoaiKPI = dto.LoaiKPI;
            kpiTemplate.NamHoc = dto.NamHoc;
            kpiTemplate.LoaiTemplate = dto.LoaiTemplate;

            await _unitOfWork.iKpiTruongRepository.SaveChangeAsync();
            return _mapper.Map<KpiTemplateDto>(kpiTemplate);
        }
    }
}
