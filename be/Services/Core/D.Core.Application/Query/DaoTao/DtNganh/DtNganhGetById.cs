using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.Nganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.DtNganh
{
    public class DtNganhGetById
        : IQueryHandler<DtNganhGetByIdRequestDto, DtNganhResponseDto>
    {
        public IDaoTaoService _service;

        public DtNganhGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtNganhResponseDto> Handle(
            DtNganhGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtNganhById(request.Id);
        }
    }
}
