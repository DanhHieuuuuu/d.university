using AutoMapper;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class DmPhongBanService : ServiceBase, IDmPhongBanService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DmPhongBanService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DmPhongBanResponseDto> GetByIdAsync(int id)
        {
            var entity =  _unitOfWork.iDmPhongBanRepository.FindById(id);
            if (entity == null) return null;
            return _mapper.Map<DmPhongBanResponseDto>(entity);
        }
    }
}
