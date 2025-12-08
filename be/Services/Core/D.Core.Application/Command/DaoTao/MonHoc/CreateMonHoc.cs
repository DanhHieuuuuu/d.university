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
    public class CreateMonHoc : ICommandHandler<CreateDtMonHocDto>
    {
        private readonly IDaoTaoService _service;

        public CreateMonHoc(IDaoTaoService dtMonHocService)
        {
            _service = dtMonHocService;
        }

        public async Task Handle(CreateDtMonHocDto req, CancellationToken cancellationToken)
        {
            await _service.CreateDtMonHoc(req);
        }
    }
}
