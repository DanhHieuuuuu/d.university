using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.ChuongTrinhKhung
{
    public class DeleteChuongTrinhKhung : ICommandHandler<DeleteDtChuongTrinhKhungDto>
    {
        private readonly IDaoTaoService _service;

        public DeleteChuongTrinhKhung(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(DeleteDtChuongTrinhKhungDto request, CancellationToken cancellationToken)
        {
            await _service.DeleteDtChuongTrinhKhung(request.Id);
        }
    }
}
