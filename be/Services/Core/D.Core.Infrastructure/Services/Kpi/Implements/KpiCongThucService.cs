using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiCongThuc;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiCongThucService : ServiceBase, IKpiCongThucService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiCongThucService(
            ILogger<KpiCongThucService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<KpiCongThucDto>> GetAllKpiCongThuc(GetCongThucRequestDto dto)
        {
            var query = await _unitOfWork.iKpiCongThucRepository.TableNoTracking
                .Where(x => x.isActive == 1).ToListAsync(); 
            return _mapper.Map<List<KpiCongThucDto>>(query);
        }
    }
}
