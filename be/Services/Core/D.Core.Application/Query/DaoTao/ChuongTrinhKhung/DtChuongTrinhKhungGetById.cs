using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.ChuongTrinhKhung
{
    public class DtChuongTrinhKhungGetById
        : IQueryHandler<DtChuongTrinhKhungGetByIdRequestDto, DtChuongTrinhKhungResponseDto>
    {
        public IDaoTaoService _service;

        public DtChuongTrinhKhungGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtChuongTrinhKhungResponseDto> Handle(
            DtChuongTrinhKhungGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtChuongTrinhKhungById(request.Id);
        }
    }
}
