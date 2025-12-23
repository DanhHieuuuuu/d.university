using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class FindReceptionTimeLog : IQueryHandler<FindReceptionTimeLogDto, PageResultDto<ViewReceptionTimeLogDto>>
    {
        private readonly ILogReceptionTimeService _service;

        public FindReceptionTimeLog(ILogReceptionTimeService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<ViewReceptionTimeLogDto>> Handle(FindReceptionTimeLogDto request, CancellationToken cancellationToken)
        {
            return _service.FindLogReceptionTime(request);
        }

    }
}
