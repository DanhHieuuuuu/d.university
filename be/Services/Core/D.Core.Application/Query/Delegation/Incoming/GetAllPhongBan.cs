using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class GetAllPhongBan : IQueryHandler<ViewPhongBanRequestDto,List<ViewPhongBanResponseDto>>  
    {
        private readonly IDelegationIncomingService _service;

        public GetAllPhongBan(IDelegationIncomingService service)
        {
            _service = service;
        }

        public async Task<List<ViewPhongBanResponseDto>> Handle(ViewPhongBanRequestDto request, CancellationToken cancellationToken)
        {
            return _service.GetAllPhongBan(request);
        }
    }
}
