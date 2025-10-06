using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllLoaiPhongBan
        : IQueryHandler<DmLoaiPhongBanRequestDto, PageResultDto<DmLoaiPhongBanResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllLoaiPhongBan(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<PageResultDto<DmLoaiPhongBanResponseDto>> Handle(
            DmLoaiPhongBanRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmLoaiPhongBan(request);
        }
    }
}
