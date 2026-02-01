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
        public bool IsCodeExist(string code, int delegationIncomingId, int? excludeId)
        {
            return TableNoTracking.Any(x =>
                x.Code == code
                && x.DelegationIncomingId == delegationIncomingId
                && !x.Deleted
                && (!excludeId.HasValue || x.Id != excludeId.Value)
            );
        }

    }

    public interface IDetailDelegationIncomingRepository : IRepositoryBase<DetailDelegationIncoming>
    {
        bool IsCodeExist(string code, int delegationIncomingId, int? excludeId);
    }
}
