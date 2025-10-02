using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmGioiTinhRepository : RepositoryBase<DmGioiTinh>, IDmGioiTinhRepository
    {
        public DmGioiTinhRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDmGioiTinhRepository : IRepositoryBase<DmGioiTinh> { }
}
