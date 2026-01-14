using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.HopDong;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.HopDong
{
    public class FindPagingNsHopDong
        : IQueryHandler<NsHopDongRequestDto, PageResultDto<NsHopDongResponseDto>>
    {
        public INsHopDongService _service;

        public FindPagingNsHopDong(INsHopDongService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<NsHopDongResponseDto>> Handle(
            NsHopDongRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllContract(request);
        }
    }
}
