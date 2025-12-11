using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.ChuongTrinhKhungMon
{
    public class DeleteChuongTrinhKhungMon : ICommandHandler<DeleteDtChuongTrinhKhungMonDto>
    {
        private readonly IDaoTaoService _service;

        public DeleteChuongTrinhKhungMon(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(DeleteDtChuongTrinhKhungMonDto req, CancellationToken cancellationToken)
        {
            await _service.DeleteDtChuongTrinhKhungMon(req.Id);
        }
    }
}
