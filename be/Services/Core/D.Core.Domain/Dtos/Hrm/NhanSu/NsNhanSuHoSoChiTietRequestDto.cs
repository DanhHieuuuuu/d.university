using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuHoSoChiTietRequestDto : IQuery<NsNhanSuHoSoChiTietResponseDto>
    {
        public int IdNhanSu { get; set; }
    }
}
