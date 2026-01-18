using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiTinhDiem;
using D.Core.Domain.Dtos.Kpi.KpiTinhDiem.D.Core.Domain.Dtos.Kpi.KpiTinhDiem;
using D.Core.Domain.Entities.Kpi.Constants;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiTinhDiemService : ServiceBase, IKpiTinhDiemService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiTinhDiemService(
            ILogger<KpiTinhDiemService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PersonalScoreDto> CalculatePersonalScore(PersonalScoreRequestDto dto)
        {
            var user = await _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == dto.IdNhanSu);

            if (user == null) return null;

            var roles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdNhanSu == dto.IdNhanSu)
                .ToListAsync();

            if (!roles.Any()) return null;

            decimal finalScore = 0;
            bool isAllFinalized = true;
            bool hasAnyData = false;

            foreach (var role in roles)
            {
                decimal componentScore = 0;
                decimal tiLe = (role.TiLe ?? 0) / 100m;

                if (tiLe <= 0) continue;
                if (role.Role == "TRUONG_DON_VI_CAP_2" && role.IdDonVi.HasValue)
                {
                    var kpiDonViList = await _unitOfWork.iKpiDonViRepository.TableNoTracking
                        .Where(x => x.IdDonVi == role.IdDonVi && x.NamHoc == dto.NamHoc && !x.Deleted)
                        .Select(x => new { x.DiemKpiCapTren, x.LoaiKpi, x.TrangThai })
                        .ToListAsync();

                    if (kpiDonViList.Any())
                    {
                        hasAnyData = true;
                        componentScore = kpiDonViList.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0));
                        if (!kpiDonViList.All(x => x.TrangThai == KpiStatus.PrincipalApprove))
                        {
                            isAllFinalized = false;
                        }
                    }
                }
                else
                {
                    var kpiCaNhanList = await _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                        .Where(x => x.IdNhanSu == dto.IdNhanSu
                                    && x.NamHoc == dto.NamHoc
                                    && x.Role == role.Role
                                    && !x.Deleted)
                        .Select(x => new { x.DiemKpiCapTren, x.LoaiKPI, x.Status })
                        .ToListAsync();

                    if (kpiCaNhanList.Any())
                    {
                        hasAnyData = true;
                        componentScore = kpiCaNhanList.Sum(x => x.LoaiKPI == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0));
                        if (!kpiCaNhanList.All(x => x.Status == KpiStatus.PrincipalApprove))
                        {
                            isAllFinalized = false;
                        }
                    }
                }
                finalScore += (componentScore * tiLe);
            }

            return new PersonalScoreDto
            {
                IdNhanSu = dto.IdNhanSu,
                HoTen = $"{user.HoDem} {user.Ten}".Trim(),
                ChucVuChinh = roles.OrderByDescending(r => r.TiLe).FirstOrDefault()?.Role ?? "",
                DiemTongKet = finalScore,
                IsFinalized = hasAnyData && isAllFinalized,

                XepLoai = CalculateRank(finalScore)
            };
        }

        public async Task<SchoolScoreDto> CalculateSchoolScore(SchoolScoreRequestDto dto)
        {
            var kpiList = await _unitOfWork.iKpiTruongRepository.TableNoTracking
                .Where(x => x.NamHoc == dto.NamHoc && !x.Deleted)
                .Select(x => new { x.DiemKpiCapTren, x.LoaiKpi, x.TrangThai })
                .ToListAsync();

            decimal score = 0;
            bool isFinalized = false;

            if (kpiList.Any())
            {
                score = kpiList.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0));
                isFinalized = kpiList.All(x => x.TrangThai == KpiStatus.PrincipalApprove);
            }

            return new SchoolScoreDto
            {
                DiemKpiTruong = score,
                XepLoaiTruong = CalculateRank(score),
                IsFinalized = isFinalized
            };
        }
        public async Task<UnitScoreDto> CalculateUnitScore(UnitScoreRequestDto dto)
        {
            var donVi = await _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .FirstOrDefaultAsync(x => x.Id == dto.IdDonVi);

            var kpiList = await _unitOfWork.iKpiDonViRepository.TableNoTracking
                .Where(x => x.IdDonVi == dto.IdDonVi && x.NamHoc == dto.NamHoc && !x.Deleted)
                .Select(x => new { x.DiemKpiCapTren, x.LoaiKpi, x.TrangThai })
                .ToListAsync();

            decimal score = 0;
            bool isFinalized = false;

            if (kpiList.Any())
            {
                score = kpiList.Sum(x => x.LoaiKpi == 3 ? -(x.DiemKpiCapTren ?? 0) : (x.DiemKpiCapTren ?? 0));
                isFinalized = kpiList.All(x => x.TrangThai == KpiStatus.PrincipalApprove);
            }

            return new UnitScoreDto
            {
                IdDonVi = dto.IdDonVi,
                TenDonVi = donVi?.TenPhongBan ?? "N/A",
                DiemKpiDonVi = score,
                XepLoaiDonVi = CalculateRank(score),
                IsFinalized = isFinalized
            };
        }

        public async Task<List<int>> GetManagedUnitIds(int userId)
        {
            var roles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdNhanSu == userId)
                .ToListAsync();

            var unitIds = new List<int>();

            foreach (var role in roles)
            {
                if ((role.Role == "PHO_HIEU_TRUONG" || role.Role == "TRUONG_DON_VI_CAP_2") && role.IdDonVi.HasValue)
                {
                    unitIds.Add(role.IdDonVi.Value);
                }
            }
            return unitIds.Distinct().ToList();
        }

        public async Task<List<PersonalScoreDto>> GetStaffScoresInUnit(StaffScoreRequestDto dto)
        {
            var currentUserId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var currentUnit = await _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .FirstOrDefaultAsync(u => u.Id == dto.IdDonVi);

            bool isBanGiamHieu = false;
            if (currentUnit != null)
            {
                string uName = currentUnit.TenPhongBan.ToLower();
                isBanGiamHieu = uName.Contains("ban giám hiệu") || uName.Contains("hội đồng trường");
            }
            var staffRoles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdDonVi == dto.IdDonVi && !r.Deleted)
                .Select(r => new { r.IdNhanSu, r.Role })
                .ToListAsync();

            var validStaffIds = staffRoles
                .Where(r =>
                {
                    if (r.Role == "HIEU_TRUONG" || r.Role == "CHU_TICH_HOI_DONG_TRUONG")
                        return false;
                    if (r.IdNhanSu == currentUserId)
                        return false;
                    if (r.Role == "PHO_HIEU_TRUONG")
                    {
                        if (isBanGiamHieu) return true;
                        return false;
                    }
                    return true;
                })
                .Select(r => r.IdNhanSu)
                .Distinct()
                .ToList();

            var result = new List<PersonalScoreDto>();
            foreach (var id in validStaffIds)
            {
                var score = await CalculatePersonalScore(new PersonalScoreRequestDto
                {
                    IdNhanSu = id,
                    NamHoc = dto.NamHoc
                });
                if (score != null) result.Add(score);
            }
            return result.OrderByDescending(x => x.DiemTongKet).ToList();
        }

        private string CalculateRank(decimal score)
        {
            if (score >= 120) return "A (Hoàn thành xuất sắc nhiệm vụ)";
            if (score > 100) return "B (Hoàn thành tốt nhiệm vụ)";
            if (score > 80) return "C (Hoàn thành nhiệm vụ)";
            if (score > 50) return "D (Còn thiếu sót nhưng hoàn thành nhiệm vụ)";
            if (score <= 50) return "F (Không hoàn thành nhiệm vụ)";
            return "Chưa xếp loại";
        }

        public async Task<KpiDashboardResponse> GetDashboardData(GetKpiScoreBoardDto dto)
        {
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var namHoc = string.IsNullOrEmpty(dto.NamHoc) ? DateTime.Now.Year.ToString() : dto.NamHoc;

            var result = new KpiDashboardResponse();
            result.MyScore = await CalculatePersonalScore(new PersonalScoreRequestDto
            {
                IdNhanSu = userId,
                NamHoc = namHoc
            });

            var userRoles = await _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => r.IdNhanSu == userId).ToListAsync();

            bool isHieuTruong = userRoles.Any(r => r.Role == "HIEU_TRUONG" || r.Role == "CHU_TICH_HOI_DONG_TRUONG");
            var allowedUnitIds = new List<int>();
            foreach (var role in userRoles)
            {
                if (!role.IdDonVi.HasValue) continue;

                if (role.Role == "TRUONG_DON_VI_CAP_2")
                {
                    allowedUnitIds.Add(role.IdDonVi.Value);
                }
                else if (role.Role == "PHO_HIEU_TRUONG")
                {
                    var unit = await _unitOfWork.iDmPhongBanRepository.TableNoTracking
                        .FirstOrDefaultAsync(u => u.Id == role.IdDonVi.Value);

                    if (unit != null)
                    {
                        string unitName = unit.TenPhongBan.ToLower();
                        bool isRestrictedUnit = unitName.Contains("ban giám hiệu") || unitName.Contains("hội đồng trường");

                        if (!isRestrictedUnit)
                        {
                            allowedUnitIds.Add(role.IdDonVi.Value);
                        }
                    }
                }
            }
            allowedUnitIds = allowedUnitIds.Distinct().ToList();

            if (isHieuTruong)
            {
                if (dto.ViewUnitId == null)
                {
                    result.ViewMode = "SCHOOL";
                    result.SchoolScore = await CalculateSchoolScore(new SchoolScoreRequestDto { NamHoc = namHoc });

                    var allUnits = await _unitOfWork.iDmPhongBanRepository.TableNoTracking.ToListAsync();
                    result.AllUnits = new List<UnitScoreDto>();
                    foreach (var dv in allUnits)
                    {
                        var uScore = await CalculateUnitScore(new UnitScoreRequestDto { IdDonVi = dv.Id, NamHoc = namHoc });
                        result.AllUnits.Add(uScore);
                    }
                }
                else
                {
                    result.ViewMode = "UNIT";
                    result.CurrentUnitScore = await CalculateUnitScore(new UnitScoreRequestDto { IdDonVi = dto.ViewUnitId.Value, NamHoc = namHoc });
                    result.StaffScores = await GetStaffScoresInUnit(new StaffScoreRequestDto { IdDonVi = dto.ViewUnitId.Value, NamHoc = namHoc });
                }
            }
            else if (allowedUnitIds.Any())
            {
                int targetUnitId;
                if (dto.ViewUnitId.HasValue && allowedUnitIds.Contains(dto.ViewUnitId.Value))
                {
                    targetUnitId = dto.ViewUnitId.Value;
                    result.ViewMode = "UNIT";
                    result.CurrentUnitScore = await CalculateUnitScore(new UnitScoreRequestDto { IdDonVi = targetUnitId, NamHoc = namHoc });
                    result.StaffScores = await GetStaffScoresInUnit(new StaffScoreRequestDto { IdDonVi = targetUnitId, NamHoc = namHoc });
                }
                else if (allowedUnitIds.Count == 1)
                {
                    targetUnitId = allowedUnitIds.First();
                    result.ViewMode = "UNIT";
                    result.CurrentUnitScore = await CalculateUnitScore(new UnitScoreRequestDto { IdDonVi = targetUnitId, NamHoc = namHoc });
                    result.StaffScores = await GetStaffScoresInUnit(new StaffScoreRequestDto { IdDonVi = targetUnitId, NamHoc = namHoc });
                }
                else
                {
                    result.ViewMode = "SCHOOL"; 
                    result.SchoolScore = null; 
                    result.AllUnits = new List<UnitScoreDto>();

                    foreach (var unitId in allowedUnitIds)
                    {
                        var uScore = await CalculateUnitScore(new UnitScoreRequestDto { IdDonVi = unitId, NamHoc = namHoc });
                        result.AllUnits.Add(uScore);
                    }
                }
            }
            else
            {
                result.ViewMode = "PERSONAL";
            }

            return result;
        }
    }
}