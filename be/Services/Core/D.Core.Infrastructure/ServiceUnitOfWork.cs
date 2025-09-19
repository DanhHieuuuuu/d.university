using D.Core.Infrastructure.Repositories.Hrm;
using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure
{
    public class ServiceUnitOfWork
    {
        private IDbContext _dbContext;
        private IHttpContextAccessor _httpContext;

        private NsNhanSuRepository _nsNhanSuRepository;

        public ServiceUnitOfWork(IDbContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public INsNhanSuRepository iNsNhanSuRepository
        {
            get
            {
                if (_nsNhanSuRepository == null)
                {
                    _nsNhanSuRepository = new NsNhanSuRepository(_dbContext, _httpContext);
                }
                return _nsNhanSuRepository;
            }
        }
    }
}
