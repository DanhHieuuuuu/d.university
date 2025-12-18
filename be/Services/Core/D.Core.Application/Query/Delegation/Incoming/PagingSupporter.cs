using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class PagingSupporter : IQueryHandler<FilterSupporterDto, PageResultDto<PageSupporterResultDto>>
    {
        private readonly ISupporterService _service;

        public PagingSupporter(ISupporterService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<PageSupporterResultDto>> Handle(FilterSupporterDto request, CancellationToken cancellationToken)
        {
            return _service.PagingSupporter(request);
        }
    }
}
