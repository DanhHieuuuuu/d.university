using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class ReceptionTimeGetById : IQueryHandler<ReceptionTimeRequestDto, ReceptionTimeResponseDto>
    {
        private readonly IReceptionTimeService _service;

        public ReceptionTimeGetById(IReceptionTimeService service)
        {
            _service = service;
        }
        public async Task<ReceptionTimeResponseDto> Handle(ReceptionTimeRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdReceptionTime(request.Id);
        }
    }
}
