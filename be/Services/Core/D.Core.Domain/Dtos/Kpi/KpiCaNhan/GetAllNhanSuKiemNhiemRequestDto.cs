using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Kpi.KpiCaNhan
{
    public class GetAllNhanSuKiemNhiemRequestDto : IQuery<List<GetAllNhanSuKiemNhiemResponseDto>>
    {
        public int? idPhongBan { get; set; }
    }
}
