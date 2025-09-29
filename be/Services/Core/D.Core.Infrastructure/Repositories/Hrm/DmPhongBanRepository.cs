using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmPhongBanRepository : RepositoryBase<DmPhongBan>, IDmPhongBanRepository
    {
        public DmPhongBanRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDmPhongBanRepository : IRepositoryBase<DmPhongBan> { }
}
