using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.ChuyenNganh
{
    public class DeleteChuyenNganh : ICommandHandler<DeleteDtChuyenNganhDto>
    {
        private readonly IDaoTaoService _service;

        public DeleteChuyenNganh(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(DeleteDtChuyenNganhDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteDtChuyenNganh(request.Id);
        }
    }
}
