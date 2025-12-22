using D.Core.Domain.Dtos.Kpi.KpiTemplate;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiTemplateService
    {
        Task<KpiTemplateDto> CreateKpiTemplate(CreateKpiTemplateDto dto);
        PageResultDto<KpiTemplateDto> GetAllKpiTemplate(FilterKpiTemplateDto dto);
        Task DeleteKpiTemplate(DeleteKpiTemplateDto dto);
        Task<KpiTemplateDto> UpdateKpiTemplate(UpdateKpiTemplateDto dto);

        Task<List<TemplateTypeDto>> GetTemplateTypesAsync();
        Task<List<SyncKpiTemplateResponseDto>> SyncKpiTemplate(SyncKpiTemplateRequestDto dto);
    }
}
