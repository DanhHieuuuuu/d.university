using AutoMapper;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
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
    public class DmChucVuService : ServiceBase, IDmChucVuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DmChucVuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DmChucVuResponseDto> GetByIdAsync(int id)
        {
            var entity = _unitOfWork.iDmChucVuRepository.FindById(id);
            if (entity == null) return null;
            return _mapper.Map<DmChucVuResponseDto>(entity);
        }
    }
}
