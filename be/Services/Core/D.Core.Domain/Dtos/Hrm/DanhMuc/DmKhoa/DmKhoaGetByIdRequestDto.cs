using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa
{
    public class DmKhoaGetByIdRequestDto : IQuery<DmKhoaResponseDto>
    {
        public int Id { get; set; }
    }
}
