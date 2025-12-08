using D.ApplicationBase;
using D.ControllerBase;
using D.Core.Domain.Dtos.DaoTao.Nganh;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.Nganh
{
    public class DeleteNganh : ICommandHandler<DeleteDtNganhDto>
    {
        private readonly IDaoTaoService _service;

        public DeleteNganh(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(DeleteDtNganhDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteDtNganh(request.Id);
        }
    }
}