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
    public class DtKhoaRepository : RepositoryBase<DtKhoa>, IDtKhoaRepository
    {
        public DtKhoaRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsMaKhoaExistAsync(string maKhoa)
        {
            return await TableNoTracking.AnyAsync(x => x.MaKhoa == maKhoa);
        }
    }

    public interface IDtKhoaRepository : IRepositoryBase<DtKhoa>
    {
        Task<bool> IsMaKhoaExistAsync(string maKhoa);
    }
}
