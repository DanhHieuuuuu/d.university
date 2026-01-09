using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.NsNhanSu
{
    public class GetAllNhanSuByKpiRole : IQueryHandler<NsNhanSuByKpiRoleRequestDto, PageResultDto<NsNhanSuByKpiRoleResponseDto>>
    {
        public INsNhanSuService _nsNhanSuService;

        public GetAllNhanSuByKpiRole(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }

        public async Task<PageResultDto<NsNhanSuByKpiRoleResponseDto>> Handle(
            NsNhanSuByKpiRoleRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _nsNhanSuService.GetAllNhanSuByKpiRole(request);
        }
    }
}
