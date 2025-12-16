using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Delegation.Incoming
{
    public class InsertReceptionTimeLog : ICommandHandler<InsertReceptionTimeLogDto>
    {
        public ILogReceptionTimeService _service { get; set; }

        public InsertReceptionTimeLog(ILogReceptionTimeService service)
        {
            _service = service;
        }

        public async Task Handle(InsertReceptionTimeLogDto req, CancellationToken cancellationToken)
        {
            _service.InsertLogReceptionTime(req);
            return;
        }
    }
}
