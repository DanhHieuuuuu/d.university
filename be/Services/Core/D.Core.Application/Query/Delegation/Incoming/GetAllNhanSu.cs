using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Delegation.Incoming
{
    public class GetAllNhanSu : IQueryHandler<ViewNhanSuRequestDto, List<ViewNhanSuResponseDto>>
    {
        private readonly IDelegationIncomingService _service;

        public GetAllNhanSu(IDelegationIncomingService service)
        {
            _service = service;
        }

        public async Task<List<ViewNhanSuResponseDto>> Handle(ViewNhanSuRequestDto request, CancellationToken cancellationToken)
        {
            return _service.GetAllNhanSu(request);
        }
    }
}
