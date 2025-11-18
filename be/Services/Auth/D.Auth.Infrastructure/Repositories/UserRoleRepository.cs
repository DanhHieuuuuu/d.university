using D.Auth.Domain.Entities;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace D.Auth.Infrastructure.Repositories
{
    public class UserRoleRepository : RepositoryBase<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbContext dbContext, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
        }

        public IQueryable<UserRole> GetDetailUserRole(int id)
        {
            return TableNoTracking.Where(x => x.NhanSuId == id).Include(x => x.Role).ThenInclude(x => x.RolePermissions);
        }

        public UserRole? FindByRoleIdAndNsId(int roleId, int nsId)
        {
            return TableNoTracking.FirstOrDefault(x => x.RoleId == roleId && x.NhanSuId == nsId);
        }
    }

    public interface IUserRoleRepository : IRepositoryBase<UserRole>
    {
        UserRole? FindByRoleIdAndNsId(int roleId, int nsId);
        IQueryable<UserRole> GetDetailUserRole(int id);
    }
}
