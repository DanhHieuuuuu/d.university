using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class DepartmentSupportGetById : IQueryHandler<DetailDepartmentSupportRequestDto, DetailDepartmentSupportResponseDto>
    {
        private readonly IDepartmentSupportService _service;

        public DepartmentSupportGetById(IDepartmentSupportService service)
        {
            _service = service;
        }
        public async Task<DetailDepartmentSupportResponseDto> Handle(DetailDepartmentSupportRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdDepartmentSupport(request.DepartmentSupportId);
        }
    }
}
