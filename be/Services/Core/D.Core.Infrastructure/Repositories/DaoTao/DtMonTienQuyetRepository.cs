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
    public class DtMonTienQuyetRepository : RepositoryBase<DtMonTienQuyet>, IDtMonTienQuyetRepository
    {
        public DtMonTienQuyetRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public async Task<bool> IsRelationshipExistAsync(int monHocId, int monTienQuyetId)
        {
            return await TableNoTracking.AnyAsync(x => x.MonHocId == monHocId && x.MonTienQuyetId == monTienQuyetId);
        }
    }

    public interface IDtMonTienQuyetRepository : IRepositoryBase<DtMonTienQuyet>
    {
        Task<bool> IsRelationshipExistAsync(int monHocId, int monTienQuyetId);
    }
}
