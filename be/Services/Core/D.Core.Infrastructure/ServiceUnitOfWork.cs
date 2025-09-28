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
        private NsQuanHeGiaDinhRepository _nsQuanHeGiaDinhRepository;
        private NsHopDongRepository _nsHopDongRepository;
        private NsHopDongChiTietRepository _nsHopDongChiTietRepository;
        private DmChucVuRepository _dmChucVuRepository;
        
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

        public INsQuanHeGiaDinhRepository iNsQuanHeGiaDinhRepository
        {
            get
            {
                if (_nsQuanHeGiaDinhRepository == null)
                {
                    _nsQuanHeGiaDinhRepository = new NsQuanHeGiaDinhRepository(_dbContext, _httpContext);
                }
                return _nsQuanHeGiaDinhRepository;
            }
        }

        public INsHopDongRepository iNsHopDongRepository
        {
            get
            {
                if (_nsHopDongRepository == null)
                {
                    _nsHopDongRepository = new NsHopDongRepository(_dbContext, _httpContext);
                }
                return _nsHopDongRepository;
            }
        }

        public INsHopDongChiTietRepository iNsHopDongChiTietRepository
        {
            get
            {
                if (_nsHopDongChiTietRepository == null)
                {
                    _nsHopDongChiTietRepository = new NsHopDongChiTietRepository(_dbContext, _httpContext);
                }
                return _nsHopDongChiTietRepository;
            }
        }

        public IDmChucVuRepository iDmChucVuRepository
        {
            get
            {
                if (_dmChucVuRepository == null)
                {
                    _dmChucVuRepository = new DmChucVuRepository(_dbContext, _httpContext);
                }
                return _dmChucVuRepository;
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
