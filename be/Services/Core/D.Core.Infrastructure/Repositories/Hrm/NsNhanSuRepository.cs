using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public interface INsNhanSuRepository : IRepositoryBase<NsNhanSu> { }

    public class NsNhanSuRepository : RepositoryBase<NsNhanSu>, INsNhanSuRepository
    {
        public NsNhanSuRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }
}
