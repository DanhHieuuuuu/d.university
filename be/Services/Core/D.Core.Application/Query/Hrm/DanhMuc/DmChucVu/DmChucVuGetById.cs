using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmChucVu
{
    public class DmChucVuGetById : IQueryHandler<DmChucVuGetByIdRequestDto, DmChucVuResponseDto>
    {
        public IDmDanhMucService _service;

        public DmChucVuGetById(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<DmChucVuResponseDto> Handle(
            DmChucVuGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDmChucVuByIdAsync(request.Id);
        }
    }
}
