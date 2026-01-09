using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllPhongBanByKpiRole
        : IQueryHandler<DmPhongBanByKpiRoleRequestDto, PageResultDto<DmPhongBanByKpiRoleResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllPhongBanByKpiRole(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmPhongBanByKpiRoleResponseDto>> Handle(
            DmPhongBanByKpiRoleRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmPhongBanByKpiRole(request);
        }
    }
}
