using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class NsQuanHeGiaDinhRepository
        : RepositoryBase<NsQuanHeGiaDinh>,
            INsQuanHeGiaDinhRepository
    {
        public NsQuanHeGiaDinhRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface INsQuanHeGiaDinhRepository : IRepositoryBase<NsQuanHeGiaDinh> { }
}
