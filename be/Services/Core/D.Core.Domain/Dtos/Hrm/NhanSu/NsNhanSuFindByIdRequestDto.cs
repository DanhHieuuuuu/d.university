using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.NhanSu
{
    public class NsNhanSuFindByIdRequestDto : IQuery<NsNhanSuFindByIdResponseDto>
    {
        public int IdNhanSu { get; set; }
    }
}
