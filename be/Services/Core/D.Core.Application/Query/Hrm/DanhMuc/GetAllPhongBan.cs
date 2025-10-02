using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllPhongBan
        : IQueryHandler<DmPhongBanRequestDto, PageResultDto<DmPhongBanResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllPhongBan(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmPhongBanResponseDto>> Handle(
            DmPhongBanRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmPhongBan(request);
        }
    }
}
