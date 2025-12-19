using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class CreateSupporterRequestDto : ICommand<List<CreateSupporterResponseDto>>
    {
        public int DepartmentSupportId { get; set; }

        public List<SupporterItemDto> Supporters { get; set; } = new();
    }

    public class SupporterItemDto
    {
        public int SupporterId { get; set; }
        public string? SupporterCode { get; set; }
    }
}
