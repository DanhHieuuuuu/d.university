using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.ChuongTrinhKhungMon
{
    public class GetAllChuongTrinhKhungMon
        : IQueryHandler<DtChuongTrinhKhungMonRequestDto, PageResultDto<DtChuongTrinhKhungMonResponseDto>>
    {
        private readonly IDaoTaoService _service;

        public GetAllChuongTrinhKhungMon(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<PageResultDto<DtChuongTrinhKhungMonResponseDto>> Handle(
            DtChuongTrinhKhungMonRequestDto req,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetAllDtChuongTrinhKhungMon(req);
        }
    }
}
