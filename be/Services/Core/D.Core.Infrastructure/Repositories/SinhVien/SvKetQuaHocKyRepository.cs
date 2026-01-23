using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public class SvKetQuaHocKyRepository : RepositoryBase<SvKetQuaHocKy>, ISvKetQuaHocKyRepository
    {
        public SvKetQuaHocKyRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface ISvKetQuaHocKyRepository : IRepositoryBase<SvKetQuaHocKy>
    {
    }
}
