using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public class SvThongTinHocVuRepository : RepositoryBase<SvThongTinHocVu>, ISvThongTinHocVuRepository
    {
        public SvThongTinHocVuRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface ISvThongTinHocVuRepository : IRepositoryBase<SvThongTinHocVu>
    {
    }
}
