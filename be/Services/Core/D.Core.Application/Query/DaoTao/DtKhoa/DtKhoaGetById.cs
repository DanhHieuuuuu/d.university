using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.DtKhoa
{
    public class DtKhoaGetById
        : IQueryHandler<DtKhoaGetByIdRequestDto, DtKhoaResponseDto>
    {
        public IDaoTaoService _service;

        public DtKhoaGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtKhoaResponseDto> Handle(
            DtKhoaGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtKhoaById(request.Id);
        }
    }
}
