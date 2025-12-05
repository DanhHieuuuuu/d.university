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
    public class UpdateChuongTrinhKhungMon : ICommandHandler<UpdateDtChuongTrinhKhungMonDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateChuongTrinhKhungMon(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtChuongTrinhKhungMonDto req, CancellationToken cancellationToken)
        {
            await _service.UpdateDtChuongTrinhKhungMon(req);
        }
    }
}
