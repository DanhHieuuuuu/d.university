using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Http;

namespace D.Core.Infrastructure
{
    public class ServiceUnitOfWork
    {
        private IDbContext _dbContext;
        private IHttpContextAccessor _httpContext;

        private NsNhanSuRepository _nsNhanSuRepository;
        private SvSinhVienRepository _svSinhVienRepository;

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

        public ISvSinhVienRepository iSvSinhVienRepository
        {
            get
            {
                if (_svSinhVienRepository == null)
                {
                    _svSinhVienRepository = new SvSinhVienRepository(_dbContext, _httpContext);
                }
                return _svSinhVienRepository;
            }
        }
    }
}
