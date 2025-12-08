using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.MonTienQuyet;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.DaoTao.MonTienQuyet
{
    public class DtMonTienQuyetGetById
        : IQueryHandler<DtMonTienQuyetGetByIdRequestDto, DtMonTienQuyetResponseDto>
    {
        public IDaoTaoService _service;

        public DtMonTienQuyetGetById(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task<DtMonTienQuyetResponseDto> Handle(
            DtMonTienQuyetGetByIdRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.GetDtMonTienQuyetById(request.Id);
        }
    }
}
