using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class FindPagingNsNhanSu
        : IQueryHandler<NsNhanSuRequestDto, PageResultDto<NsNhanSuResponseDto>>
    {
        public INsNhanSuService _nsNhanSuService;

        public FindPagingNsNhanSu(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<PageResultDto<NsNhanSuResponseDto>> Handle(
            NsNhanSuRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.FindPagingNsNhanSu(request);
        }
    }
}
