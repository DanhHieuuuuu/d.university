using AutoMapper;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmDanToc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmGioiTinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmLoaiHopDong;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuanHeGiaDinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmQuocTich;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmToBoMon;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmTonGiao;
using D.Core.Domain.Entities.Hrm.DanhMuc;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class DmDanhMucService : ServiceBase, IDmDanhMucService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public DmDanhMucService(
            ILogger<DmDanhMucService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<DmChucVuResponseDto> GetAllDmChucVu(DmChucVuRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmChucVu)} method called.");

            var query = _unitOfWork.iDmChucVuRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmChucVuResponseDto>
            {
                Items = _mapper.Map<List<DmChucVuResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmDanTocResponseDto> GetAllDmDanToc(DmDanTocRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmDanToc)} method called.");

            var query = _unitOfWork.iDmDanTocRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmDanTocResponseDto>
            {
                Items = _mapper.Map<List<DmDanTocResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmGioiTinhResponseDto> GetAllDmGioiTinh(DmGioiTinhRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmGioiTinh)} method called.");

            var query = _unitOfWork.iDmGioiTinhRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmGioiTinhResponseDto>
            {
                Items = _mapper.Map<List<DmGioiTinhResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmLoaiHopDongResponseDto> GetAllDmLoaiHopDong(
            DmLoaiHopDongRequestDto dto
        )
        {
            _logger.LogInformation($"{nameof(GetAllDmLoaiHopDong)} method called.");

            var query = _unitOfWork.iDmLoaiHopDongRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmLoaiHopDongResponseDto>
            {
                Items = _mapper.Map<List<DmLoaiHopDongResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmLoaiPhongBanResponseDto> GetAllDmLoaiPhongBan(
            DmLoaiPhongBanRequestDto dto
        )
        {
            _logger.LogInformation($"{nameof(GetAllDmLoaiPhongBan)} method called.");

            var query = _unitOfWork.iDmLoaiPhongBanRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmLoaiPhongBanResponseDto>
            {
                Items = _mapper.Map<List<DmLoaiPhongBanResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmPhongBanResponseDto> GetAllDmPhongBan(DmPhongBanRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllDmPhongBan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iDmPhongBanRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Keyword)
                || x.TenPhongBan.ToLower().Contains(dto.Keyword.ToLower())
            );

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            var result = query.Select(x => new DmPhongBanResponseDto
            {
                Id = x.Id,
                MaPhongBan = x.MaPhongBan,
                TenPhongBan = x.TenPhongBan,
                LoaiPhongBan = x.IdLoaiPhongBan.HasValue
                    ? _unitOfWork
                        .iDmLoaiPhongBanRepository.FindById(x.IdLoaiPhongBan.Value)
                        .TenLoaiPhongBan
                    : null,
                DiaChi = x.DiaChi,
                Hotline = x.Hotline,
                Fax = x.Fax,
                NgayThanhLap = x.NgayThanhLap,
                STT = x.STT,
                NguoiDaiDien = x.NguoiDaiDien,
                ChucVuNguoiDaiDien = x.ChucVuNguoiDaiDien,
            });

            return new PageResultDto<DmPhongBanResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmQuanHeGiaDinhResponseDto> GetAllDmQuanHeGiaDinh(
            DmQuanHeGiaDinhRequestDto dto
        )
        {
            _logger.LogInformation($"{nameof(GetAllDmQuanHeGiaDinh)} method called.");

            var query = _unitOfWork.iDmQuanHeGiaDinhRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmQuanHeGiaDinhResponseDto>
            {
                Items = _mapper.Map<List<DmQuanHeGiaDinhResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmQuocTichResponseDto> GetAllDmQuocTich(DmQuocTichRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmQuocTich)} method called.");

            var query = _unitOfWork.iDmQuocTichRepository.TableNoTracking.OrderBy(x => x.STT == null).ThenBy(x => x.STT); ;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmQuocTichResponseDto>
            {
                Items = _mapper.Map<List<DmQuocTichResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmToBoMonResponseDto> GetAllDmToBoMon(DmToBoMonRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmToBoMon)} method called.");

            var query = _unitOfWork.iDmToBoMonRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Keyword)
                || EF.Functions.Like(x.TenBoMon ?? "", $"%{dto.Keyword}%")
            );

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            var result = query.Select(x => new DmToBoMonResponseDto
            {
                Id = x.Id,
                MaBoMon = x.MaBoMon,
                TenBoMon = x.TenBoMon,
                PhongBan = x.IdPhongBan.HasValue
                    ? _unitOfWork.iDmPhongBanRepository.FindById(x.IdPhongBan.Value).TenPhongBan
                    : null,
                NgayThanhLap = x.NgayThanhLap,
            });

            return new PageResultDto<DmToBoMonResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmTonGiaoResponseDto> GetAllDmTonGiao(DmTonGiaoRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmTonGiao)} method called.");

            var query = _unitOfWork.iDmTonGiaoRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmTonGiaoResponseDto>
            {
                Items = _mapper.Map<List<DmTonGiaoResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public PageResultDto<DmKhoaResponseDto> GetAllDmKhoa(DmKhoaRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmKhoa)} method called.");

            var query = _unitOfWork.iDmKhoaRepository.TableNoTracking;

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmKhoaResponseDto>
            {
                Items = _mapper.Map<List<DmKhoaResponseDto>>(items),
                TotalItem = totalCount,
            };
        }

        public void CreateDmChucVu(CreateDmChucVuDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDmChucVu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmChucVuRepository.IsMaChucVuExist(dto.MaChucVu!);

            if (exist)
            {
                throw new Exception($"Đã có chức vụ với mã {dto.MaChucVu}");
            }
            else
            {
                var newChucVu = _mapper.Map<DmChucVu>(dto);

                _unitOfWork.iDmChucVuRepository.Add(newChucVu);
                _unitOfWork.iDmChucVuRepository.SaveChange();
            }
        }

        public void CreateDmPhongBan(CreateDmPhongBanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDmPhongBan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmPhongBanRepository.IsMaPhongBanExist(dto.MaPhongBan!);
            if (exist)
            {
                throw new Exception($"Đã tồn tại phòng ban có mã {dto.MaPhongBan}");
            }
            else
            {
                var newPhongBan = _mapper.Map<DmPhongBan>(dto);

                _unitOfWork.iDmPhongBanRepository.Add(newPhongBan);
                _unitOfWork.iDmPhongBanRepository.SaveChange();
            }
        }

        public void CreateDmToBoMon(CreateDmToBoMonDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDmToBoMon)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmToBoMonRepository.IsMaBoMonExist(dto.MaBoMon!);
            if (exist)
            {
                throw new Exception($"Đã tồn tại tổ bộ môn có mã {dto.MaBoMon}");
            }
            else
            {
                var newToBoMon = _mapper.Map<DmToBoMon>(dto);

                _unitOfWork.iDmToBoMonRepository.Add(newToBoMon);
                _unitOfWork.iDmToBoMonRepository.SaveChange();
            }
        }

        public void CreateDmKhoa(CreateDmKhoaDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDmKhoa)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmKhoaRepository.IsMaKhoaExist(dto.MaKhoa!);

            if (exist)
            {
                throw new Exception($"Đã có khóa với mã {dto.MaKhoa}");
            }
            else
            {
                var newKhoa = _mapper.Map<DmKhoa>(dto);

                _unitOfWork.iDmKhoaRepository.Add(newKhoa);
                _unitOfWork.iDmKhoaRepository.SaveChange();
            }
        }

        public async Task<DmChucVuResponseDto> GetDmChucVuByIdAsync(int id)
        {
            var entity = _unitOfWork.iDmChucVuRepository.FindById(id);
            if (entity == null)
                return null;
            return _mapper.Map<DmChucVuResponseDto>(entity);
        }

        public async Task<DmPhongBanResponseDto> GetDmPhongBanByIdAsync(int id)
        {
            var entity = _unitOfWork.iDmPhongBanRepository.FindById(id);
            if (entity == null)
                return null;
            return _mapper.Map<DmPhongBanResponseDto>(entity);
        }

        public async Task<DmKhoaResponseDto> GetDmKhoaByIdAsync(int id)
        {
            var entity = _unitOfWork.iDmKhoaRepository.FindById(id);
            if (entity == null)
                return null;
            return _mapper.Map<DmKhoaResponseDto>(entity);
        }
    }
}
