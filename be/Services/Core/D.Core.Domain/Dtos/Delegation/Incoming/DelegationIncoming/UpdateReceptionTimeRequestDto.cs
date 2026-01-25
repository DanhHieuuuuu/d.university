using D.DomainBase.Common; 
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming 
{ 
    public class UpdateReceptionTimesRequestDto: ICommand<List<UpdateReceptionTimeResponseDto>> 
    { 
        public List<UpdateReceptionTimeRequestDto> Items { get; set; } = new(); 
    } 
    public class UpdateReceptionTimeRequestDto 
    { 
        public int? Id { get; set; } 
        public int DelegationIncomingId { get; set; } 
        public TimeOnly StartDate { get; set; } 
        public TimeOnly EndDate { get; set; } 
        public DateOnly Date { get; set; } 
        public string? Content { get; set; } 
        public int TotalPerson { get; set; }  
        public string? Address { get; set; } 
    }   
      
     
} 
 