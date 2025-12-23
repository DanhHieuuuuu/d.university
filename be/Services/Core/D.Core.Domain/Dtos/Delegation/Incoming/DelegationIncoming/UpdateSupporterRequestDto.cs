using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdateSupporterRequestDto : ICommand<UpdateSupporterResponseDto>
    {
        public int DepartmentSupportId { get; set; }
        public List<UpdateSupporterItemDto>? Supporters { get; set; }
    }
    public class UpdateSupporterItemDto
    {
        public int SupporterId { get; set; } 
        public string? SupporterCode { get; set; }
    }
}
