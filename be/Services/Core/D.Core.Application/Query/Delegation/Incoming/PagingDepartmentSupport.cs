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
    public class PagingDepartmentSupport : IQueryHandler<FilterDepartmentSupportDto, PageResultDto<PageDepartmentSupportResultDto>>
    {
        private readonly IDepartmentSupportService _service;

        public PagingDepartmentSupport(IDepartmentSupportService service)
        {
            _service = service;
        }

        public async Task<PageResultDto<PageDepartmentSupportResultDto>> Handle(FilterDepartmentSupportDto request, CancellationToken cancellationToken)
        {
            return _service.PagingDepartmentSupport(request);
        }
    }
}
