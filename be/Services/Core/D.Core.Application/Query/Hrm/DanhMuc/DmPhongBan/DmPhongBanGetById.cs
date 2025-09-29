using D.ApplicationBase;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Application.Query.Hrm.DanhMuc.DmPhongBan
{
    public class DmPhongBanGetById : IQueryHandler<DmPhongBanGetByIdRequestDto, DmPhongBanResponseDto>
    {
        public IDmPhongBanService _dmPhongbanService;
        public DmPhongBanGetById (IDmPhongBanService dmPhongbanService)
        {
            _dmPhongbanService = dmPhongbanService;
        }

        public async Task<DmPhongBanResponseDto> Handle(DmPhongBanGetByIdRequestDto request, CancellationToken cancellationToken)
        {
            return await _dmPhongbanService.GetByIdAsync(request.Id);
        }
    }
}
