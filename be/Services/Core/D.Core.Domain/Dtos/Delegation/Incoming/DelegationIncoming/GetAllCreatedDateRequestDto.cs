using D.DomainBase.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming
{
    public class GetAllCreatedDateRequestDto
        : IRequest<List<DateOptionDto>>
    {
    }
}
