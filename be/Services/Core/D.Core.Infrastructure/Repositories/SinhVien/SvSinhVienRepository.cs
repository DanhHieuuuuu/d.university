using D.Core.Domain.Entities.SinhVien;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Repositories.SinhVien
{
    public interface ISvSinhVienRepository : IRepositoryBase<SvSinhVien> { }

    public class SvSinhVienRepository : RepositoryBase<SvSinhVien>, ISvSinhVienRepository
    {
        public SvSinhVienRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }
    }
}
