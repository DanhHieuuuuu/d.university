using AutoMapper;
using AutoMapper.QueryableExtensions;
using D.Core.Domain.Dtos.SinhVien;
using D.Core.Domain.Entities.SinhVien;
using D.Core.Infrastructure.Services.SinhVien.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.SinhVien.Implements
{
    public class SvSinhVienService : ServiceBase, ISvSinhVienService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public SvSinhVienService(
            ILogger<SvSinhVienService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<SvSinhVienResponseDto> FindPagingSvSinhVien(SvSinhVienRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindPagingSvSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Mssv) || dto.Mssv == x.Mssv
            );

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ProjectTo<SvSinhVienResponseDto>(_mapper.ConfigurationProvider)
                .ToList();

            return new PageResultDto<SvSinhVienResponseDto> { Items = items, TotalItem = totalCount };
        }

        public PageResultDto<SvSinhVienGetAllResponseDto> GetAllSinhVien(SvSinhVienGetAllRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iSvSinhVienRepository.TableNoTracking.Where(x =>
                string.IsNullOrEmpty(dto.Mssv) || x.Mssv == dto.Mssv
            );

            var totalCount = query.Count();

            var items = query
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            var result = items
                .Select(x => new SvSinhVienGetAllResponseDto
                {
                    IdStudent = x.Id,
                    Mssv = x.Mssv,
                    HoDem = x.HoDem,
                    Ten = x.Ten,
                    NgaySinh = x.NgaySinh,
                    NoiSinh = x.NoiSinh,
                    SoCccd = x.SoCccd,
                    SoDienThoai = x.SoDienThoai,
                    Email = x.Email,
                    Khoa = x.Khoa,
                    //NganhHoc = x.IdNganh.HasValue
                    //    ? _unitOfWork.iDmNganhRepository.FindById(x.IdNganh.Value)?.TenNganh
                    //    : null,
                    TrangThai = x.TrangThaiHoc switch
                    {
                        0 => "Đang học",
                        1 => "Bảo lưu",
                        2 => "Đã tốt nghiệp",
                        _ => "Không xác định"
                    }
                })
                .ToList();

            return new PageResultDto<SvSinhVienGetAllResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }


        public SvSinhVienResponseDto CreateSinhVien(CreateSinhVienDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateSinhVien)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var exist = _unitOfWork.iSvSinhVienRepository.IsSoCccdExits(dto.SoCccd!);

            if (exist)
                throw new Exception("Sinh viên đã tồn tại trong hệ thống (trùng Số CCCD).");

            var newSv = _mapper.Map<SvSinhVien>(dto);

            newSv.Mssv = _unitOfWork.iSvSinhVienRepository.GenerateMssv(dto.Khoa!.Value);
            newSv.Email2 = _unitOfWork.iSvSinhVienRepository.GenerateEmail(newSv.Mssv!);
            _unitOfWork.iSvSinhVienRepository.AddAsync(newSv);
            _unitOfWork.iSvSinhVienRepository.SaveChange();

            return _mapper.Map<SvSinhVienResponseDto>(newSv);
        }


        public async Task<bool> UpdateSinhVien(UpdateSinhVienDto dto)
        {
            var sv = _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null) return false;

            sv.HoDem = dto.HoDem;
            sv.Ten = dto.Ten;
            sv.NgaySinh = dto.NgaySinh;
            sv.NoiSinh = dto.NoiSinh;
            sv.GioiTinh = dto.GioiTinh;
            sv.QuocTich = dto.QuocTich;
            sv.DanToc = dto.DanToc;
            sv.SoCccd = dto.SoCccd;
            sv.SoDienThoai = dto.SoDienThoai;

            _unitOfWork.iSvSinhVienRepository.Update(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public async Task<bool> DeleteSinhVien(DeleteSinhVienDto dto)
        {
            var sv = _unitOfWork.iSvSinhVienRepository.GetByMssv(dto.Mssv!);
            if (sv == null)
                return false;

            _unitOfWork.iSvSinhVienRepository.Delete(sv);
            await _unitOfWork.iSvSinhVienRepository.SaveChangeAsync();
            return true;
        }

        public SvSinhVienResponseDto FindByMssv(FindByMssvDto dto)
        {
            _logger.LogInformation(
                $"{nameof(FindByMssv)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var sv = _unitOfWork.iSvSinhVienRepository.TableNoTracking.FirstOrDefault(x =>
                string.IsNullOrEmpty(dto.Keyword)
                || dto.Keyword == x.Mssv
            );

            var result = new SvSinhVienResponseDto
            {
                IdSinhVien = sv.Id,
                Mssv = sv.Mssv,
                HoDem = sv.HoDem,
                Ten = sv.Ten,
                NgaySinh = sv.NgaySinh,
                NoiSinh = sv.NoiSinh,
                SoCccd = sv.SoCccd,
                SoDienThoai = sv.SoDienThoai,
                Email = sv.Email,
                Khoa = sv.Khoa,
                //NganhHoc = sv.IdNganh.HasValue
                //    ? _unitOfWork.iDmNganhRepository.FindById(sv.IdNganh.Value)?.TenNganh
                //    : null,
            };

            return result;
        }

    }
}
