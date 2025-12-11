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
    public class UpdateMonHoc : ICommandHandler<UpdateDtMonHocDto>
    {
        private readonly IDaoTaoService _service;

        public UpdateMonHoc(IDaoTaoService daoTaoService)
        {
            _service = daoTaoService;
        }

        public async Task Handle(UpdateDtMonHocDto request, CancellationToken cancellationToken)
        {
            await _service.UpdateDtMonHoc(request);
        }
    }
}
