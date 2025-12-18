using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class CreateSupporterRequestDto : ICommand<CreateSupporterResponseDto>
    {
    
        public int SupporterId { get; set; }

        public string? SupporterCode { get; set; } // mã người hỗ trợ

        public int DepartmentSupportId { get; set; } // phòng ban hỗ trợ 
    }
}
