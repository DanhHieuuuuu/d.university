using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanGetById
        : IQueryHandler<DmPhongBanGetByIdRequestDto, DmPhongBanResponseDto>
    {
        public IDmDanhMucService _service;

        public DmPhongBanGetById(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<DmPhongBanResponseDto> Handle(
            DmPhongBanGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDmPhongBanByIdAsync(request.Id);
        }
    }
}
