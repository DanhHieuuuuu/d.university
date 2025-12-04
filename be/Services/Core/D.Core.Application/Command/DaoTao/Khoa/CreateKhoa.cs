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
    public class CreateKhoa : ICommandHandler<CreateDtKhoaDto>
    {
        private readonly IDaoTaoService _service;

        public CreateKhoa(IDaoTaoService dtKhoaService)
        {
            _service = dtKhoaService;
        }

        public async Task Handle(CreateDtKhoaDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtKhoa(req);
        }
    }
}
