using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllKhoa
        : IQueryHandler<DmKhoaRequestDto, PageResultDto<DmKhoaResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllKhoa(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmKhoaResponseDto>> Handle(
            DmKhoaRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmKhoa(request);
        }
    }
}
