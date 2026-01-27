using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public class SvDiemMonHocRepository : RepositoryBase<SvDiemMonHoc>, ISvDiemMonHocRepository
    {
        public SvDiemMonHocRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface ISvDiemMonHocRepository : IRepositoryBase<SvDiemMonHoc>
    {
    }
}
