using D.Core.Domain.Entities.Kpi;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Kpi
{
    public class KpiDonViRepository : RepositoryBase<KpiDonVi>, IKpiDonViRepository
    {
        public KpiDonViRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IKpiDonViRepository : IRepositoryBase<KpiDonVi>
    {
    }
}
