using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmKhoaHocRepository : RepositoryBase<DmKhoaHoc>, IDmKhoaHocRepository
    {
        public DmKhoaHocRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public bool IsMaKhoaHocExist(string maKhoaHoc)
        {
            return TableNoTracking.Any(x => x.MaKhoaHoc == maKhoaHoc);
        }
    }

    public interface IDmKhoaHocRepository : IRepositoryBase<DmKhoaHoc>
    {
        bool IsMaKhoaHocExist(string maKhoaHoc);
    }
}
