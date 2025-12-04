using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.DtKhoa
{
    public class GetAllKhoa
        : IQueryHandler<DtKhoaRequestDto, PageResultDto<DtKhoaResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllKhoa(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtKhoaResponseDto>> Handle(
            DtKhoaRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtKhoa(request);
        }
    }
}
