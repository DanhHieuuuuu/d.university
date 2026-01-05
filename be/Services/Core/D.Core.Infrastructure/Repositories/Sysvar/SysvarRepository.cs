using D.Core.Domain.Entities.Sysvar;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Sysvar
{
    public class SysvarRepository : RepositoryBase<SysVar>, ISysVarRepository
    {
        public SysvarRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface ISysVarRepository : IRepositoryBase<SysVar>
    {
    }
}
