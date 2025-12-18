using D.Core.Domain.Entities.Delegation.Incoming;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Delegation.Incoming
{
    public class SupporterRepository : RepositoryBase<Supporter>, ISupporterRepository
    {
        public SupporterRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
        public bool IsSupporterCodeExist(string supporterCode)
        {
            return TableNoTracking.Any(x => x.SupporterCode == supporterCode);
        }
    }

    public interface ISupporterRepository : IRepositoryBase<Supporter>
    {
        bool IsSupporterCodeExist(string supporterCode);
    }
}
