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
    public class DeleteDepartmentSupport : ICommandHandler<DeleteDepartmentSupportDto>
    {
        private readonly IDepartmentSupportService _service;
        public DeleteDepartmentSupport(IDepartmentSupportService service)
        {
            _service = service;
        }
        public async Task Handle(DeleteDepartmentSupportDto request, CancellationToken cancellationToken)
        {
            _service.DeleteDepartmentSupport(request.Id);
        }


    }
}
