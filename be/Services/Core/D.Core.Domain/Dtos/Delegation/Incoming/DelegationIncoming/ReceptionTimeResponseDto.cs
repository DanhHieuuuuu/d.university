using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 
 
namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class ReceptionTimeResponseDto 
    { 
        public int Id { get; set; }   
        public TimeOnly StartDate { get; set; } 
        public TimeOnly EndDate { get; set; } 
        public DateOnly Date { get; set; } 
        public string? Content { get; set; } 
        public int TotalPerson { get; set; } 
        public string? Address { get; set; } 
        public int DelegationIncomingId { get; set; } 
        public string? DelegationName { get; set; } 
        public string? DelegationCode { get; set; } 
        public List<PrepareResponseDto> Prepares { get; set; } = new(); 
    } 
    public class PrepareResponseDto 
    { 
        public int Id { get; set; }  
        public string? Name { get; set; } 
        public string? Description { get; set; }  
        public decimal Money { get; set; } 
    } 
} 
  