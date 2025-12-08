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
    public class UpdateChuongTrinhKhung : ICommandHandler<UpdateDtChuongTrinhKhungDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateChuongTrinhKhung(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtChuongTrinhKhungDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateDtChuongTrinhKhung(request);
        }
    }
}
