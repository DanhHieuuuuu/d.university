using Azure.Core;
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
    public class GetListTrangThai : IQueryHandler<ViewTrangThaiRequestDto,List<ViewTrangThaiResponseDto>>
    {
        private readonly IDelegationIncomingService _service;
        public GetListTrangThai (IDelegationIncomingService service)
        {
            _service = service;
        }
        public async Task<List<ViewTrangThaiResponseDto>> Handle (ViewTrangThaiRequestDto request, CancellationToken cancellationToken)
        {
            return _service.GetListTrangThai(request);
        }
    }
}
