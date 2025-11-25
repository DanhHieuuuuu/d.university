using D.Core.Domain.Entities.Delegation.Incoming;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Delegation.Incoming
{
    public class LogStatusRepository : RepositoryBase<LogStatus>, ILogStatusRepository
    {
        public LogStatusRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface ILogStatusRepository : IRepositoryBase<LogStatus>
    {
    }
}
