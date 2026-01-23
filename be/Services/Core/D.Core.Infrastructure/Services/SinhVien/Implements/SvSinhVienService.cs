using AutoMapper;
using AutoMapper.QueryableExtensions;
using d.Shared.Permission.Auth;
using d.Shared.Permission.Error;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhung;
using D.Core.Domain.Dtos.DaoTao.ChuongTrinhKhungMon;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Dtos.SinhVien.Auth;
using D.Core.Domain.Dtos.SinhVien.ThongTinChiTiet;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Services.DaoTao.Abstracts;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace D.Core.Infrastructure.Services.SinhVien.Implements
{
    public class SvSinhVienService : ServiceBase, ISvSinhVienService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private IConfiguration _configuration;
        private readonly IDatabase _database;
        private readonly IDaoTaoService _daoTaoService;

        public SvSinhVienService(
            ILogger<SvSinhVienService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            IConfiguration configuration,
            IDatabase database,
            IDaoTaoService daoTaoService
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _database = database;
            _daoTaoService = daoTaoService;
        }

        public PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingSvSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking
            .Where(x => string.IsNullOrEmpty(dto.Keyword)
                     || x.Mssv.Contains(dto.Keyword)
                     || x.Ten.Contains(dto.Keyword));

            if (dto.Khoa.HasValue) query = query.Where(x => x.Khoa == dto.Khoa);
            if (dto.Nganh.HasValue) query = query.Where(x => x.Nganh == dto.Nganh);
            if (dto.KhoaHoc.HasValue) query = query.Where(x => x.KhoaHoc == dto.KhoaHoc);

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            var totalCount = query.Count();

            return new PageResultDto<SvSinhVienResponseDto> { Items = items, TotalItem = totalCount };
        }

        public PageResultDto<SvSinhVienGetAllResponseDto> GetAllSinhVien(SvSinhVienGetAllRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking;

            if (dto.Khoa.HasValue) query = query.Where(x => x.Khoa == dto.Khoa);
            if (dto.Nganh.HasValue) query = query.Where(x => x.Nganh == dto.Nganh);
            if (dto.KhoaHoc.HasValue) query = query.Where(x => x.KhoaHoc == dto.KhoaHoc);

            var items = query
                .ProjectTo<SvSinhVienGetAllResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PageResultDto<SvSinhVienGetAllResponseDto>
            {
                Items = items,
                TotalItem = query.Count()
            };
        }


        public async Task<SvSinhVienResponseDto> CreateSinhVien(CreateSinhVienDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iSvSinhVienRepository.IsSoCccdExits(dto.SoCccd!);

            if (exist)
                throw new Exception("Sinh viên đã tồn tại trong hệ thống (trùng Số CCCD).");

            var newSv = _mapper.Map<SvSinhVien>(dto);

            newSv.Mssv = await _unitOfWork.iSvSinhVienRepository.GenerateMssv(dto.KhoaHoc!.Value);
            newSv.Email2 = _unitOfWork.iSvSinhVienRepository.GenerateEmail(newSv.Mssv!);

            string rawPassword = newSv.NgaySinh?.ToString("ddMMyyyy") ?? newSv.Mssv!;
            
            var (hash, salt) = PasswordHelper.HashPassword(rawPassword);
            newSv.Password = hash;
            newSv.PasswordKey = salt;

            await _unitOfWork.iSvSinhVienRepository.AddAsync(newSv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();

            return _mapper.Map<SvSinhVienResponseDto>(newSv);
        }

        public async Task<bool> UpdateSinhVien(UpdateSinhVienDto dto)
        {
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) throw new Exception("Không tìm thấy sinh viên.");

            _mapper.Map(dto, sv);

            _unitOfWork.iSvSinhVienRepository.Update(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteSinhVien(DeleteSinhVienDto dto)
        {
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) throw new Exception("Không tìm thấy sinh viên.");

            _unitOfWork.iSvSinhVienRepository.Delete(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<SvSinhVienResponseDto> FindByMssv(FindByMssvDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindByMssv)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var result = await _unitOfWork.iSvSinhVienRepository.TableNoTracking
                .Where(x => x.Mssv == dto.Mssv)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (result == null)
                throw new Exception($"Không tìm thấy sinh viên có mssv: {dto.Mssv}");

            return result;
        }

        public async Task<SvThongTinChiTietResponseDto> GetThongTinChiTiet(SvThongTinChiTietRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetThongTinChiTiet)} method called. Mssv: {dto.Mssv}");
            
            var result = new SvThongTinChiTietResponseDto();
            int sinhVienId;
            int? gioiTinhId, khoaHocId, khoaId, nganhId, chuyenNganhId, trangThaiHoc;
            string? mssv, hoDem, ten, email, email2, soDienThoai, noiOHienTai;
            DateTime? ngaySinh;

            // Step 1: Query sinh vien using raw SQL to avoid mapping issues
            try
            {
                var svData = await _unitOfWork.iSvSinhVienRepository.TableNoTracking
                    .Where(x => x.Mssv == dto.Mssv)
                    .Select(x => new 
                    {
                        x.Id,
                        x.Mssv,
                        x.HoDem,
                        x.Ten,
                        x.NgaySinh,
                        x.Email,
                        x.Email2,
                        x.SoDienThoai,
                        x.NoiOHienTai,
                        x.GioiTinh,
                        x.KhoaHoc,
                        x.Khoa,
                        x.Nganh,
                        x.ChuyenNganh,
                        x.TrangThaiHoc
                    })
                    .FirstOrDefaultAsync();

                if (svData == null)
                    throw new Exception($"Không tìm thấy sinh viên có mssv: {dto.Mssv}");

                sinhVienId = svData.Id;
                mssv = svData.Mssv;
                hoDem = svData.HoDem;
                ten = svData.Ten;
                ngaySinh = svData.NgaySinh;
                email = svData.Email;
                email2 = svData.Email2;
                soDienThoai = svData.SoDienThoai;
                noiOHienTai = svData.NoiOHienTai;
                gioiTinhId = svData.GioiTinh;
                khoaHocId = svData.KhoaHoc;
                khoaId = svData.Khoa;
                nganhId = svData.Nganh;
                chuyenNganhId = svData.ChuyenNganh;
                trangThaiHoc = svData.TrangThaiHoc;
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 1 - Query SinhVien] {ex.Message}", ex);
            }

            // Step 2: Query GioiTinh
            string? gioiTinhStr = null;
            try
            {
                if (gioiTinhId.HasValue)
                {
                    var gioiTinhEntity = await _unitOfWork.iDmGioiTinhRepository.TableNoTracking
                        .FirstOrDefaultAsync(x => x.Id == gioiTinhId.Value);
                    gioiTinhStr = gioiTinhEntity?.TenGioiTinh;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 2 - Query GioiTinh] {ex.Message}", ex);
            }

            // Step 3: Query KhoaHoc
            string? khoaHocStr = null;
            try
            {
                if (khoaHocId.HasValue)
                {
                    var khoaHocEntity = await _unitOfWork.iDmKhoaHocRepository.TableNoTracking
                        .FirstOrDefaultAsync(x => x.Id == khoaHocId.Value);
                    khoaHocStr = khoaHocEntity?.TenKhoaHoc;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 3 - Query KhoaHoc] {ex.Message}", ex);
            }

            // Step 4: Query ThongTinHocVu
            SvThongTinHocVu? thongTinHocVu = null;
            try
            {
                thongTinHocVu = await _unitOfWork.iSvThongTinHocVuRepository.TableNoTracking
                    .FirstOrDefaultAsync(x => x.SinhVienId == sinhVienId);
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 4 - Query ThongTinHocVu] {ex.Message}", ex);
            }

            result.SinhVien = new ThongTinSinhVienDto
            {
                MaSinhVien = mssv,
                HoTen = $"{hoDem} {ten}".Trim(),
                NgaySinh = ngaySinh,
                GioiTinh = gioiTinhStr,
                Email = email ?? email2,
                SoDienThoai = soDienThoai,
                DiaChi = noiOHienTai,
                KhoaHoc = khoaHocStr,
                HocKyHienTai = thongTinHocVu?.HocKyHienTai,
                TinhTrang = trangThaiHoc == 1 ? "Đang học" : "Khác"
            };

            // Step 5: Query Khoa
            try
            {
                if (khoaId.HasValue)
                {
                    var khoa = await _unitOfWork.iDtKhoaRepository.TableNoTracking
                        .FirstOrDefaultAsync(x => x.Id == khoaId.Value);
                    if (khoa != null)
                    {
                        result.Khoa = new ThongTinKhoaDto
                        {
                            MaKhoa = khoa.MaKhoa,
                            TenKhoa = khoa.TenKhoa,
                            MoTa = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 5 - Query Khoa] {ex.Message}", ex);
            }

            // Step 6: Query Nganh
            try
            {
                if (nganhId.HasValue)
                {
                    var nganh = await _unitOfWork.iDtNganhRepository.TableNoTracking
                        .FirstOrDefaultAsync(x => x.Id == nganhId.Value);
                    if (nganh != null)
                    {
                        result.Nganh = new ThongTinNganhDto
                        {
                            MaNganh = nganh.MaNganh,
                            TenNganh = nganh.TenNganh,
                            SoTinChiToiThieu = null,
                            ThoiGianDaoTao = "4 năm",
                            MoTa = nganh.MoTa
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 6 - Query Nganh] {ex.Message}", ex);
            }

            // Step 7: Query ChuyenNganh
            try
            {
                if (chuyenNganhId.HasValue)
                {
                    var chuyenNganh = await _unitOfWork.iDtChuyenNganhRepository.TableNoTracking
                        .FirstOrDefaultAsync(x => x.Id == chuyenNganhId.Value);
                    if (chuyenNganh != null)
                    {
                        result.ChuyenNganh = new ThongTinChuyenNganhDto
                        {
                            MaChuyenNganh = chuyenNganh.MaChuyenNganh,
                            TenChuyenNganh = chuyenNganh.TenChuyenNganh,
                            MoTa = chuyenNganh.MoTa
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 7 - Query ChuyenNganh] {ex.Message}", ex);
            }

            // Step 8: Query ChuongTrinhKhung using DaoTaoService
            try
            {
                if (nganhId.HasValue && chuyenNganhId.HasValue)
                {
                    // 8.1: Find ChuongTrinhKhung by NganhId and ChuyenNganhId
                    var ctkRequest = new DtChuongTrinhKhungRequestDto
                    {
                        NganhId = nganhId.Value,
                        ChuyenNganhId = chuyenNganhId.Value,
                        PageSize = 1
                    };
                    var ctkResult = await _daoTaoService.GetAllDtChuongTrinhKhung(ctkRequest);
                    var ctk = ctkResult.Items?.FirstOrDefault();

                    if (ctk != null)
                    {
                        _logger.LogInformation($"[Step 8] Found CTK: {ctk.MaChuongTrinhKhung}");

                        // 8.2: Get all ChuongTrinhKhungMon by ChuongTrinhKhungId
                        var ctkMonRequest = new DtChuongTrinhKhungMonRequestDto
                        {
                            ChuongTrinhKhungId = ctk.Id,
                            PageSize = 1000
                        };
                        var ctkMonResult = await _daoTaoService.GetAllDtChuongTrinhKhungMon(ctkMonRequest);
                        var ctkMons = ctkMonResult.Items ?? new List<DtChuongTrinhKhungMonResponseDto>();

                        // Get all MonHoc info
                        var monHocIds = ctkMons.Select(x => x.MonHocId).Distinct().ToList();
                        var monHocs = await _unitOfWork.iDtMonHocRepository.TableNoTracking
                            .Where(x => monHocIds.Contains(x.Id))
                            .Select(x => new { x.Id, x.MaMonHoc, x.TenMonHoc, x.SoTinChi })
                            .ToListAsync();

                        // Group by HocKy
                        var groupedByHocKy = ctkMons
                            .GroupBy(x => int.TryParse(x.HocKy, out var hk) ? hk : 0)
                            .OrderBy(x => x.Key);

                        foreach (var group in groupedByHocKy)
                        {
                            var hocKyDto = new ChuongTrinhKhungHocKyDto
                            {
                                HocKy = group.Key,
                                MonHoc = group.Select(ctkMon =>
                                {
                                    var monHoc = monHocs.FirstOrDefault(m => m.Id == ctkMon.MonHocId);
                                    return new MonHocKhungDto
                                    {
                                        MaMon = monHoc?.MaMonHoc,
                                        TenMon = monHoc?.TenMonHoc,
                                        SoTinChi = monHoc?.SoTinChi ?? 0,
                                        Loai = "Bắt buộc"
                                    };
                                }).ToList()
                            };
                            result.ChuongTrinhKhung.Add(hocKyDto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"[Step 8 - ChuongTrinhKhung] Error: {ex.Message}");
            }

            // Step 9: Query KetQuaHocKy
            try
            {
                var ketQuaHocKys = await _unitOfWork.iSvKetQuaHocKyRepository.TableNoTracking
                    .Where(x => x.SinhVienId == sinhVienId)
                    .OrderBy(x => x.HocKy)
                    .ToListAsync();

                var diemMonHocs = await _unitOfWork.iSvDiemMonHocRepository.TableNoTracking
                    .Where(x => x.SinhVienId == sinhVienId)
                    .ToListAsync();

                var allMonHocIds = diemMonHocs.Select(x => x.MonHocId).Distinct().ToList();
                var allMonHocs = await _unitOfWork.iDtMonHocRepository.TableNoTracking
                    .Where(x => allMonHocIds.Contains(x.Id))
                    .Select(x => new { x.Id, x.MaMonHoc, x.TenMonHoc })
                    .ToListAsync();

                foreach (var ketQua in ketQuaHocKys)
                {
                    var diemKyDto = new DiemCacKyDto
                    {
                        HocKy = ketQua.HocKy,
                        NamHoc = ketQua.NamHoc,
                        DiemTrungBinhHocKy = ketQua.DiemTrungBinhHocKy,
                        DiemTrungBinhTichLuy = ketQua.DiemTrungBinhTinhLuy,
                        SoTinChiDat = ketQua.SoTinChiDat,
                        SoTinChiTichLuy = ketQua.SoTinChiTichLuy,
                        XepLoaiHocKy = ketQua.XepLoaiHocKy,
                        DiemMon = diemMonHocs
                            .Where(d => d.HocKy == ketQua.HocKy && d.NamHoc == ketQua.NamHoc)
                            .Select(d =>
                            {
                                var monHoc = allMonHocs.FirstOrDefault(m => m.Id == d.MonHocId);
                                return new DiemMonDto
                                {
                                    MaMon = monHoc?.MaMonHoc,
                                    TenMon = monHoc?.TenMonHoc,
                                    DiemQuaTrinh = d.DiemQuaTrinh,
                                    DiemCuoiKy = d.DiemCuoiKy,
                                    DiemTongKet = d.DiemTongKet,
                                    DiemHe4 = d.DiemHe4,
                                    DiemChu = d.DiemChu,
                                    KetQua = d.KetQua
                                };
                            }).ToList()
                    };
                    result.DiemCacKy.Add(diemKyDto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 9 - Query KetQuaHocKy/DiemMonHoc] {ex.Message}", ex);
            }

            // Step 10: ThongTinHocVu
            result.ThongTinHocVu = new ThongTinHocVuDto
            {
                GpaHienTai = thongTinHocVu?.GpaHienTai ?? 0,
                XepLoaiHocLuc = thongTinHocVu?.XepLoaiHocLuc,
                SoMonNo = thongTinHocVu?.SoTinChiNo ?? 0,
                CanhBaoHocVu = thongTinHocVu?.CanhBaoHocVu ?? false
            };

            // Step 11: Query QuyDinhThangDiem
            try
            {
                var thangDiem = await _unitOfWork.iDtQuyDinhThangDiemRepository.TableNoTracking
                    .Where(x => x.TrangThai)
                    .OrderByDescending(x => x.DiemSoMin)
                    .Select(x => new { x.DiemChu, x.DiemSoMin, x.DiemSoMax, x.DiemHe4 })
                    .ToListAsync();

                result.QuyDinhThangDiem = thangDiem.Select(x => new QuyDinhThangDiemDto
                {
                    DiemChu = x.DiemChu,
                    DiemSoMin = x.DiemSoMin,
                    DiemSoMax = x.DiemSoMax,
                    DiemHe4 = x.DiemHe4
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[Step 11 - Query QuyDinhThangDiem] {ex.Message}", ex);
            }

            return result;
        }


        #region auth
        public async Task<SvLoginResponseDto> Login(SvLoginRequestDto loginRequest)
        {
            _logger.LogInformation($"{nameof(Login)} method called. Dto: {JsonSerializer.Serialize(loginRequest)}");

            // 1. Tìm sinh viên theo MSSV
            var sv = await _unitOfWork.iSvSinhVienRepository.GetByMssv(loginRequest.Mssv);

            if (sv == null)
            {
                throw new UserFriendlyException(ErrorCodeConstant.PasswordOrCodeWrong, "Không đúng mật khẩu hoặc tài khoản.");
            }

            // 2. Check mật khẩu
            if (string.IsNullOrEmpty(sv.Password) || string.IsNullOrEmpty(sv.PasswordKey))
            {
                throw new UserFriendlyException(500, "Tài khoản chưa được thiết lập mật khẩu.");
            }

            if (!PasswordHelper.VerifyPassword(loginRequest.Password, sv.Password, sv.PasswordKey))
            {
                throw new UserFriendlyException(ErrorCodeConstant.PasswordWrong, "Mật khẩu không đúng.");
            }

            // 3. Tạo Token
            SvLoginResponseDto result = new SvLoginResponseDto();
            result.User = _mapper.Map<SvSinhVienResponseDto>(sv);

            DateTime date = DateTime.Now;

            result.Token = GenerateToken(sv);
            result.ExpiredToken = date.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"]));

            // Lưu Access Token vào Redis
            await SaveAccessTokenAsync(result.Token, sv.Id, TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])));

            result.RefreshToken = GenerateRefreshToken();
            result.ExpiredRefreshToken = date.AddDays(7);

            // Lưu Refresh Token vào Redis
            await SaveRefreshTokenAsync(result.Token, result.RefreshToken, TimeSpan.FromDays(7));

            return result;
        }

        public async Task<SvRefreshTokenResponseDto> RefreshToken(SvRefreshTokenRequestDto refreshToken)
        {
            _logger.LogInformation($"{nameof(RefreshToken)} method called.");

            var principal = GetPrincipalFromExpiredToken(refreshToken.Token);
            var claim = principal?.FindFirst(CustomClaimType.UserId);

            if (claim == null) throw new UserFriendlyException(ErrorCode.Unauthorized, "Token không hợp lệ.");

            var checkRefresh = await ValidateRefreshTokenAsync(refreshToken.Token, refreshToken.RefreshToken);
            if (!checkRefresh)
                throw new UserFriendlyException(ErrorCodeConstant.RefreshTokenNotFound, "Refresh token không đúng hoặc đã hết hạn.");

            var sv = _unitOfWork.iSvSinhVienRepository.FindById(int.Parse(claim.Value));

            DateTime date = DateTime.Now;
            string newToken = GenerateToken(sv);
            string newRefreshToken = GenerateRefreshToken();

            await SaveAccessTokenAsync(newToken, sv.Id, TimeSpan.FromMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])));
            await SaveRefreshTokenAsync(newToken, newRefreshToken, TimeSpan.FromDays(7));

            return new SvRefreshTokenResponseDto
            {
                Token = newToken,
                ExpiredToken = date.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                RefreshToken = newRefreshToken,
                ExpiredRefreshToken = date.AddDays(7)
            };
        }

        public async Task<bool> Logout(SvLogoutRequestDto logoutRequestDto)
        {
            var token = CommonUntil.GetToken(_contextAccessor);
            await DeleteTokenAsync(token);
            await DeleteTokenAsync(token, true);
            return true;
        }

        private string GenerateToken(SvSinhVien sv)
        {
            var claims = new[]
            {
            new Claim(CustomClaimType.UserId, $"{sv.Id}"),
            new Claim(CustomClaimType.FirstName, sv.HoDem ?? ""),
            new Claim(CustomClaimType.LastName, sv.Ten ?? ""),
            new Claim(CustomClaimType.DateOfBirth, sv.NgaySinh?.ToString() ?? ""),
            // Sinh viên type 4
            new Claim(CustomClaimType.UserType, UserTypeConstant.STUDENT.ToString()), 
            // Thêm MSSV vào claim 
            new Claim("Mssv", sv.Mssv ?? "")
            
        };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiryMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        private async Task SaveAccessTokenAsync(string token, int userId, TimeSpan expiry)
            => await _database.StringSetAsync($"token:{token}", userId.ToString(), expiry);

        private async Task SaveRefreshTokenAsync(string token, string refreshToken, TimeSpan expiry)
            => await _database.StringSetAsync($"refreshToken:{token}", refreshToken, expiry);

        private async Task<bool> ValidateRefreshTokenAsync(string token, string refreshToken)
        {
            string key = $"refreshToken:{token}";
            var value = await _database.StringGetAsync(key);
            return value.HasValue && value.ToString() == refreshToken;
        }

        private async Task DeleteTokenAsync(string token, bool isRefresh = false)
        {
            string key = isRefresh ? $"refreshToken:{token}" : $"token:{token}";
            await _database.KeyDeleteAsync(key);
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }
            return principal;
        }

        #endregion
    }
}
