using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class UpdateStatusRequestDto : ICommand
    {
        public int IdDelegation { get; set; }
        public int OldStatus { get; set; }

        /// <summary>
        /// Action thực hiện: upgrade; cancel
        /// </summary>
        public string Action { get; set; }
    }
}
