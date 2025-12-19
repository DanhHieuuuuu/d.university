using D.Core.Infrastructure.Repositories.DaoTao;
using D.Core.Infrastructure.Repositories.Delegation.Incoming;
using D.Core.Infrastructure.Repositories.File;
using D.Core.Infrastructure.Repositories.Hrm;
using D.Core.Infrastructure.Repositories.Kpi;
using D.Core.Infrastructure.Repositories.SinhVien;
using D.Core.Infrastructure.Repositories.Survey;
using D.InfrastructureBase.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
        private DmKhoaHocRepository _dmKhoaHocRepository;

        private NsNhanSuRepository _nsNhanSuRepository;
        private NsQuanHeGiaDinhRepository _nsQuanHeGiaDinhRepository;
        private NsHopDongRepository _nsHopDongRepository;
        private NsHopDongChiTietRepository _nsHopDongChiTietRepository;

        #region Dao Tao
        private DtKhoaRepository _dtKhoaRepository;
        private DtNganhRepository _dtNganhRepository;
        private DtChuyenNganhRepository _dtChuyenNganhRepository;
        private DtMonHocRepository _dtMonHocRepository;
        private DtMonTienQuyetRepository _dtMonTienQuyetRepository;
        private DtChuongTrinhKhungRepository _dtChuongTrinhKhungRepository;
        private DtChuongTrinhKhungMonRepository _dtChuongTrinhKhungMonRepository;

        #endregion

        private SvSinhVienRepository _svSinhVienRepository;

        private FileRepository _fileRepository;

        #region Delegation
        private DelegationIncomingRepository _delegationIncomingRepository;
        private DepartmentSupportRepository _departmentSupportRepository;
        private DetailDelegationIncomingRepository _detailDelegationIncomingRepository;
        private LogReceptionTimeRepository _logReceptionTimeRepository;
        private LogStatusRepository _logStatusRepository;
        private PrepareRepository _prepareRepository;
        private ReceptionTimeRepository _receptionTimeRepository;
        private SupporterRepository _supporterRepository;

        #endregion

        #region Kpi
        private KpiCaNhanRepository _kpiCaNhanRepository;
        private KpiDonViRepository _kpiDonViRepository;
        private KpiLogStatusRepository _kpiLogStatusRepository;
        private KpiTemplateRepository _kpiTemplateRepository;
        private KpiTruongRepository _kpiTruongRepository;
        #endregion

        #region Survey
        private SurveyRequestRepository _surveyRequestRepository;
        private SurveyLogRepository _surveyLogRepository;
        #endregion

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


        public IDmKhoaHocRepository iDmKhoaHocRepository
        {
            get
            {
                if (_dmKhoaHocRepository == null)
                {
                    _dmKhoaHocRepository = new DmKhoaHocRepository(_dbContext, _httpContext);
                }
                return _dmKhoaHocRepository;
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

        #region Dao Tao
        public IDtKhoaRepository iDtKhoaRepository
        {
            get
            {
                if (_dtKhoaRepository == null)
                {
                    _dtKhoaRepository = new DtKhoaRepository(_dbContext, _httpContext);
                }
                return _dtKhoaRepository;
            }
        }

        public IDtNganhRepository iDtNganhRepository
        {
            get
            {
                if (_dtNganhRepository == null)
                {
                    _dtNganhRepository = new DtNganhRepository(_dbContext, _httpContext);
                }
                return _dtNganhRepository;
            }
        }
        public IDtChuyenNganhRepository iDtChuyenNganhRepository
        {
            get
            {
                if (_dtChuyenNganhRepository == null)
                {
                    _dtChuyenNganhRepository = new DtChuyenNganhRepository(_dbContext, _httpContext);
                }
                return _dtChuyenNganhRepository;
            }
        }
        public IDtMonHocRepository iDtMonHocRepository
        {
            get
            {
                if (_dtMonHocRepository == null)
                {
                    _dtMonHocRepository = new DtMonHocRepository(_dbContext, _httpContext);
                }
                return _dtMonHocRepository;
            }
        }
        public IDtMonTienQuyetRepository iDtMonTienQuyetRepository
        {
            get
            {
                if (_dtMonTienQuyetRepository == null)
                {
                    _dtMonTienQuyetRepository = new DtMonTienQuyetRepository(_dbContext, _httpContext);
                }
                return _dtMonTienQuyetRepository;
            }
        }
        public IDtChuongTrinhKhungRepository iDtChuongTrinhKhungRepository
        {
            get
            {
                if (_dtChuongTrinhKhungRepository == null)
                {
                    _dtChuongTrinhKhungRepository = new DtChuongTrinhKhungRepository(_dbContext, _httpContext);
                }
                return _dtChuongTrinhKhungRepository;
            }
        }
        public IDtChuongTrinhKhungMonRepository iDtChuongTrinhKhungMonRepository
        {
            get
            {
                if (_dtChuongTrinhKhungMonRepository == null)
                {
                    _dtChuongTrinhKhungMonRepository = new DtChuongTrinhKhungMonRepository(_dbContext, _httpContext);
                }
                return _dtChuongTrinhKhungMonRepository;
            }
        }

        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public DatabaseFacade Database => _dbContext.Database;

        #region Delegation

        public IDelegationIncomingRepository iDelegationIncomingRepository
        {
            get
            {
                if (_delegationIncomingRepository == null)
                {
                    _delegationIncomingRepository = new DelegationIncomingRepository(_dbContext, _httpContext);
                }
                return _delegationIncomingRepository;
            }
        }

        public IDepartmentSupportRepository iDepartmentSupportRepository
        {
            get
            {
                if (_departmentSupportRepository == null)
                {
                    _departmentSupportRepository = new DepartmentSupportRepository(_dbContext, _httpContext);
                }
                return _departmentSupportRepository;
            }
        }

        public IDetailDelegationIncomingRepository iDetailDelegationIncomingRepository
        {
            get
            {
                if (_detailDelegationIncomingRepository == null)
                {
                    _detailDelegationIncomingRepository = new DetailDelegationIncomingRepository(_dbContext, _httpContext);
                }
                return _detailDelegationIncomingRepository;
            }
        }
        public ILogReceptionTimeRepository iLogReceptionTimeRepository
        {
            get
            {
                if (_logReceptionTimeRepository == null)
                {
                    _logReceptionTimeRepository = new LogReceptionTimeRepository(_dbContext, _httpContext);
                }
                return _logReceptionTimeRepository;
            }
        }
        public ILogStatusRepository iLogStatusRepository
        {
            get
            {
                if (_logStatusRepository == null)
                {
                    _logStatusRepository = new LogStatusRepository(_dbContext, _httpContext);
                }
                return _logStatusRepository;
            }
        }
        public IPrepareRepository iPrepareRepository
        {
            get
            {
                if (_prepareRepository == null)
                {
                    _prepareRepository = new PrepareRepository(_dbContext, _httpContext);
                }
                return _prepareRepository;
            }
        }
        public IReceptionTimeRepository iReceptionTimeRepository
        {
            get
            {
                if (_receptionTimeRepository == null)
                {
                    _receptionTimeRepository = new ReceptionTimeRepository(_dbContext, _httpContext);
                }
                return _receptionTimeRepository;
            }
        }
        public ISupporterRepository iSupporterRepository
        {
            get
            {
                if (_supporterRepository == null)
                {
                    _supporterRepository = new SupporterRepository(_dbContext, _httpContext);
                }
                return _supporterRepository;
            }
        }
        #endregion

        #region Kpi
        public IKpiCaNhanRepository iKpiCaNhanRepository
        {
            get
            {
                if (_kpiCaNhanRepository == null)
                {
                    _kpiCaNhanRepository = new KpiCaNhanRepository(_dbContext, _httpContext);
                }
                return _kpiCaNhanRepository;
            }
        }

        public IKpiDonViRepository iKpiDonViRepository
        {
            get
            {
                if (_kpiDonViRepository == null)
                {
                    _kpiDonViRepository = new KpiDonViRepository(_dbContext, _httpContext);
                }
                return _kpiDonViRepository;
            }
        }
        public IKpiLogStatusRepository iKpiLogStatusRepository
        {
            get
            {
                if (_kpiLogStatusRepository == null)
                {
                    _kpiLogStatusRepository = new KpiLogStatusRepository(_dbContext, _httpContext);
                }
                return _kpiLogStatusRepository;
            }
        }
        public IKpiTemplateRepository iKpiTemplateRepository
        {
            get
            {
                if (_kpiTemplateRepository == null)
                {
                    _kpiTemplateRepository = new KpiTemplateRepository(_dbContext, _httpContext);
                }
                return _kpiTemplateRepository;
            }
        }
        public IKpiTruongRepository iKpiTruongRepository
        {
            get
            {
                if (_kpiTruongRepository == null)
                {
                    _kpiTruongRepository = new KpiTruongRepository(_dbContext, _httpContext);
                }
                return _kpiTruongRepository;
            }
        }

        #endregion

        #region Survey
        public IKsSurveyRequestRepository iKsSurveyRequestRepository
        {
            get
            {
                if (_surveyRequestRepository == null)
                {
                    _surveyRequestRepository = new SurveyRequestRepository(_dbContext, _httpContext);
                }
                return _surveyRequestRepository;
            }
        }

        public IKsSurveyLogRepository iKsSurveyLogRepository
        {
            get
            {
                if (_surveyLogRepository == null)
                {
                    _surveyLogRepository = new SurveyLogRepository(_dbContext, _httpContext);
                }
                return _surveyLogRepository;
            }
        }

        #endregion
    }
}
