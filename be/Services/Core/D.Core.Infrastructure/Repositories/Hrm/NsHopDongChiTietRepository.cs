using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class NsHopDongChiTietRepository
        : RepositoryBase<NsHopDongChiTiet>,
            INsHopDongChiTietRepository
    {
        public NsHopDongChiTietRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface INsHopDongChiTietRepository : IRepositoryBase<NsHopDongChiTiet> { }
}
