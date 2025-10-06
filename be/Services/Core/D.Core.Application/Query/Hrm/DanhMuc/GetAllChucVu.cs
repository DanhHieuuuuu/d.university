using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllChucVu
        : IQueryHandler<DmChucVuRequestDto, PageResultDto<DmChucVuResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllChucVu(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<PageResultDto<DmChucVuResponseDto>> Handle(
            DmChucVuRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmChucVu(request);
        }
    }
}
