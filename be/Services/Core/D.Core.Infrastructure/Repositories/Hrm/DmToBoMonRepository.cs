using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmToBoMonRepository : RepositoryBase<DmToBoMon>, IDmToBoMonRepository
    {
        public DmToBoMonRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public bool IsMaBoMonExist(string maBoMon)
        {
            return TableNoTracking.Any(x => x.MaBoMon == maBoMon);
        }
    }

    public interface IDmToBoMonRepository : IRepositoryBase<DmToBoMon>
    {
        bool IsMaBoMonExist(string maBoMon);
    }
}
