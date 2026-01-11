using System.Text.Json;
using AutoMapper;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Domain.Dtos.Hrm.QuanHeGiaDinh;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using d.Shared.Permission.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NsNhanSuEntity = D.Core.Domain.Entities.Hrm.NhanSu.NsNhanSu;
using D.Core.Domain.Entities.Kpi;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NsNhanSuService : ServiceBase, INsNhanSuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly INsQuyetDinhService _decisionService;

        public NsNhanSuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            INsQuyetDinhService decisionService
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _decisionService = decisionService;
        }

        #region Public API

        public PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingNsNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iNsNhanSuRepository.TableNoTracking.AsQueryable();

            // Filter
            if (!string.IsNullOrEmpty(dto.Cccd))
            {
                query = query.Where(x => dto.Cccd == x.SoCccd);
            }

            if (dto.IdPhongBan != null)
            {
                query = query.Where(x => x.HienTaiPhongBan == dto.IdPhongBan);
            }

            var totalCount = query.Count();

            var items = query
                .OrderBy(x => x.Id) // cố định thứ tự
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            // Lấy tên phòng ban, chức vụ
            var (pbDict, cvDict) = GetPhongBanChucVuDict(items);

            var result = items
                .Select(x => new NsNhanSuResponseDto
                {
                    IdNhanSu = x.Id,
                    MaNhanSu = x.MaNhanSu,
                    HoDem = x.HoDem,
                    Ten = x.Ten,
                    NgaySinh = x.NgaySinh,
                    NoiSinh = x.NoiSinh,
                    SoDienThoai = x.SoDienThoai,
                    Email = x.Email,
                    GioiTinh = x.GioiTinh,
                    HoTen = (x.HoDem ?? "") + " " + (x.Ten ?? ""),
                    SoCccd = x.SoCccd,
                    TenPhongBan =
                        x.HienTaiPhongBan.HasValue
                        && pbDict.TryGetValue(x.HienTaiPhongBan.Value, out var pbName)
                            ? pbName
                            : null,
                    TenChucVu =
                        x.HienTaiChucVu.HasValue
                        && cvDict.TryGetValue(x.HienTaiChucVu.Value, out var cvName)
                            ? cvName
                            : null,
                })
                .ToList();

            return new PageResultDto<NsNhanSuResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }

        public PageResultDto<NsNhanSuGetAllResponseDto> GetAllNhanSu(NsNhanSuGetAllRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iNsNhanSuRepository.TableNoTracking.AsQueryable();

            // Chỉ lấy user có password
            query = query.Where(x => !string.IsNullOrEmpty(x.Password));

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                var kw = dto.Keyword;
                query = query.Where(x =>
                    x.MaNhanSu.Contains(kw)
                    || ((x.HoDem ?? "") + " " + (x.Ten ?? "")).Contains(kw)
                    || (x.SoCccd ?? "").Contains(kw)
                );
            }
            if (dto.IdPhongBan.HasValue)
            {
                query = query.Where(x => x.HienTaiPhongBan == dto.IdPhongBan.Value);
            }

            var totalCount = query.Count();

            var items = query.OrderBy(x => x.Id).Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            var (pbDict, cvDict) = GetPhongBanChucVuDict(items);

            var result = items
                .Select(x => new NsNhanSuGetAllResponseDto
                {
                    Id = x.Id,
                    MaNhanSu = x.MaNhanSu,
                    HoDem = x.HoDem,
                    Ten = x.Ten,
                    NgaySinh = x.NgaySinh,
                    NoiSinh = x.NoiSinh,
                    SoDienThoai = x.SoDienThoai,
                    Email = x.Email,
                    Email2 = x.Email2,
                    SoCccd = x.SoCccd,
                    TenPhongBan =
                        x.HienTaiPhongBan.HasValue
                        && pbDict.TryGetValue(x.HienTaiPhongBan.Value, out var pbName)
                            ? pbName
                            : null,
                    TenChucVu =
                        x.HienTaiChucVu.HasValue
                        && cvDict.TryGetValue(x.HienTaiChucVu.Value, out var cvName)
                            ? cvName
                            : null,
                    TrangThai = GetTrangThaiText(x),
                })
                .ToList();

            return new PageResultDto<NsNhanSuGetAllResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }

        public PageResultDto<NsNhanSuByKpiRoleResponseDto> GetAllNhanSuByKpiRole(NsNhanSuByKpiRoleRequestDto dto)
        {
            var userId = CommonUntil.GetCurrentUserId(_contextAccessor);

            var userRoles = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(x => x.IdNhanSu == userId)
                .ToList();

            var isHieuTruong = userRoles.Any(x => x.Role == "HIEU_TRUONG");

            var donViIds = userRoles
                .Where(x => (x.Role.StartsWith("TRUONG_DON_VI_CAP_2") || x.Role.StartsWith("TRUONG_DON_VI_CAP_3")) && x.IdDonVi.HasValue)
                .Select(x => x.IdDonVi!.Value)
                .Distinct()
                .ToList();

            List<int> allowedNhanSuIds;

            if (isHieuTruong)
            {
                allowedNhanSuIds = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .Where(r => r.Role != "TRUONG_DON_VI_CAP_2") 
                    .Select(r => r.IdNhanSu)
                    .Distinct()
                    .ToList();
            }
            else
            {
                allowedNhanSuIds = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .Where(r => r.IdDonVi.HasValue && donViIds.Contains(r.IdDonVi.Value))
                    .Select(r => r.IdNhanSu)
                    .Distinct()
                    .ToList();
            }

            if (dto.IdPhongBan.HasValue)
            {
                allowedNhanSuIds = _unitOfWork.iKpiRoleRepository.TableNoTracking
                    .Where(r => r.IdDonVi == dto.IdPhongBan.Value && allowedNhanSuIds.Contains(r.IdNhanSu))
                    .Select(r => r.IdNhanSu)
                    .Distinct()
                    .ToList();
            }

            if (!allowedNhanSuIds.Any())
            {
                return new PageResultDto<NsNhanSuByKpiRoleResponseDto>
                {
                    Items = new List<NsNhanSuByKpiRoleResponseDto>(),
                    TotalItem = 0
                };
            }

            // Lấy danh sách nhân sự
            var items = _unitOfWork.iNsNhanSuRepository.TableNoTracking
                .Where(x => !string.IsNullOrEmpty(x.Password) && allowedNhanSuIds.Contains(x.Id))
                .OrderBy(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            if (!isHieuTruong && donViIds.Any())
            {
                items = items.Where(x => x.Id != userId).ToList();
            }

            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                var kw = dto.Keyword.ToLower();
                items = items.Where(x =>
                    (x.MaNhanSu ?? "").ToLower().Contains(kw) ||
                    ((x.HoDem ?? "") + " " + (x.Ten ?? "")).ToLower().Contains(kw) ||
                    (x.SoCccd ?? "").Contains(kw)
                ).ToList();
            }

            // Lấy tất cả KPI Role của các nhân sự này
            var kpiRolesDict = _unitOfWork.iKpiRoleRepository.TableNoTracking
                .Where(r => items.Select(i => i.Id).Contains(r.IdNhanSu) && r.IdDonVi.HasValue)
                .ToList()
                .GroupBy(r => r.IdNhanSu)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Lấy tất cả phòng ban cần hiển thị
            var allDonViIds = kpiRolesDict.Values.SelectMany(v => v.Select(r => r.IdDonVi!.Value)).Distinct().ToList();
            var pbDict = _unitOfWork.iDmPhongBanRepository.TableNoTracking
                .Where(p => allDonViIds.Contains(p.Id))
                .ToDictionary(p => p.Id, p => p.TenPhongBan);

            // Map sang DTO
            var result = items.SelectMany(x =>
            {
                kpiRolesDict.TryGetValue(x.Id, out var rolesOfNhanSu);
                if (rolesOfNhanSu == null || !rolesOfNhanSu.Any())
                    return new List<NsNhanSuByKpiRoleResponseDto>();

                // Lọc Role theo người xem
                IEnumerable<KpiRole> rolesToShow;

                if (isHieuTruong)
                {
                    // Hiệu trưởng: tất cả Role nhưng bỏ TRUONG_DON_VI_CAP_2
                    rolesToShow = rolesOfNhanSu.Where(r => r.Role != "TRUONG_DON_VI_CAP_2");
                }
                else
                {
                    // Trưởng đơn vị: chỉ Role trong đơn vị do họ quản lý
                    rolesToShow = rolesOfNhanSu.Where(r => r.IdDonVi.HasValue && donViIds.Contains(r.IdDonVi.Value)).Where(r => r.Role != "PHO_HIEU_TRUONG"); ;
                }

                // Nếu dto.IdPhongBan filter thì chỉ lấy role ứng với phòng ban đó
                if (dto.IdPhongBan.HasValue)
                    rolesToShow = rolesToShow.Where(r => r.IdDonVi == dto.IdPhongBan.Value);

                return rolesToShow.Select(r =>
                {
                    var idDonViKpi = r.IdDonVi ?? 0;
                    var roleName = MapRoleToChucVu(r.Role);

                    return new NsNhanSuByKpiRoleResponseDto
                    {
                        Id = x.Id,
                        MaNhanSu = x.MaNhanSu,
                        HoDem = x.HoDem,
                        Ten = x.Ten,
                        NgaySinh = x.NgaySinh,
                        NoiSinh = x.NoiSinh,
                        SoDienThoai = x.SoDienThoai,
                        Email = x.Email,
                        Email2 = x.Email2,
                        SoCccd = x.SoCccd,
                        TenPhongBan = pbDict.TryGetValue(idDonViKpi, out var pbName) ? pbName : null,
                        TenChucVu = roleName,
                        TrangThai = GetTrangThaiText(x),
                    };
                });
            }).ToList();


            return new PageResultDto<NsNhanSuByKpiRoleResponseDto>
            {
                Items = result,
                TotalItem = items.Count
            };

        }


        public void CreateGiaDinhNhanSu(int idNhanSu, CreateNsQuanHeGiaDinhDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateGiaDinhNhanSu)} method called. Dto: IdNhanSu: {idNhanSu} {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iNsQuanHeGiaDinhRepository.TableNoTracking.Any(x =>
                x.IdNhanSu == idNhanSu && x.HoTen == dto.HoTen
            );

            if (exist)
                return;

            var nsGiaDinh = new NsQuanHeGiaDinh
            {
                IdNhanSu = idNhanSu,
                HoTen = dto.HoTen,
                NgaySinh = dto.NgaySinh,
                DonViCongTac = dto.DonViCongTac,
                QuanHe = dto.QuanHe,
                QuocTich = dto.QuocTich,
                SoDienThoai = dto.SoDienThoai,
                NgheNghiep = dto.NgheNghiep,
                QueQuan = dto.QueQuan,
            };

            _unitOfWork.iNsQuanHeGiaDinhRepository.Add(nsGiaDinh);
            _unitOfWork.iNsQuanHeGiaDinhRepository.SaveChange();
        }

        public NsNhanSuResponseDto CreateNhanSu(CreateNhanSuDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            // Tạo entity nhân sự
            var newNhanSu = CreateNewNhanSu(dto);

            // Thêm gia đình (nếu có) — gom lại và gọi SaveChange 1 lần cho repository gia đình
            if (HasFamilyInfo(dto))
            {
                AddGiaDinh(newNhanSu.Id, dto.ThongTinGiaDinh!);
            }

            return _mapper.Map<NsNhanSuResponseDto>(newNhanSu);
        }

        public void UpdateThongTinCongViec(UpdateNhanSuCongViecDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateNhanSuCongViecDto)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var nhansu = _unitOfWork.iNsNhanSuRepository.Table.FirstOrDefault(x =>
                dto.IdNhanSu == x.Id
            );

            if (nhansu == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tìm thấy nhân sự với Id: {dto.IdNhanSu}"
                );
            }
            else
            {
                nhansu.MaNhanSu = dto.MaNhanSu;
                nhansu.MaSoThue = dto.MaSoThue;
                nhansu.TenNganHang1 = dto.TenNganHang1;
                nhansu.TenNganHang2 = dto.TenNganHang2;
                nhansu.Atm1 = dto.Atm1;
                nhansu.Atm2 = dto.Atm2;
                nhansu.HienTaiChucVu = dto.HienTaiChucVu;
                nhansu.HienTaiPhongBan = dto.HienTaiPhongBan;

                _unitOfWork.iNsNhanSuRepository.Update(nhansu);
                _unitOfWork.iNsNhanSuRepository.SaveChange();
            }
        }

        public NsNhanSuResponseDto FindByMaNsSdt(FindByMaNsSdtDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindByMaNsSdt)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x =>
                string.IsNullOrEmpty(dto.Keyword)
                || dto.Keyword == x.SoDienThoai
                || dto.Keyword == x.MaNhanSu
            );

            if (nhanSu == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tìm thấy nhân sự với từ khóa: {dto.Keyword}"
                );
            }

            // Gán tên phòng ban & chức vụ
            var (pbDict, cvDict) = GetPhongBanChucVuDict(new[] { nhanSu });

            var result = new NsNhanSuResponseDto
            {
                MaNhanSu = nhanSu.MaNhanSu,
                HoDem = nhanSu.HoDem,
                Ten = nhanSu.Ten,
                NgaySinh = nhanSu.NgaySinh,
                NoiSinh = nhanSu.NoiSinh,
                SoDienThoai = nhanSu.SoDienThoai,
                Email = nhanSu.Email,
                TenPhongBan =
                    nhanSu.HienTaiPhongBan.HasValue
                    && pbDict.TryGetValue(nhanSu.HienTaiPhongBan.Value, out var pbName)
                        ? pbName
                        : null,
                TenChucVu =
                    nhanSu.HienTaiChucVu.HasValue
                    && cvDict.TryGetValue(nhanSu.HienTaiChucVu.Value, out var cvName)
                        ? cvName
                        : null,
            };

            return result;
        }

        public NsNhanSuFindByIdResponseDto FindById(int idNhanSu)
        {
            _logger.LogInformation($"{nameof(FindById)} method called. IdNhanSu: {idNhanSu}");

            var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x =>
                idNhanSu == x.Id
            );

            if (nhanSu == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tìm thấy nhân sự với Id: {idNhanSu}"
                );
            }

            var result = _mapper.Map<NsNhanSuFindByIdResponseDto>(nhanSu);

            var (pbDict, cvDict) = GetPhongBanChucVuDict(new[] { nhanSu });

            result.TenPhongBan =
                nhanSu.HienTaiPhongBan.HasValue
                && pbDict.TryGetValue(nhanSu.HienTaiPhongBan.Value, out var pbName)
                    ? pbName
                    : null;
            result.TenChucVu =
                nhanSu.HienTaiChucVu.HasValue
                && cvDict.TryGetValue(nhanSu.HienTaiChucVu.Value, out var cvName)
                    ? cvName
                    : null;

            return result;
        }

        public NsNhanSuHoSoChiTietResponseDto HoSoChiTietNhanSu(int idNhanSu)
        {
            _logger.LogInformation(
                $"{nameof(HoSoChiTietNhanSu)} method called. IdNhanSu: {idNhanSu}"
            );

            var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x =>
                idNhanSu == x.Id
            );

            if (nhanSu == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tìm thấy nhân sự với Id: {idNhanSu}"
                );
            }

            var result = _mapper.Map<NsNhanSuHoSoChiTietResponseDto>(nhanSu);

            var (pbDict, cvDict) = GetPhongBanChucVuDict(new[] { nhanSu });

            result.TenPhongBan =
                nhanSu.HienTaiPhongBan.HasValue
                && pbDict.TryGetValue(nhanSu.HienTaiPhongBan.Value, out var pbName)
                    ? pbName
                    : null;
            result.TenChucVu =
                nhanSu.HienTaiChucVu.HasValue
                && cvDict.TryGetValue(nhanSu.HienTaiChucVu.Value, out var cvName)
                    ? cvName
                    : null;

            result.ThongTinGiaDinh = GetThongTinGiaDinh(idNhanSu);

            return result;
        }

        #endregion

        #region Private helpers


        // chỉ thêm mới thông tin nhân sự
        private NsNhanSuEntity CreateNewNhanSu(CreateNhanSuDto dto)
        {
            var entity = _mapper.Map<NsNhanSuEntity>(dto);

            _unitOfWork.iNsNhanSuRepository.Add(entity);
            _unitOfWork.iNsNhanSuRepository.SaveChange();

            return entity;
        }

        private List<NsQuanHeGiaDinhResponseDto> GetThongTinGiaDinh(int idNhanSu)
        {
            var thanhViens = _unitOfWork
                .iNsQuanHeGiaDinhRepository.TableNoTracking.Where(x => x.IdNhanSu == idNhanSu)
                .ToList();

            if (!thanhViens.Any())
                return new List<NsQuanHeGiaDinhResponseDto>();

            var quanHeIds = thanhViens.Select(x => x.QuanHe).Distinct().ToList();

            var quanHeDict = _unitOfWork
                .iDmQuanHeGiaDinhRepository.TableNoTracking.Where(x => quanHeIds.Contains(x.Id))
                .ToDictionary(x => x.Id, x => x.TenQuanHe);

            var result = thanhViens
                .Select(tv => new NsQuanHeGiaDinhResponseDto
                {
                    HoTen = tv.HoTen,
                    DonViCongTac = tv.DonViCongTac,
                    NgaySinh = tv.NgaySinh,
                    NgheNghiep = tv.NgheNghiep,
                    QueQuan = tv.QueQuan,
                    QuocTich = tv.QuocTich,
                    SoDienThoai = tv.SoDienThoai,
                    QuanHe = tv.QuanHe,
                    TenQuanHe = quanHeDict.ContainsKey(tv.QuanHe.Value)
                        ? quanHeDict[tv.QuanHe.Value]
                        : null,
                })
                .ToList();

            return result;
        }

        private bool HasFamilyInfo(CreateNhanSuDto dto) =>
            dto.ThongTinGiaDinh != null && dto.ThongTinGiaDinh.Any();

        private void AddGiaDinh(int idNhanSu, IEnumerable<CreateNsQuanHeGiaDinhDto> listGiaDinhDto)
        {
            var existedNames = _unitOfWork
                .iNsQuanHeGiaDinhRepository.TableNoTracking.Where(x => x.IdNhanSu == idNhanSu)
                .Select(x => x.HoTen)
                .ToHashSet();

            var newMembers = listGiaDinhDto
                .Where(x => !existedNames.Contains(x.HoTen))
                .Select(x => new NsQuanHeGiaDinh
                {
                    IdNhanSu = idNhanSu,
                    HoTen = x.HoTen,
                    NgaySinh = x.NgaySinh,
                    DonViCongTac = x.DonViCongTac,
                    QuanHe = x.QuanHe,
                    QuocTich = x.QuocTich,
                    SoDienThoai = x.SoDienThoai,
                    NgheNghiep = x.NgheNghiep,
                    QueQuan = x.QueQuan,
                })
                .ToList();

            if (!newMembers.Any())
                return;

            _unitOfWork.iNsQuanHeGiaDinhRepository.AddRange(newMembers);
            _unitOfWork.iNsQuanHeGiaDinhRepository.SaveChange();
        }

        private (
            Dictionary<int, string> pbDict,
            Dictionary<int, string> cvDict
        ) GetPhongBanChucVuDict(IEnumerable<NsNhanSuEntity> list)
        {
            var pbIds = list.Where(x => x.HienTaiPhongBan.HasValue)
                .Select(x => x.HienTaiPhongBan!.Value)
                .Distinct()
                .ToList();

            var cvIds = list.Where(x => x.HienTaiChucVu.HasValue)
                .Select(x => x.HienTaiChucVu!.Value)
                .Distinct()
                .ToList();

            Dictionary<int, string> pbDict = new();
            Dictionary<int, string> cvDict = new();

            if (pbIds.Any())
            {
                pbDict = _unitOfWork
                    .iDmPhongBanRepository.TableNoTracking.Where(x => pbIds.Contains(x.Id))
                    .ToDictionary(x => x.Id, x => x.TenPhongBan);
            }

            if (cvIds.Any())
            {
                cvDict = _unitOfWork
                    .iDmChucVuRepository.TableNoTracking.Where(x => cvIds.Contains(x.Id))
                    .ToDictionary(x => x.Id, x => x.TenChucVu);
            }

            return (pbDict, cvDict);
        }

        private string GetTrangThaiText(NsNhanSuEntity x)
        {
            return x.Status == false ? "Vô hiệu hóa"
                : x.IsThoiViec == true ? "Thôi việc"
                : x.DaVeHuu == true ? "Nghỉ hưu"
                : x.DaChamDutHopDong == true ? "Chấm dứt HĐ"
                : "Đang hoạt động";
        }

        private string MapRoleToChucVu(string role)
        {
            return role switch
            {
                "HIEU_TRUONG" => "Hiệu trưởng",
                "PHO_HIEU_TRUONG" => "Phó Hiệu trưởng",
                "TRUONG_DON_VI_CAP_2" => "Trưởng đơn vị",
                "GIANG_VIEN" => "Giảng viên",
                "CHUYEN_VIEN" => "Chuyên viên",
                _ => role
            };
        }

        #endregion
    }
}
