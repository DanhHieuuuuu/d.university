using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmLoaiHopDong;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllLoaiHopDong
        : IQueryHandler<DmLoaiHopDongRequestDto, PageResultDto<DmLoaiHopDongResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllLoaiHopDong(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmLoaiHopDongResponseDto>> Handle(
            DmLoaiHopDongRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmLoaiHopDong(request);
        }
    }
}
