using D.ApplicationBase;
using D.Core.Domain.Dtos.DaoTao.MonTienQuyet;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.DaoTao.MonTienQuyet
{
    public class CreateMonTienQuyet : ICommandHandler<CreateDtMonTienQuyetDto>
    {
        private readonly IDaoTaoService _service;

        public CreateMonTienQuyet(IDaoTaoService dtMonTienQuyetService)
        {
            _service = dtMonTienQuyetService;
        }

        public async Task Handle(CreateDtMonTienQuyetDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtMonTienQuyet(req);
        }
    }
}
