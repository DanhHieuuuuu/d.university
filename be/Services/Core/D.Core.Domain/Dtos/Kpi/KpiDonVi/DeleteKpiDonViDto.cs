using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class DeleteKpiDonViDto : ICommand
    {
        public int Id { get; set; }
    }
}
