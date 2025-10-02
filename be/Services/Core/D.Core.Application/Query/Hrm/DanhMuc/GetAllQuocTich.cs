using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuocTich;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllQuocTich
        : IQueryHandler<DmQuocTichRequestDto, PageResultDto<DmQuocTichResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllQuocTich(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmQuocTichResponseDto>> Handle(
            DmQuocTichRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmQuocTich(request);
        }
    }
}
