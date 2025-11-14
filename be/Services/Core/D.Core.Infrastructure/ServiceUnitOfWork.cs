using D.Core.Infrastructure.Repositories.File;
using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace D.Core.Infrastructure
{
    public class ServiceUnitOfWork
    {
        private IDbContext _dbContext;
        private IHttpContextAccessor _httpContext;

        private DmChucVuRepository _dmChucVuRepository;
        private DmDanTocRepository _dmDanTocRepository;
        private DmGioiTinhRepository _dmGioiTinhRepository;
        private DmLoaiHopDongRepository _dmLoaiHopDongRepository;
        private DmLoaiPhongBanRepository _dmLoaiPhongBanRepository;
        private DmPhongBanRepository _dmPhongBanRepository;
        private DmQuanHeGiaDinhRepository _dmQuanHeGiaDinhRepository;
        private DmQuocTichRepository _dmQuocTichRepository;
        private DmToBoMonRepository _dmToBoMonRepository;
        private DmTonGiaoRepository _dmTonGiaoRepository;
        private DmKhoaRepository _dmKhoaRepository;

        private NsNhanSuRepository _nsNhanSuRepository;
        private NsQuanHeGiaDinhRepository _nsQuanHeGiaDinhRepository;
        private NsHopDongRepository _nsHopDongRepository;
        private NsHopDongChiTietRepository _nsHopDongChiTietRepository;

        private SvSinhVienRepository _svSinhVienRepository;

        private FileRepository _fileRepository;

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
                    _nsQuanHeGiaDinhRepository = new NsQuanHeGiaDinhRepository(
                        _dbContext,
                        _httpContext
                    );
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
                    _nsHopDongChiTietRepository = new NsHopDongChiTietRepository(
                        _dbContext,
                        _httpContext
                    );
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

        public IDmPhongBanRepository iDmPhongBanRepository
        {
            get
            {
                if (_dmPhongBanRepository == null)
                {
                    _dmPhongBanRepository = new DmPhongBanRepository(_dbContext, _httpContext);
                }
                return _dmPhongBanRepository;
            }
        }

        public IDmLoaiPhongBanRepository iDmLoaiPhongBanRepository
        {
            get
            {
                if (_dmLoaiPhongBanRepository == null)
                {
                    _dmLoaiPhongBanRepository = new DmLoaiPhongBanRepository(
                        _dbContext,
                        _httpContext
                    );
                }
                return _dmLoaiPhongBanRepository;
            }
        }

        public IDmDanTocRepository iDmDanTocRepository
        {
            get
            {
                if (_dmDanTocRepository == null)
                {
                    _dmDanTocRepository = new DmDanTocRepository(_dbContext, _httpContext);
                }
                return _dmDanTocRepository;
            }
        }

        public IDmGioiTinhRepository iDmGioiTinhRepository
        {
            get
            {
                if (_dmGioiTinhRepository == null)
                {
                    _dmGioiTinhRepository = new DmGioiTinhRepository(_dbContext, _httpContext);
                }
                return _dmGioiTinhRepository;
            }
        }

        public IDmLoaiHopDongRepository iDmLoaiHopDongRepository
        {
            get
            {
                if (_dmLoaiHopDongRepository == null)
                {
                    _dmLoaiHopDongRepository = new DmLoaiHopDongRepository(
                        _dbContext,
                        _httpContext
                    );
                }
                return _dmLoaiHopDongRepository;
            }
        }

        public IDmQuanHeGiaDinhRepository iDmQuanHeGiaDinhRepository
        {
            get
            {
                if (_dmQuanHeGiaDinhRepository == null)
                {
                    _dmQuanHeGiaDinhRepository = new DmQuanHeGiaDinhRepository(
                        _dbContext,
                        _httpContext
                    );
                }
                return _dmQuanHeGiaDinhRepository;
            }
        }

        public IDmQuocTichRepository iDmQuocTichRepository
        {
            get
            {
                if (_dmQuocTichRepository == null)
                {
                    _dmQuocTichRepository = new DmQuocTichRepository(_dbContext, _httpContext);
                }
                return _dmQuocTichRepository;
            }
        }
        public IDmTonGiaoRepository iDmTonGiaoRepository
        {
            get
            {
                if (_dmTonGiaoRepository == null)
                {
                    _dmTonGiaoRepository = new DmTonGiaoRepository(_dbContext, _httpContext);
                }
                return _dmTonGiaoRepository;
            }
        }
        public IDmToBoMonRepository iDmToBoMonRepository
        {
            get
            {
                if (_dmToBoMonRepository == null)
                {
                    _dmToBoMonRepository = new DmToBoMonRepository(_dbContext, _httpContext);
                }
                return _dmToBoMonRepository;
            }
        }


        public IDmKhoaRepository iDmKhoaRepository
        {
            get
            {
                if (_dmKhoaRepository == null)
                {
                    _dmKhoaRepository = new DmKhoaRepository(_dbContext, _httpContext);
                }
                return _dmKhoaRepository;
            }
        }

        public IFileRepository iFileRepository
        {
            get
            {
                if (_fileRepository == null)
                {
                    _fileRepository = new FileRepository(_dbContext, _httpContext);
                }
                return _fileRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public DatabaseFacade Database => _dbContext.Database;
    }
}
