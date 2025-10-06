using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class CreateToBoMon : ICommandHandler<CreateDmChucVuDto>
    {
        private readonly IDmDanhMucService _service;

        public CreateToBoMon(IDmDanhMucService service)
        {
            _service = service;
        }

        public async Task Handle(CreateDmChucVuDto request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
