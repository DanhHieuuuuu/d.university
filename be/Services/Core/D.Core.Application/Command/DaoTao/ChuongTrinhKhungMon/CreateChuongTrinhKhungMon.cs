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
    public class CreateChuongTrinhKhungMon : ICommandHandler<CreateDtChuongTrinhKhungMonDto>
    {
        private readonly IDaoTaoService _service;

        public CreateChuongTrinhKhungMon(IDaoTaoService dtChuongTrinhKhungMonService)
        {
            _service = dtChuongTrinhKhungMonService;
        }

        public async Task Handle(CreateDtChuongTrinhKhungMonDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtChuongTrinhKhungMon(req);
        }
    }
}
