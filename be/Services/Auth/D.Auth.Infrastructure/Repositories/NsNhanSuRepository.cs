using d.Shared.Permission.Error;
using D.Auth.Domain.Entities;
using D.ControllerBase.Exceptions;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Repositories
{
    public class NsNhanSuRepository : RepositoryBase<NsNhanSu>, INsNhanSuRepository
    {
        public NsNhanSuRepository(IDbContext dbContext, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
        }

        public NsNhanSu FindByMaNhanSu(string MaNhanSu)
        {
            var ns = TableNoTracking.FirstOrDefault(x => x.MaNhanSu == MaNhanSu);
            if (ns == null)
                throw new UserFriendlyException(ErrorCodeConstant.CodeNotFound, "Không tìm thấy mã nhân sự");
            return ns;
        }

        public NsNhanSu FindByMaNhanSuFollowChange(string MaNhanSu)
        {
            var ns = Table.FirstOrDefault(x => x.MaNhanSu == MaNhanSu);
            if (ns == null)
                throw new UserFriendlyException(ErrorCodeConstant.CodeNotFound, "Không tìm thấy mã nhân sự");
            return ns;
        }
    }

    public interface INsNhanSuRepository : IRepositoryBase<NsNhanSu>
    {
        NsNhanSu FindByMaNhanSu(string MaNhanSu);
        NsNhanSu FindByMaNhanSuFollowChange(string MaNhanSu);
    }
}
