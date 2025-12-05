using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.MonHoc;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.MonHoc
{
    public class DeleteMonHoc : ICommandHandler<DeleteDtMonHocDto>
    {
        private readonly IDaoTaoService _service;

        public DeleteMonHoc(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(DeleteDtMonHocDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteDtMonHoc(request.Id);
        }
    }
}
