using AutoMapper;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
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
    public class DmKhoaService : ServiceBase, IDmKhoaService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DmKhoaService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DmKhoaResponseDto> GetByIdAsync(int id)
        {
            var entity = _unitOfWork.iDmKhoaRepository.FindById(id);
            if (entity == null) return null;
            return _mapper.Map<DmKhoaResponseDto>(entity);
        }
    }
}
