using D.Auth.Domain.Entities;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Auth.Infrastructure.Repositories
{
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(IDbContext dbContext, IHttpContextAccessor httpContext)
            : base(dbContext, httpContext) { }

        public bool IsValidPermission(int permissionId)
        {
            return TableNoTracking.Any(p => p.Id == permissionId);
        }
    }

    public interface IPermissionRepository : IRepositoryBase<Permission>
    {
        bool IsValidPermission(int permissionId);
    }
}
