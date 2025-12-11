using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.MonHoc
{
    public class DtMonHocGetById
        : IQueryHandler<DtMonHocGetByIdRequestDto, DtMonHocResponseDto>
    {
        public IDaoTaoService _service;

        public DtMonHocGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtMonHocResponseDto> Handle(
            DtMonHocGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtMonHocById(request.Id);
        }
    }
}
