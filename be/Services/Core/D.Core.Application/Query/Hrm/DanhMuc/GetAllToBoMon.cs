using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllToBoMon
        : IQueryHandler<DmToBoMonRequestDto, PageResultDto<DmToBoMonResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllToBoMon(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmToBoMonResponseDto>> Handle(
            DmToBoMonRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmToBoMon(request);
        }
    }
}
