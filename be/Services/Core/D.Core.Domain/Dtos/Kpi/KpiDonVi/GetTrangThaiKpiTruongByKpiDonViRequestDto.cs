using D.DomainBase.Common;


namespace D.Core.Domain.Dtos.Kpi.KpiDonVi
{
    public class GetTrangThaiKpiTruongByKpiDonViRequestDto : IQuery<GetTrangThaiKpiTruongByKpiDonViResponseDto>
    {
        public int idKpiDonVi { get; set; }
    }
}
