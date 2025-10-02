using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class NsToBoMonRepository : RepositoryBase<NsToBoMon>, INsToBoMonRepository
    {
        public NsToBoMonRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface INsToBoMonRepository : IRepositoryBase<NsToBoMon> { }
}
