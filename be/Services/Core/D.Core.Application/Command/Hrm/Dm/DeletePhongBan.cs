using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Hrm.Dm
{
    public class DeletePhongBan : ICommandHandler<DeleteDmPhongBanDto>
    {
        private readonly IDmDanhMucService _service;

        public DeletePhongBan(IDmDanhMucService dmDanhMucService)
        {
            _service = dmDanhMucService;
        }

        public async Task Handle(DeleteDmPhongBanDto request, CancellationToken cancellationToken)
        {
            _service.DeleteDmPhongBan(request.Id);
            return;
        }
    }
}
