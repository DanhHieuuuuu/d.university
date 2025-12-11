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
    public class DeleteKhoa : ICommandHandler<DeleteDtKhoaDto>
    {
        private readonly IDaoTaoService _service;

        public DeleteKhoa(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(DeleteDtKhoaDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteDtKhoa(request.Id);
        }
    }
}
