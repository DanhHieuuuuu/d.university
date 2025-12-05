using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.DtChuyenNganh
{
    public class GetAllChuyenNganh
        : IQueryHandler<DtChuyenNganhRequestDto, PageResultDto<DtChuyenNganhResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllChuyenNganh(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtChuyenNganhResponseDto>> Handle(
            DtChuyenNganhRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtChuyenNganh(request);
        }
    }
}
