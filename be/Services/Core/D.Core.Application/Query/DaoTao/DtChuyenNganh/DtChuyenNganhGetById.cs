using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.DtChuyenNganh
{
    public class DtChuyenNganhGetById
        : IQueryHandler<DtChuyenNganhGetByIdRequestDto, DtChuyenNganhResponseDto>
    {
        public IDaoTaoService _service;

        public DtChuyenNganhGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtChuyenNganhResponseDto> Handle(
            DtChuyenNganhGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtChuyenNganhById(request.Id);
        }
    }
}
