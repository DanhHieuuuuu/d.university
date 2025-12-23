using D.ApplicationBase;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Command.Delegation.Incoming
{
    public class UpdateDepartmentSupport : ICommandHandler<UpdateDepartmentSupportRequestDto, UpdateDepartmentSupportResponseDto>
    {
        private readonly IDepartmentSupportService _service;

        public UpdateDepartmentSupport(IDepartmentSupportService service)
        {
            _service = service;
        }

        public async Task<UpdateDepartmentSupportResponseDto> Handle(UpdateDepartmentSupportRequestDto request, CancellationToken cancellationToken)
        {
            return await _service.UpdateDepartmentSupport(request);
        }

    }
}
