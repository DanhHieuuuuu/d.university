using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public interface INsQuyetDinhLogRepository : IRepositoryBase<NsQuyetDinhLog> { }

    public class NsQuyetDinhLogRepository
        : RepositoryBase<NsQuyetDinhLog>,
            INsQuyetDinhLogRepository
    {
        public NsQuyetDinhLogRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }
}
