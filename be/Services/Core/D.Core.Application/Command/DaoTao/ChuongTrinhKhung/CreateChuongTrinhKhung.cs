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
    public class CreateChuongTrinhKhung : ICommandHandler<CreateDtChuongTrinhKhungDto>
    {
        private readonly IDaoTaoService _service;

        public CreateChuongTrinhKhung(IDaoTaoService dtChuongTrinhKhungService)
        {
            _service = dtChuongTrinhKhungService;
        }

        public async Task Handle(CreateDtChuongTrinhKhungDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtChuongTrinhKhung(req);
        }
    }
}
