using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmTonGiao;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllTonGiao
        : IQueryHandler<DmTonGiaoRequestDto, PageResultDto<DmTonGiaoResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllTonGiao(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmTonGiaoResponseDto>> Handle(
            DmTonGiaoRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmTonGiao(request);
        }
    }
}
