using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging 
{
    public class PageSupporterResultDto 
    { 
        public int Id { get; set; }                  
        public int SupporterId { get; set; }        
        public string? SupporterName { get; set; } 
        public string? SupporterCode { get; set; }     
        public int DepartmentSupportId { get; set; }  

    } 
} 
