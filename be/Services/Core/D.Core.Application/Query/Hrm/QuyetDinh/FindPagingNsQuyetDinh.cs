using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.QuyetDinh
{
    public class FindPagingNsQuyetDinh
        : IQueryHandler<NsQuyetDinhRequestDto, PageResultDto<NsQuyetDinhResponseDto>>
    {
        public INsDecisionService _service;

        public FindPagingNsQuyetDinh(INsDecisionService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<NsQuyetDinhResponseDto>> Handle(
            NsQuyetDinhRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.FindPagingNsQuyetDinh(request);
        }
    }
}
