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

        public bool IsMaPhongBanExist(string maPhongBan)
        {
            return TableNoTracking.Any(x => x.MaPhongBan == maPhongBan);
        }
    }

    public interface IDmPhongBanRepository : IRepositoryBase<DmPhongBan>
    {
        bool IsMaPhongBanExist(string maPhongBan);
    }
}
