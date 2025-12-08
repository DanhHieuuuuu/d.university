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
    public class DtMonHocRepository : RepositoryBase<DtMonHoc>, IDtMonHocRepository
    {
        public DtMonHocRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsMaMonHocExistAsync(string maMonHoc)
        {
            return await TableNoTracking.AnyAsync(x => x.MaMonHoc == maMonHoc);
        }
    }

    public interface IDtMonHocRepository : IRepositoryBase<DtMonHoc>
    {
        Task<bool> IsMaMonHocExistAsync(string maMonHoc);
    }
}
