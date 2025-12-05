using D.Core.Domain.Entities.Kpi;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Kpi
{
    public class KpiRoleRepository : RepositoryBase<KpiRole>, IKpiRoleRepository
    {
        public KpiRoleRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IKpiRoleRepository : IRepositoryBase<KpiRole>
    {
    }
}
