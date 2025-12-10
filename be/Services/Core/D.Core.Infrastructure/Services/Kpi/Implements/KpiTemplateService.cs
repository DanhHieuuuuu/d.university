using AutoMapper;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiTemplateService : ServiceBase, IKpiTemplateService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiTemplateService(
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
