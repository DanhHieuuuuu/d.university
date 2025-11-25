using D.DomainBase.Common;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging
{
    /// <summary>
    /// Filter của paging đoàn vào
    /// </summary>
    public class FilterDelegationIncomingDto : FilterBaseDto, IQuery<PageResultDto<PageDelegationIncomingResultDto>>
    {
    }
}
