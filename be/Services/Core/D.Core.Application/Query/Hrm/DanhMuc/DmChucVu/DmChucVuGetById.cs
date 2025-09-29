using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmChucVu
{
    public class DmChucVuGetById : IQueryHandler<DmChucVuGetByIdRequestDto, DmChucVuResponseDto>
    {
        public IDmChucVuService _dmChucvuService;
        public DmChucVuGetById(IDmChucVuService dmChucvuService)
        {
            _dmChucvuService = dmChucvuService;
        }

        public async Task<DmChucVuResponseDto> Handle(DmChucVuGetByIdRequestDto request, CancellationToken cancellationToken)
        {
            return await _dmChucvuService.GetByIdAsync(request.Id);
        }
    }
}
