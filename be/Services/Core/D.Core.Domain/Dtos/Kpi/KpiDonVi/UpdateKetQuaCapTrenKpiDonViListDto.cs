using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class UpdateKetQuaCapTrenKpiDonVi
    {
        public int Id { get; set; }
        public decimal? KetQuaCapTren { get; set; }
        public decimal? DiemKpiCapTren { get; set; }
    }
    public class UpdateKetQuaCapTrenKpiDonViListDto : ICommand
    {
        public List<UpdateKetQuaCapTrenKpiDonVi>? Items { get; set; }
    }
}
