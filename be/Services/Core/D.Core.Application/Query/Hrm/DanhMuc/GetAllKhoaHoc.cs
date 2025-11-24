using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllKhoaHoc
        : IQueryHandler<DmKhoaHocRequestDto, PageResultDto<DmKhoaHocResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllKhoaHoc(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmKhoaHocResponseDto>> Handle(
            DmKhoaHocRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmKhoaHoc(request);
        }
    }
}
