using D.Core.Domain.Entities.Delegation.Incoming;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Delegation.Incoming
{
    public class DetailDelegationIncomingRepository : RepositoryBase<DetailDelegationIncoming>, IDetailDelegationIncomingRepository
    {
        public DetailDelegationIncomingRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDetailDelegationIncomingRepository : IRepositoryBase<DetailDelegationIncoming>
    {
    }
}
