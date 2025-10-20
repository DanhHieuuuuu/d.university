using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmKhoa
{
    public class DmKhoaGetById
        : IQueryHandler<DmKhoaGetByIdRequestDto, DmKhoaResponseDto>
    {
        public IDmDanhMucService _service;

        public DmKhoaGetById(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task<DmKhoaResponseDto> Handle(
            DmKhoaGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDmKhoaByIdAsync(request.Id);
        }
    }
}
