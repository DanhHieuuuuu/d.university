using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Hrm.QuyetDinh
{
    public class NsQuyetDinhFindByIdRequestDto : IQuery<NsQuyetDinhFindByIdResponseDto>
    {
        public int Id { get; set; }
    }
}
