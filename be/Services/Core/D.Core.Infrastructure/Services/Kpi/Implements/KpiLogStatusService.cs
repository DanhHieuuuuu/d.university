using AutoMapper;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiLogStatusService : ServiceBase, IKpiLogStatusService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiLogStatusService(
            ILogger<KpiRoleService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
