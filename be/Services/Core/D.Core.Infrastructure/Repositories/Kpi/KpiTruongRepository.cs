using D.Core.Domain.Entities.Kpi;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Kpi
{
    public class KpiTruongRepository : RepositoryBase<KpiTruong>, IKpiTruongRepository
    {
        public KpiTruongRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IKpiTruongRepository : IRepositoryBase<KpiTruong>
    {
    }
}
