using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming.Paging;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts
{
    public interface IDelegationIncomingService
    {
        Task<CreateResponseDto> Create(CreateRequestDto dto);
        PageResultDto<PageDelegationIncomingResultDto> Paging(FilterDelegationIncomingDto dto);
        List<ViewPhongBanResponseDto> GetAllPhongBan(ViewPhongBanRequestDto dto);
        List<ViewTrangThaiResponseDto> GetListTrangThai(ViewTrangThaiRequestDto dto);
        Task<UpdateDelegationIncomingResponseDto> UpdateDelegationIncoming(UpdateDelegationIncomingRequestDto dto);
        void DeleteDoanVao(int id);
        Task<PageDelegationIncomingResultDto> GetByIdDelegationIncoming(int id);
        Task<DetailDelegationIncomingResponseDto> GetByIdDetailDelegation(int id);
        Task<ReceptionTimeResponseDto> GetByIdReceptionTime(int id);
    }
}
