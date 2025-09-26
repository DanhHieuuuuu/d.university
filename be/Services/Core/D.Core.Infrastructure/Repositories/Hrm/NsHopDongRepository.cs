using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class NsHopDongRepository : RepositoryBase<NsHopDong>, INsHopDongRepository
    {
        public NsHopDongRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface INsHopDongRepository : IRepositoryBase<NsHopDong> { }
}
