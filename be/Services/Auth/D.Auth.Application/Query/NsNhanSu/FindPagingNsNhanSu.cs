using D.ApplicationBase;
using D.Auth.Domain.Dtos;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Application.Query.NsNhanSu
{
    public class FindPagingNsNhanSu : IQueryHandler<NsNhanSuRequestDto, PageResultDto<NsNhanSuResponseDto>>
    {
        public INsNhanSuService _nsNhanSuService;
        public FindPagingNsNhanSu(INsNhanSuService nsNhanSuService)
        {
            _nsNhanSuService = nsNhanSuService;
        }
        public async Task<PageResultDto<NsNhanSuResponseDto>> Handle(NsNhanSuRequestDto request, CancellationToken cancellationToken)
        {
            return _nsNhanSuService.FindPagingNsNhanSu(request);
        }
    }
}
