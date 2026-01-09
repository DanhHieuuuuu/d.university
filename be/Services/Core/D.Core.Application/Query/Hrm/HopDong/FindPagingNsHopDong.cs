using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.HopDong;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.HopDong
{
    public class FindPagingNsHopDong
        : IQueryHandler<NsHopDongRequestDto, PageResultDto<NsHopDongResponseDto>>
    {
        public INsContractService _service;

        public FindPagingNsHopDong(INsContractService service)
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
