using D.Core.Domain.Entities.Hrm.NhanSu;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public interface INsQuaTrinhCongTacRepository : IRepositoryBase<NsQuaTrinhCongTac> { }

    public class NsQuaTrinhCongTacRepository
        : RepositoryBase<NsQuaTrinhCongTac>,
            INsQuaTrinhCongTacRepository
    {
        public NsQuaTrinhCongTacRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }
}
