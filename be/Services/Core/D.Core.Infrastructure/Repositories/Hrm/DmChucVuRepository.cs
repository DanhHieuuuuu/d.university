using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmChucVuRepository : RepositoryBase<DmChucVu>, IDmChucVuRepository
    {
        public DmChucVuRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDmChucVuRepository : IRepositoryBase<DmChucVu> { }
}
