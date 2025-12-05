using D.Core.Domain.Entities.DaoTao;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.DaoTao
{
    public class DtChuongTrinhKhungRepository : RepositoryBase<DtChuongTrinhKhung>, IDtChuongTrinhKhungRepository
    {
        public DtChuongTrinhKhungRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsMaChuongTrinhKhungExistAsync(string maChuongTrinhKhung)
        {
            return await TableNoTracking.AnyAsync(x => x.MaChuongTrinhKhung == maChuongTrinhKhung);
        }
    }

    public interface IDtChuongTrinhKhungRepository : IRepositoryBase<DtChuongTrinhKhung>
    {
        Task<bool> IsMaChuongTrinhKhungExistAsync(string maChuongTrinhKhung);
    }
}
