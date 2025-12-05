using D.Core.Domain.Entities.Kpi;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Kpi
{
    public class KpiTemplateRepository : RepositoryBase<KpiTemplate>, IKpiTemplateRepository
    {
        public KpiTemplateRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IKpiTemplateRepository : IRepositoryBase<KpiTemplate>
    {
    }
}
