using D.Auth.Domain.Entities;
using D.InfrastructureBase.Database;
using D.InfrastructureBase.Repository;
using Microsoft.AspNetCore.Http;

namespace D.Auth.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<NsNhanSu>, IUserRepository
    {
        public UserRepository(IDbContext dbContext, IHttpContextAccessor httpContext) : base(dbContext, httpContext)
        {
        }
    }
    public interface IUserRepository : IRepositoryBase<NsNhanSu> { }
}
