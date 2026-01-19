using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.QuyetDinh
{
    public class FindByIdNsQuyetDinhHandler
        : IQueryHandler<NsQuyetDinhFindByIdRequestDto, NsQuyetDinhFindByIdResponseDto>
    {
        private readonly INsQuyetDinhService _service;

        public FindByIdNsQuyetDinhHandler(INsQuyetDinhService service)
        {
            _service = service;
        }

        public async Task<NsQuyetDinhFindByIdResponseDto> Handle(
            NsQuyetDinhFindByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.FindById(request.Id);
        }
    }
}
