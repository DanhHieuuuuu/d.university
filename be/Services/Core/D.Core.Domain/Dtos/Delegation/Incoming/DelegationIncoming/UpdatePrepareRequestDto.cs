using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdatePrepareRequestDto : ICommand<List<UpdatePrepareResponseDto>>
    {
        public int ReceptionTimeId { get; set; }
        public List<UpdatePrepareItemDto> Items { get; set; } = new();
    }

    public class UpdatePrepareItemDto
    {
        public int Id { get; set; }    
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Money { get; set; }
    }
}
