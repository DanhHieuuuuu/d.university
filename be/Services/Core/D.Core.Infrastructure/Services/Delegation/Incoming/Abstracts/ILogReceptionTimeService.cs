using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts
{
    public interface ILogReceptionTimeService
    {
        PageResultDto<ViewReceptionTimeLogDto> FindLogReceptionTime(FindReceptionTimeLogDto dto);
        void InsertLogReceptionTime(InsertReceptionTimeLogDto dto);
        List<DateOptionDto> GetAllCreatedDate();

    }
}
