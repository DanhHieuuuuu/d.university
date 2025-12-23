using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdateDepartmentSupportRequestDto : ICommand<UpdateDepartmentSupportResponseDto>
    {
        public int DepartmentSupportId { get; set; }
        public int DelegationIncomingId { get; set; }

        public string? Content { get; set; }

        // Danh sách nhân sự hỗ trợ
        public List<UpdateSupporterItemDto> Supporters { get; set; } = new();

    }
}
