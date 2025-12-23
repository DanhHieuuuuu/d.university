using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts
{
    public interface IDepartmentSupportService
    {
        Task<CreateDepartmentSupportResponseDto> CreateDepartmentSupport(CreateDepartmentSupportRequestDto dto);
        PageResultDto<PageDepartmentSupportResultDto> PagingDepartmentSupport(FilterDepartmentSupportDto dto);
        List<ViewDelegationIncomingResponseDto> GetAllDelegationIncoming(ViewDelegationIncomingRequestDto dto);
        Task<UpdateDepartmentSupportResponseDto> UpdateDepartmentSupport(UpdateDepartmentSupportRequestDto dto);
        Task<DetailDepartmentSupportResponseDto?> GetByIdDepartmentSupport(int id);
    }
}
