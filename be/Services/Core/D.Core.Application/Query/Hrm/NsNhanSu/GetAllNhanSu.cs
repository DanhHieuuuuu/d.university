using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    internal class GetAllNhanSu : IQueryHandler<NsNhanSuGetAllRequestDto, PageResultDto<NsNhanSuGetAllResponseDto>>
    {
        public INsNhanSuService _nsNhanSuService;

        public GetAllNhanSu(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<PageResultDto<NsNhanSuGetAllResponseDto>> Handle(
            NsNhanSuGetAllRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.GetAllNhanSu(request);
        }
    }
}
