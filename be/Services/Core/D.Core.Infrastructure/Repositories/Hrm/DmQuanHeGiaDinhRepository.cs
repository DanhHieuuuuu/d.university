using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmQuanHeGiaDinhRepository
        : RepositoryBase<DmQuanHeGiaDinh>,
            IDmQuanHeGiaDinhRepository
    {
        public DmQuanHeGiaDinhRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDmQuanHeGiaDinhRepository : IRepositoryBase<DmQuanHeGiaDinh> { }
}
