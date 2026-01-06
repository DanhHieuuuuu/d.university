using System.Text.Json;
using AutoMapper;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmDanToc;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmGioiTinh;
using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoaHoc;
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
using Microsoft.Extensions.Logging;

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

            var query = _unitOfWork.iDmChucVuRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Keyword)
                || x.TenChucVu.ToLower().Contains(dto.Keyword.ToLower())
            );

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();
            ;

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

            var result = query
                .OrderBy(x => x.STT)
                .ThenBy(x => x.Id)
                .Select(x => new DmPhongBanResponseDto
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

            var query = _unitOfWork
                .iDmQuocTichRepository.TableNoTracking.OrderBy(x => x.STT == null)
                .ThenBy(x => x.STT);
            ;

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
                || x.TenBoMon.ToLower().Contains(dto.Keyword.ToLower())
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

        public PageResultDto<DmKhoaHocResponseDto> GetAllDmKhoaHoc(DmKhoaHocRequestDto dto)
        {
            _logger.LogInformation($"{nameof(GetAllDmKhoaHoc)} method called.");

            var query = _unitOfWork.iDmKhoaHocRepository.TableNoTracking.AsQueryable();
            if (!string.IsNullOrWhiteSpace(dto.Keyword))
            {
                var keyword = dto.Keyword.Trim();

                query = query.Where(x =>
                    x.MaKhoaHoc.Contains(keyword) || x.TenKhoaHoc.Contains(keyword)
                );
            }

            var totalCount = query.Count();
            var items = query.ToList();

            return new PageResultDto<DmKhoaHocResponseDto>
            {
                Items = _mapper.Map<List<DmKhoaHocResponseDto>>(items),
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

        public void UpdateDmChucVu(UpdateDmChucVuDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateDmChucVu)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmChucVuRepository.TableNoTracking.FirstOrDefault(x =>
                x.Id == dto.Id
            );
            var existMaChucVu = _unitOfWork.iDmChucVuRepository.TableNoTracking.Any(x =>
                x.MaChucVu == dto.MaChucVu! && x.Id != dto.Id
            );

            if (exist == null)
            {
                throw new Exception($"Không tìm thấy chức vụ này.");
            }
            else if (existMaChucVu)
            {
                throw new Exception($"Đã tồn tại mã chức vụ \"{dto.MaChucVu}\" trogn csdl.");
            }
            else
            {
                exist.MaChucVu = dto.MaChucVu;
                exist.TenChucVu = dto.TenChucVu;
                exist.HsChucVu = dto.HsChucVu;
                exist.HsTrachNhiem = dto.HsTrachNhiem;

                _unitOfWork.iDmChucVuRepository.Update(exist);
                _unitOfWork.iDmChucVuRepository.SaveChange();
            }
        }

        public void DeleteDmChucVu(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDmChucVu)} method called. Dto: {id}");

            var exist = _unitOfWork.iDmChucVuRepository.FindById(id);

            if (exist != null)
            {
                _unitOfWork.iDmChucVuRepository.Delete(exist);
                _unitOfWork.iDmChucVuRepository.SaveChange();
            }
            else
            {
                throw new Exception($"Chức vụ không tồn tại hoặc đã bị xóa");
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

        public void UpdateDmPhongBan(UpdateDmPhongBanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateDmPhongBan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmPhongBanRepository.TableNoTracking.FirstOrDefault(x =>
                x.Id == dto.Id
            );
            var existMaPhongBan = _unitOfWork.iDmPhongBanRepository.TableNoTracking.Any(x =>
                x.MaPhongBan == dto.MaPhongBan! && x.Id != dto.Id
            );

            if (exist == null)
            {
                throw new Exception($"Không tìm thấy phòng ban này.");
            }
            else if (existMaPhongBan)
            {
                throw new Exception($"Đã tồn tại mã phòng ban \"{dto.MaPhongBan}\" trogn csdl.");
            }
            else
            {
                exist.MaPhongBan = dto.MaPhongBan;
                exist.TenPhongBan = dto.TenPhongBan;
                exist.IdLoaiPhongBan = dto.IdLoaiPhongBan;
                exist.DiaChi = dto.DiaChi;
                exist.Hotline = dto.Hotline;
                exist.Fax = dto.Fax;
                exist.NgayThanhLap = dto.NgayThanhLap;
                exist.STT = dto.STT;
                exist.ChucVuNguoiDaiDien = dto.ChucVuNguoiDaiDien;
                exist.NguoiDaiDien = dto.NguoiDaiDien;

                _unitOfWork.iDmPhongBanRepository.Update(exist);
                _unitOfWork.iDmPhongBanRepository.SaveChange();
            }
        }

        public void DeleteDmPhongBan(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDmPhongBan)} method called. Dto: {id}");

            var exist = _unitOfWork.iDmPhongBanRepository.FindById(id);

            if (exist != null)
            {
                _unitOfWork.iDmPhongBanRepository.Delete(exist);
                _unitOfWork.iDmPhongBanRepository.SaveChange();
            }
            else
            {
                throw new Exception($"Chức vụ không tồn tại hoặc đã bị xóa");
            }
        }

        #region To Bo Mon

        public void CreateDmToBoMon(CreateDmToBoMonDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDmToBoMon)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmToBoMonRepository.IsMaBoMonExist(dto.MaBoMon!);
            if (exist)
                throw new Exception($"Đã tồn tại tổ bộ môn có mã {dto.MaBoMon}");
            if (dto.IdPhongBan.HasValue)
            {
                var existPhongBan = _unitOfWork.iDmPhongBanRepository.TableNoTracking.Any(x =>
                    x.Id == dto.IdPhongBan.Value
                );
                if (!existPhongBan)
                    throw new Exception($"Không tìm thấy phòng ban có Id = {dto.IdPhongBan}");
            }
            var newToBoMon = _mapper.Map<DmToBoMon>(dto);
            _unitOfWork.iDmToBoMonRepository.Add(newToBoMon);
            _unitOfWork.iDmToBoMonRepository.SaveChange();
        }

        public void UpdateDmToBoMon(UpdateDmToBoMonDto dto)
        {
            _logger.LogInformation(
                $"{nameof(UpdateDmToBoMon)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmToBoMonRepository.TableNoTracking.FirstOrDefault(x =>
                x.Id == dto.Id
            );

            if (exist == null)
                throw new Exception($"Không tìm thấy tổ bộ môn có Id = {dto.Id}");

            var existMaBoMon = _unitOfWork.iDmToBoMonRepository.TableNoTracking.Any(x =>
                x.MaBoMon == dto.MaBoMon! && x.Id != dto.Id
            );

            if (existMaBoMon)
                throw new Exception($"Đã tồn tại tổ bộ môn có mã \"{dto.MaBoMon}\" trong CSDL.");

            if (dto.IdPhongBan.HasValue)
            {
                var existPhongBan = _unitOfWork.iDmPhongBanRepository.TableNoTracking.Any(x =>
                    x.Id == dto.IdPhongBan
                );

                if (!existPhongBan)
                    throw new Exception($"Không tìm thấy phòng ban có Id = {dto.IdPhongBan}");
            }

            exist.MaBoMon = dto.MaBoMon;
            exist.TenBoMon = dto.TenBoMon;
            exist.NgayThanhLap = dto.NgayThanhLap;
            exist.IdPhongBan = dto.IdPhongBan;
            _unitOfWork.iDmToBoMonRepository.Update(exist);
            _unitOfWork.iDmToBoMonRepository.SaveChange();
        }

        public void DeleteDmToBoMon(int id)
        {
            _logger.LogInformation($"{nameof(DeleteDmToBoMon)} method called. Dto: {id}");

            var exist = _unitOfWork.iDmToBoMonRepository.FindById(id);

            if (exist != null)
            {
                _unitOfWork.iDmToBoMonRepository.Delete(exist);
                _unitOfWork.iDmToBoMonRepository.SaveChange();
            }
            else
            {
                throw new Exception($"Tổ bộ môn không tồn tại hoặc đã bị xóa");
            }
        }

        public async Task<DmToBoMonResponseDto> GetDmToBoMonByIdAsync(int id)
        {
            _logger.LogInformation($"{nameof(GetDmToBoMonByIdAsync)} called with Id = {id}");
            var entity = _unitOfWork.iDmToBoMonRepository.FindById(id);
            if (entity == null)
                return null;
            var phongban = entity.IdPhongBan.HasValue
                ? _unitOfWork.iDmPhongBanRepository.FindById(entity.IdPhongBan.Value)
                : null;
            string? tenPhongBan = phongban?.TenPhongBan;
            var result = _mapper.Map<DmToBoMonResponseDto>(entity);
            result.PhongBan = tenPhongBan;
            return result;
        }

        #endregion

        public void CreateDmKhoaHoc(CreateDmKhoaHocDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateDmKhoaHoc)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iDmKhoaHocRepository.IsMaKhoaHocExist(dto.MaKhoaHoc!);

            if (exist)
            {
                throw new Exception($"Đã có khóa với mã {dto.MaKhoaHoc}");
            }
            else
            {
                var newKhoaHoc = _mapper.Map<DmKhoaHoc>(dto);

                _unitOfWork.iDmKhoaHocRepository.Add(newKhoaHoc);
                _unitOfWork.iDmKhoaHocRepository.SaveChange();
            }
        }

        public async Task<DmChucVuResponseDto> GetDmChucVuByIdAsync(int id)
        {
            _logger.LogInformation($"{nameof(GetDmChucVuByIdAsync)} method called. Id = {id}");

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

        public async Task<DmKhoaHocResponseDto> GetDmKhoaHocByIdAsync(int id)
        {
            var entity = _unitOfWork.iDmKhoaHocRepository.FindById(id);
            if (entity == null)
                return null;
            return _mapper.Map<DmKhoaHocResponseDto>(entity);
        }
    }
}
