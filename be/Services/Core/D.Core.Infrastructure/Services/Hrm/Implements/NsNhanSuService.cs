using System.Text.Json;
using AutoMapper;
using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NsNhanSuService : ServiceBase, INsNhanSuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public NsNhanSuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingNsNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iNsNhanSuRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Cccd) || dto.Cccd == x.SoCccd
            );

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

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
                    HoTen = x.HoDem + " " + x.Ten,
                    SoCccd = x.SoCccd,
                    TenPhongBan = x.HienTaiPhongBan.HasValue
                        ? _unitOfWork
                            .iDmPhongBanRepository.FindById(x.HienTaiPhongBan.Value)
                            ?.TenPhongBan
                        : null,
                    TenChucVu = x.HienTaiChucVu.HasValue
                        ? _unitOfWork.iDmChucVuRepository.FindById(x.HienTaiChucVu.Value)?.TenChucVu
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
                $"{nameof(FindPagingNsNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iNsNhanSuRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.MaNhanSu) || x.MaNhanSu == dto.MaNhanSu
            );

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            // Map dữ liệu trả về
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
                    TenPhongBan = x.HienTaiPhongBan.HasValue
                        ? _unitOfWork
                            .iDmPhongBanRepository.FindById(x.HienTaiPhongBan.Value)
                            ?.TenPhongBan
                        : null,
                    TenChucVu = x.HienTaiChucVu.HasValue
                        ? _unitOfWork.iDmChucVuRepository.FindById(x.HienTaiChucVu.Value)?.TenChucVu
                        : null,
                    TrangThai =
                        x.IsThoiViec == true ? "Thôi việc"
                        : x.DaVeHuu == true ? "Nghỉ hưu"
                        : x.DaChamDutHopDong == true ? "Chấm dứt HĐ"
                        : "Đang hoạt động",
                })
                .ToList();

            return new PageResultDto<NsNhanSuGetAllResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
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
            {
                //throw new Exception("Đã tồn tại trong db");
                return;
            }
            else
            {
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
        }

        public NsNhanSuResponseDto CreateNhanSu(CreateNhanSuDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateNhanSu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iNsNhanSuRepository.IsMaNhanSuExist(dto.MaNhanSu!);

            if (exist)
            {
                throw new Exception("Đã tồn tại mã nhân sự này trong CSDL. Vui lòng nhập mã khác.");
            }
            else
            {
                var newNhanSu = _mapper.Map<NsNhanSu>(dto);

                _unitOfWork.iNsNhanSuRepository.Add(newNhanSu);
                _unitOfWork.iNsNhanSuRepository.SaveChange();

                if (dto.ThongTinGiaDinh != null && dto.ThongTinGiaDinh.Count > 0)
                {
                    // Lấy danh sách thành viên đã tồn tại của Nhân sự này
                    var existed = _unitOfWork
                        .iNsQuanHeGiaDinhRepository.TableNoTracking.Where(x =>
                            x.IdNhanSu == newNhanSu.Id
                        )
                        .Select(x => x.HoTen)
                        .ToHashSet();

                    // Lọc ra những thành viên chưa có trong DB
                    var listGiaDinh = dto
                        .ThongTinGiaDinh.Where(thanhvien => !existed.Contains(thanhvien.HoTen))
                        .Select(thanhvien => new NsQuanHeGiaDinh
                        {
                            IdNhanSu = newNhanSu.Id,
                            HoTen = thanhvien.HoTen,
                            NgaySinh = thanhvien.NgaySinh,
                            DonViCongTac = thanhvien.DonViCongTac,
                            QuanHe = thanhvien.QuanHe,
                            QuocTich = thanhvien.QuocTich,
                            SoDienThoai = thanhvien.SoDienThoai,
                            NgheNghiep = thanhvien.NgheNghiep,
                            QueQuan = thanhvien.QueQuan,
                        })
                        .ToList();

                    _unitOfWork.iNsQuanHeGiaDinhRepository.AddRange(listGiaDinh);
                    _unitOfWork.iNsQuanHeGiaDinhRepository.SaveChange();
                }

                return _mapper.Map<NsNhanSuResponseDto>(newNhanSu);
            }
        }

        public void CreateHopDong(CreateHopDongDto dto)
        {
            _logger.LogInformation(
                $"Method {nameof(CreateHopDong)} called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var newHd = _mapper.Map<NsHopDong>(dto);

            _unitOfWork.iNsHopDongRepository.Add(newHd);
            _unitOfWork.iNsHopDongRepository.SaveChange();

            if (dto.ThongTinNhanSu != null)
            {
                var newNhanSu = CreateNhanSu(dto.ThongTinNhanSu);

                var chiTietHopDong = new NsHopDongChiTiet
                {
                    IdHopDong = newHd.Id,
                    IdNhanSu = newNhanSu.IdNhanSu,
                    MaNhanSu = newNhanSu.MaNhanSu,
                    IdChucVu = dto.IdChucVu,
                    IdPhongBan = dto.IdPhongBan,
                    IdToBoMon = dto.IdToBoMon,
                    LuongCoBan = dto.LuongCoBan,
                    HsChucVu = _unitOfWork.iDmChucVuRepository.FindById(dto.IdChucVu).HsChucVu,
                    GhiChu = dto.GhiChu,
                };

                newHd.IdNhanSu = newNhanSu.IdNhanSu;

                _unitOfWork.iNsHopDongChiTietRepository.Add(chiTietHopDong);
                _unitOfWork.iNsHopDongChiTietRepository.SaveChange();
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

            var result = new NsNhanSuResponseDto
            {
                MaNhanSu = nhanSu.MaNhanSu,
                HoDem = nhanSu.HoDem,
                Ten = nhanSu.Ten,
                NgaySinh = nhanSu.NgaySinh,
                NoiSinh = nhanSu.NoiSinh,
                SoDienThoai = nhanSu.SoDienThoai,
                Email = nhanSu.Email,
                TenPhongBan = nhanSu.HienTaiPhongBan.HasValue
                        ? _unitOfWork
                            .iDmPhongBanRepository.FindById(nhanSu.HienTaiPhongBan.Value)
                            ?.TenPhongBan
                        : null,
                TenChucVu = nhanSu.HienTaiChucVu.HasValue
                        ? _unitOfWork.iDmChucVuRepository.FindById(nhanSu.HienTaiChucVu.Value)?.TenChucVu
                        : null,
            };


            return result;
        }
    }
}
