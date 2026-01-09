using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using D.Constants.Core.Hrm;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Hrm.HopDong;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using d.Shared.Permission.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NsContractService : ServiceBase, INsContractService
    {
        private readonly ServiceUnitOfWork _unitOfWork;
        private readonly INsDecisionService _decisionService;
        private readonly INsNhanSuService _nhansuService;

        public NsContractService(
            ILogger<NsContractService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork,
            INsDecisionService decisionService,
            INsNhanSuService nhansuService
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
            _decisionService = decisionService;
            _nhansuService = nhansuService;
        }

        public void CreateNewContract(CreateHopDongDto dto)
        {
            _logger.LogInformation(
                $"Method {nameof(CreateNewContract)} called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var nhansu = _unitOfWork.iNsNhanSuRepository.Table.FirstOrDefault(x =>
                x.Id == dto.IdNhanSu
            );

            if (nhansu != null)
            {
                ValidateMaNhanSu(dto.MaNhanSu!);

                ValidateSoHopDong(dto.SoHopDong!);

                // Tạo hợp đồng
                var newHd = _mapper.Map<NsHopDong>(dto);

                _unitOfWork.iNsHopDongRepository.Add(newHd);
                _unitOfWork.iNsHopDongRepository.SaveChange();

                var quyetDinhTuyenDung = _decisionService.TaoQuyetDinh(
                    new CreateNsQuyetDinhDto
                    {
                        IdNhanSu = dto.IdNhanSu,
                        MaNhanSu = nhansu.MaNhanSu,
                        NoiDungTomTat =
                            $"tuyển dụng nhân sự mới: {string.Join(" ", nhansu.HoDem, nhansu.Ten)}",
                        LoaiQuyetDinh = NsLoaiQuyetDinh.TiepNhan,
                        NgayHieuLuc = dto.HopDongCoThoiHanTuNgay,
                    }
                );

                _decisionService.PheDuyetQuyetDinh(quyetDinhTuyenDung.Id);

                _unitOfWork.iNsQuaTrinhCongTacRepository.Add(
                    new NsQuaTrinhCongTac
                    {
                        IdQuyetDinh = quyetDinhTuyenDung.Id,
                        IdNhanSu = dto.IdNhanSu,
                        MaNhanSu = nhansu.MaNhanSu,
                        IdChucVu = dto.IdChucVu,
                        IdPhongBan = dto.IdPhongBan,
                        IdToBoMon = dto.IdToBoMon,
                        NgayBatDau = DateTime.Now,
                        NgayKetThuc = null,
                    }
                );

                // Update thông tin công việc hiện tại của nhân sự
                _nhansuService.UpdateThongTinCongViec(
                    new UpdateNhanSuCongViecDto
                    {
                        IdNhanSu = dto.IdNhanSu,
                        MaNhanSu = dto.MaNhanSu,
                        MaSoThue = dto.MaSoThue,
                        TenNganHang1 = dto.TenNganHang1,
                        TenNganHang2 = dto.TenNganHang2,
                        Atm1 = dto.Atm1,
                        Atm2 = dto.Atm2,
                        HienTaiChucVu = dto.IdChucVu,
                        HienTaiPhongBan = dto.IdPhongBan,
                    }
                );
            }
        }

        public void CreateSubContract()
        {
            throw new NotImplementedException();
        }

        public PageResultDto<NsHopDongResponseDto> GetAllContract(NsHopDongRequestDto dto)
        {
            _logger.LogInformation(
                $"{nameof(GetAllContract)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query = _unitOfWork.iNsHopDongRepository.TableNoTracking.AsQueryable();

            // Filter
            if (dto.IdLoaiHopDong != null)
            {
                query = query.Where(x => dto.IdLoaiHopDong == x.IdLoaiHopDong);
            }

            

            var totalCount = query.Count();

            var items = query
                .OrderBy(x => x.CreatedDate).ThenBy(x => x.Id)
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            var result = items
                .Select(x => new NsHopDongResponseDto
                {
                    Id = x.Id,
                    SoHopDong = x.SoHopDong,
                    IdLoaiHopDong = x.IdLoaiHopDong,
                    IdNhanSu = x.IdNhanSu,
                    NgayKyKet = x.NgayKyKet,
                    HopDongCoThoiHanTuNgay = x.HopDongCoThoiHanTuNgay,
                    HopDongCoThoiHanDenNgay = x.HopDongCoThoiHanDenNgay,
                    NgayBatDauThuViec = x.NgayBatDauThuViec,
                    NgayKetThucThuViec = x.NgayKetThucThuViec,
                    LuongCoBan = x.LuongCoBan,
                    GhiChu = x.GhiChu,
                })
                .ToList();

            return new PageResultDto<NsHopDongResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }

        public NsHopDongResponseDto GetById(int idHopDong)
        {
            _logger.LogInformation(
                $"Method {nameof(GetById)} called. IdHopDong: {JsonSerializer.Serialize(idHopDong)}"
            );

            var exist = _unitOfWork.iNsHopDongRepository.TableNoTracking.FirstOrDefault(x =>
                x.Id == idHopDong
            );

            if (exist == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    "Không tìm thấy thông tin hợp đồng."
                );
            }
            else
            {
                var result = _mapper.Map<NsHopDongResponseDto>(exist);
                return result;
            }
        }

        private void ValidateMaNhanSu(string maNhanSu)
        {
            if (_unitOfWork.iNsNhanSuRepository.IsMaNhanSuExist(maNhanSu))
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.UserExists,
                    "Đã tồn tại mã nhân sự này trong CSDL. Vui lòng nhập mã khác."
                );
            }
        }

        private void ValidateSoHopDong(string soHopDong)
        {
            if (_unitOfWork.iNsHopDongRepository.IsSoHopDongExist(soHopDong))
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.UserExists,
                    "Đã tồn tại số hợp đồng này trong CSDL. Vui lòng nhập số khác."
                );
            }
        }
    }
}
