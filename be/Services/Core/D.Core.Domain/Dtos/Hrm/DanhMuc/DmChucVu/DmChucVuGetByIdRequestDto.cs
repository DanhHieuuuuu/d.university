using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu
{
    public class DmChucVuGetByIdRequestDto : IQuery<DmChucVuResponseDto>
    {
        public int Id { get; set; }
    }
}
