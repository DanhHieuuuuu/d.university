using D.ControllerBase;
using D.Core.Domain.Dtos.DaoTao.ChuyenNganh;
using D.Core.Domain.Entities.DaoTao;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.DaoTao
{
    public class DtChuyenNganhRepository : RepositoryBase<DtChuyenNganh>, IDtChuyenNganhRepository
    {
        public DtChuyenNganhRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsMaChuyenNganhExistAsync(string maChuyenNganh)
        {
            return await TableNoTracking.AnyAsync(x => x.MaChuyenNganh == maChuyenNganh);
        }
    }

    public interface IDtChuyenNganhRepository : IRepositoryBase<DtChuyenNganh>
    {
        Task<bool> IsMaChuyenNganhExistAsync(string maChuyenNganh);
    }
}
