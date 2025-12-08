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
    public class CreateChuyenNganh : ICommandHandler<CreateDtChuyenNganhDto>
    {
        private readonly IDaoTaoService _service;

        public CreateChuyenNganh(IDaoTaoService dtChuyenNganhService)
        {
            _service = dtChuyenNganhService;
        }

        public async Task Handle(CreateDtChuyenNganhDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtChuyenNganh(req);
        }
    }
}
