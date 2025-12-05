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
    public class UpdateChuyenNganh : ICommandHandler<UpdateDtChuyenNganhDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateChuyenNganh(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtChuyenNganhDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateDtChuyenNganh(request);
        }
    }
}
