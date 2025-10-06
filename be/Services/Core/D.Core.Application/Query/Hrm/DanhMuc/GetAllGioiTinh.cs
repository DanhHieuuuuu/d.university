using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmGioiTinh;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllGioiTinh
        : IQueryHandler<DmGioiTinhRequestDto, PageResultDto<DmGioiTinhResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllGioiTinh(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<PageResultDto<DmGioiTinhResponseDto>> Handle(
            DmGioiTinhRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmGioiTinh(request);
        }
    }
}
