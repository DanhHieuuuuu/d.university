using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public interface INsHopDongRepository : IRepositoryBase<NsHopDong>
    {
        public bool IsSoHopDongExist(string soHopDong);
    }

    public class NsHopDongRepository : RepositoryBase<NsHopDong>, INsHopDongRepository
    {
        public NsHopDongRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public bool IsSoHopDongExist(string soHopDong)
        {
            return TableNoTracking.Any(x => x.SoHopDong!.ToLower() == soHopDong.ToLower());
        }
    }
}
