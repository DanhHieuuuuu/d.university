using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Infrastructure.Services.Hrm.Abstracts;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmKhoaHoc
{
    public class DmKhoaHocGetById
        : IQueryHandler<DmKhoaHocGetByIdRequestDto, DmKhoaHocResponseDto>
    {
        public IDmDanhMucService _service;

        public DmKhoaHocGetById(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<DmKhoaHocResponseDto> Handle(
            DmKhoaHocGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDmKhoaHocByIdAsync(request.Id);
        }
    }
}
