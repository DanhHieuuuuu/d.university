using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmDanToc;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllDanToc
        : IQueryHandler<DmDanTocRequestDto, PageResultDto<DmDanTocResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllDanToc(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<PageResultDto<DmDanTocResponseDto>> Handle(
            DmDanTocRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmDanToc(request);
        }
    }
}
