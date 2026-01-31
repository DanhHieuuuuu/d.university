using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D.DomainBase.Common;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class DeleteDepartmentSupportDto : ICommand
    {
        public int Id { get; set; }
    }
}
