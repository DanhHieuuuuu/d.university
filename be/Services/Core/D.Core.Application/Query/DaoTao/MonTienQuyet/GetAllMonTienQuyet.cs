using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.MonTienQuyet;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.MonTienQuyet
{
    public class GetAllMonTienQuyet
        : IQueryHandler<DtMonTienQuyetRequestDto, PageResultDto<DtMonTienQuyetResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllMonTienQuyet(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtMonTienQuyetResponseDto>> Handle(
            DtMonTienQuyetRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtMonTienQuyet(request);
        }
    }
}
