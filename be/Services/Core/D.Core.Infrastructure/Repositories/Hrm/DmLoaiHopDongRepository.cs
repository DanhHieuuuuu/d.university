using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmLoaiHopDongRepository : RepositoryBase<DmLoaiHopDong>, IDmLoaiHopDongRepository
    {
        public DmLoaiHopDongRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDmLoaiHopDongRepository : IRepositoryBase<DmLoaiHopDong> { }
}
