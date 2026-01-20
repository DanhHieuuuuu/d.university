using D.DomainBase.Common; 
using D.DomainBase.Dto; 
using Microsoft.AspNetCore.Mvc; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks;  
 
namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging 
{ 
    public class FilterSupporterDto : FilterBaseDto, IQuery<PageResultDto<PageSupporterResultDto>> 
    { 
        [FromQuery(Name = "SupporterCode")] 
        public string? SupporterCode { get; set; } 
        [FromQuery(Name = "DepartmentSupportId")] 
        public int? DepartmentSupportId { get; set; } 
    } 
} 
 