using D.Core.Domain.Entities.DaoTao;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.DaoTao
{
    public class DtQuyDinhThangDiemRepository : RepositoryBase<DtQuyDinhThangDiem>, IDtQuyDinhThangDiemRepository
    {
        public DtQuyDinhThangDiemRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }

    public interface IDtQuyDinhThangDiemRepository : IRepositoryBase<DtQuyDinhThangDiem>
    {
    }
}
