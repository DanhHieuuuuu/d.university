using D.Core.Domain.Dtos.Hrm.NhanSu; 
using D.DomainBase.Common; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming 
{ 
    public class CreateReceptionTimeListRequestDto : ICommand<List<CreateReceptionTimeResponseDto>> 
         
    { 
        public List<CreateReceptionTimeRequestDto> Items { get; set; } = new(); 
    } 
    public class CreateReceptionTimeRequestDto 
    { 
        public TimeOnly StartDate { get; set; } 
        public TimeOnly EndDate { get; set; } 
        public DateOnly Date { get; set; } 
        public string? Content { get; set; } 
        public int TotalPerson { get; set; } 
        public string? Address { get; set; }  
        public int DelegationIncomingId { get; set; } 
         
    } 
} 
