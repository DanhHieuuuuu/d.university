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
        public decimal SumTiLeByNhanSu(int idNhanSu, int? excludeId = null)
        {
            return TableNoTracking
                .Where(x => !x.Deleted && x.IdNhanSu == idNhanSu && (!excludeId.HasValue || x.Id != excludeId.Value))
                .Sum(x => x.TiLe ?? 0);
        }
    }

    public interface IKpiRoleRepository : IRepositoryBase<KpiRole>
    {
        decimal SumTiLeByNhanSu(int idNhanSu, int? excludeId = null);
    }
}
