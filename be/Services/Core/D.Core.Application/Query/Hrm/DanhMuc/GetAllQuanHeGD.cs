using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuanHeGiaDinh;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;

namespace D.Core.Application.Query.Hrm.DanhMuc
{
    public class GetAllQuanHeGD
        : IQueryHandler<DmQuanHeGiaDinhRequestDto, PageResultDto<DmQuanHeGiaDinhResponseDto>>
    {
        private readonly IDmDanhMucService _service;

        public GetAllQuanHeGD(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<DmQuanHeGiaDinhResponseDto>> Handle(
            DmQuanHeGiaDinhRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return _service.GetAllDmQuanHeGiaDinh(request);
        }
    }
}
