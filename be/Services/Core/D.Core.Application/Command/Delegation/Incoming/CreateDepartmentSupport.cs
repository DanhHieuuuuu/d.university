using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Delegation.Incoming
{
    public class CreateDepartmentSupport: ICommandHandler<CreateDepartmentSupportRequestDto, List<CreateDepartmentSupportResponseDto>>
    {
        private readonly IDepartmentSupportService _service;

        public CreateDepartmentSupport(IDepartmentSupportService service)
        {
            _service = service;
        }

        public async Task<List<CreateDepartmentSupportResponseDto>> Handle(
            CreateDepartmentSupportRequestDto request,
            CancellationToken cancellationToken
        )
        {
            return await _service.CreateDepartmentSupport(request);
        }
    }
}
