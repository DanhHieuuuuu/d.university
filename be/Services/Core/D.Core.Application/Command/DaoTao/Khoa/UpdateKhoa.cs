using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.Khoa;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.Khoa
{
    public class UpdateKhoa : ICommandHandler<UpdateDtKhoaDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateKhoa(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtKhoaDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateDtKhoa(request);
        }
    }
}
