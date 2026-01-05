using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public interface INsQuyetDinhRepository : IRepositoryBase<NsQuyetDinh> { }

    public class NsQuyetDinhRepository : RepositoryBase<NsQuyetDinh>, INsQuyetDinhRepository
    {
        public NsQuyetDinhRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }
}
