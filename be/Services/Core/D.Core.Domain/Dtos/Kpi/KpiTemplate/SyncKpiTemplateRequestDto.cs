

using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTemplate
{
    public class SyncKpiTemplateRequestDto : ICommand<List<SyncKpiTemplateResponseDto>>
    {
        public List<int> TemplateIds { get; set; } = new();
        public List<string> Roles { get; set; } = new();
    }
}
