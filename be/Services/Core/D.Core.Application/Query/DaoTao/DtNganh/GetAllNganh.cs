using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.Nganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.DtNganh
{
    public class GetAllNganh
        : IQueryHandler<DtNganhRequestDto, PageResultDto<DtNganhResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllNganh(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtNganhResponseDto>> Handle(
            DtNganhRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtNganh(request);
        }
    }
}
