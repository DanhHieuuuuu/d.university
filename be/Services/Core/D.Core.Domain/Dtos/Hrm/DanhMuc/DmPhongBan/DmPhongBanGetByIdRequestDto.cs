using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanGetByIdRequestDto : IQuery<DmPhongBanResponseDto>
    {
        public int Id { get; set; }
    }
}
