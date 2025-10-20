using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.Hrm
{
    public class DmKhoaRepository : RepositoryBase<DmKhoa>, IDmKhoaRepository
    {
        public DmKhoaRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public bool IsMaKhoaExist(string maKhoa)
        {
            return TableNoTracking.Any(x => x.MaKhoa == maKhoa);
        }
    }

    public interface IDmKhoaRepository : IRepositoryBase<DmKhoa>
    {
        bool IsMaKhoaExist(string maKhoa);
    }
}
