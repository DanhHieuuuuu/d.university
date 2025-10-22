using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmToBoMon
{
    public class DmToBoMonGetById
        : IQueryHandler<DmToBoMonGetByIdRequestDto, DmToBoMonResponseDto>
    {
        public IDmDanhMucService _service;

        public DmToBoMonGetById(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<DmToBoMonResponseDto> Handle(
            DmToBoMonGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDmToBoMonByIdAsync(request.Id);
        }
    }
}
