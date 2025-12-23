using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Delegation.Incoming
{
    public class InsertDelegationIncomingLog : ICommandHandler<InsertDelegationIncomingLogDto>
    {
        public ILogStatusService _service { get; set; }

        public InsertDelegationIncomingLog(ILogStatusService service)
        {
            _service = service;
        }

        public async Task Handle(InsertDelegationIncomingLogDto req, CancellationToken cancellationToken)
        {
            _service.InsertLog(req);
            return;
        }
    }
}
