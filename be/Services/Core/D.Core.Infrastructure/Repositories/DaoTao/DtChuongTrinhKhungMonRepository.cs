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
    public class DtChuongTrinhKhungMonRepository : RepositoryBase<DtChuongTrinhKhungMon>, IDtChuongTrinhKhungMonRepository
    {
        public DtChuongTrinhKhungMonRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsExistAsync(int ctkId, int monHocId)
        {
            return await TableNoTracking.AnyAsync(x => x.ChuongTrinhKhungId == ctkId && x.MonHocId == monHocId);
        }
    }

    public interface IDtChuongTrinhKhungMonRepository : IRepositoryBase<DtChuongTrinhKhungMon>
    {
        Task<bool> IsExistAsync(int ctkId, int monHocId);
    }
}
