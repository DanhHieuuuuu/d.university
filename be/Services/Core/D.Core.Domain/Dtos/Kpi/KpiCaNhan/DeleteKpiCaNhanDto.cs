using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class DeleteKpiCaNhanDto : ICommand
    {
        public int Id { get; set; }
    }
}
