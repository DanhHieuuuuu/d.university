using D.Core.Domain.Entities.DaoTao;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.DaoTao
{
    public class DtNganhRepository : RepositoryBase<DtNganh>, IDtNganhRepository
    {
        public DtNganhRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsMaNganhExistAsync(string maNganh)
        {
            return await TableNoTracking.AnyAsync(x => x.MaNganh == maNganh);
        }
    }

    public interface IDtNganhRepository : IRepositoryBase<DtNganh>
    {
        Task<bool> IsMaNganhExistAsync(string maNganh);
    }
}