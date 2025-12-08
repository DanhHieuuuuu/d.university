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
    public class UpdateNganh : ICommandHandler<UpdateDtNganhDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateNganh(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtNganhDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateDtNganh(request);
        }
    }
}
