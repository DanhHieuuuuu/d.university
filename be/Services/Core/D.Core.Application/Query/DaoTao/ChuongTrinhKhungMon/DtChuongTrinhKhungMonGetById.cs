using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.ChuongTrinhKhungMon
{
    public class DtChuongTrinhKhungMonGetById
        : IQueryHandler<DtChuongTrinhKhungMonGetByIdRequestDto, DtChuongTrinhKhungMonResponseDto>
    {
        public IDaoTaoService _service;

        public DtChuongTrinhKhungMonGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtChuongTrinhKhungMonResponseDto> Handle(
            DtChuongTrinhKhungMonGetByIdRequestDto req,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtChuongTrinhKhungMonById(req.Id);
        }
    }
}
