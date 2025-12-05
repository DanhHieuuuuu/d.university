using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.ChuongTrinhKhung
{
    public class GetAllChuongTrinhKhung
        : IQueryHandler<DtChuongTrinhKhungRequestDto, PageResultDto<DtChuongTrinhKhungResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllChuongTrinhKhung(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtChuongTrinhKhungResponseDto>> Handle(
            DtChuongTrinhKhungRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtChuongTrinhKhung(request);
        }
    }
}
