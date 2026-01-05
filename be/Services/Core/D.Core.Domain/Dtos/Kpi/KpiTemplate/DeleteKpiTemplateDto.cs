

using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiTemplate
{
    public class DeleteKpiTemplateDto : ICommand
    {
        public int Id { get; set; }
    }
}
