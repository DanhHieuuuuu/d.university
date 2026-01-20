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
    public interface ISupporterService  
    { 
        Task<List<CreateSupporterResponseDto>> CreateSupporter(CreateSupporterRequestDto dto); 
        PageResultDto<PageSupporterResultDto> PagingSupporter(FilterSupporterDto dto); 
    } 
} 
