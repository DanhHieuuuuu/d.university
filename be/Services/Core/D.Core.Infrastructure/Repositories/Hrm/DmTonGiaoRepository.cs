using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmTonGiaoRepository : RepositoryBase<DmTonGiao>, IDmTonGiaoRepository
    {
        public DmTonGiaoRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDmTonGiaoRepository : IRepositoryBase<DmTonGiao> { }
}
