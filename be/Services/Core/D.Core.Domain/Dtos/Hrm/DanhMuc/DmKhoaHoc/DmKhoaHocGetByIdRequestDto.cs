using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc
{
    public class DmKhoaHocGetByIdRequestDto : IQuery<DmKhoaHocResponseDto>
    {
        public int Id { get; set; }
    }
}
