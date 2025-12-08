using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.MonHoc
{
    public class GetAllMonHoc
        : IQueryHandler<DtMonHocRequestDto, PageResultDto<DtMonHocResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllMonHoc(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtMonHocResponseDto>> Handle(
            DtMonHocRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtMonHoc(request);
        }
    }
}
