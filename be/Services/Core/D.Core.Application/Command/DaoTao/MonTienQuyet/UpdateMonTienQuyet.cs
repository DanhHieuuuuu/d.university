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
    public class UpdateMonTienQuyet : ICommandHandler<UpdateDtMonTienQuyetDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateMonTienQuyet(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtMonTienQuyetDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateDtMonTienQuyet(request);
        }
    }
}
