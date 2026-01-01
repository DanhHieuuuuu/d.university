

using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class GetNhanSuFromKpiDonViDto : IQuery<List<NhanSuDaGiaoDto>>
    {
        public int? IdKpiDonVi { get; set; }
    }
}
