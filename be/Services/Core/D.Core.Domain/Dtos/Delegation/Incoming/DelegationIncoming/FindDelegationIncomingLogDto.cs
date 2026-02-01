using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.DomainBase.Common;
using D.DomainBase.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class FindDelegationIncomingLogDto : FilterBaseDto, IQuery<PageResultDto<ViewDelegationIncomingLogDto>>
    {
        [FromQuery(Name = "CreatedByName")]
        public string? CreatedByName { get; set; }
        [FromQuery(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }
        [FromQuery(Name = "StartDate")]
        public DateTime? StartDate { get; set; }

        [FromQuery(Name = "EndDate")]
        public DateTime? EndDate { get; set; }
    }
}
