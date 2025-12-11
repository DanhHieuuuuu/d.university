using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.Nganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.Nganh
{
    public class CreateNganh : ICommandHandler<CreateDtNganhDto>
    {
        private readonly IDaoTaoService _service;

        public CreateNganh(IDaoTaoService dtNganhService)
        {
            _service = dtNganhService;
        }

        public async Task Handle(CreateDtNganhDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtNganh(req);
        }
    }
}
