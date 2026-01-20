using System.Text.Json;
using AutoMapper;
using D.Constants.Core.Hrm;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using d.Shared.Permission.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NsQuyetDinhService : ServiceBase, INsQuyetDinhService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public NsQuyetDinhService(
            ILogger<NsQuyetDinhService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public NsQuyetDinh TaoQuyetDinh(CreateNsQuyetDinhDto dto)
        {
            _logger.LogInformation(
                $"Method {nameof(TaoQuyetDinh)} called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var currentId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var nhansu = _unitOfWork.iNsNhanSuRepository.FindById(currentId);
            var hotenNhanSuHienTai = nhansu.HoDem + " " + nhansu.Ten;

            var newQuyetDinh = new NsQuyetDinh
            {
                IdNhanSu = dto.IdNhanSu,
                MaNhanSu = dto.MaNhanSu,
                LoaiQuyetDinh = dto.LoaiQuyetDinh,
                Status = NsQuyetDinhStatus.TaoMoi,
                NoiDungTomTat = dto.NoiDungTomTat,
                NgayHieuLuc = dto.NgayHieuLuc,
            };

            _unitOfWork.iNsQuyetDinhRepository.Add(newQuyetDinh);
            _unitOfWork.iNsQuyetDinhRepository.SaveChange();

            _unitOfWork.iNsQuyetDinhLogRepository.Add(
                new NsQuyetDinhLog
                {
                    IdQuyetDinh = newQuyetDinh.Id,
                    OldStatus = null,
                    NewStatus = NsQuyetDinhStatus.TaoMoi,
                    Description =
                        $"{hotenNhanSuHienTai} đã {NsQuyetDinhStatus.Names[NsQuyetDinhStatus.TaoMoi]} quyết định {dto.NoiDungTomTat}.",
                }
            );
            _unitOfWork.iNsQuyetDinhLogRepository.SaveChange();

            return newQuyetDinh;
        }

        public void PheDuyetQuyetDinh(int idQuyetDinh)
        {
            _logger.LogInformation(
                $"Method {nameof(PheDuyetQuyetDinh)} called. IdQuyetDinh: {idQuyetDinh}"
            );

            var currentId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var nhansu = _unitOfWork.iNsNhanSuRepository.FindById(currentId);
            var hotenNhanSuHienTai = nhansu.HoDem + " " + nhansu.Ten;

            var quyetdinh = _unitOfWork.iNsQuyetDinhRepository.Table.FirstOrDefault(x =>
                x.Id == idQuyetDinh
            );

            if (quyetdinh == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tồn tại quyết định có Id: {idQuyetDinh}"
                );
            }
            else
            {
                _unitOfWork.iNsQuyetDinhLogRepository.Add(
                    new NsQuyetDinhLog
                    {
                        IdQuyetDinh = idQuyetDinh,
                        OldStatus = quyetdinh.Status,
                        NewStatus = NsQuyetDinhStatus.PheDuyet,
                        Description =
                            $"{hotenNhanSuHienTai} {NsQuyetDinhStatus.Names[NsQuyetDinhStatus.PheDuyet]} quyết định",
                    }
                );
                _unitOfWork.iNsQuyetDinhLogRepository.SaveChange();

                quyetdinh.Status = NsQuyetDinhStatus.PheDuyet;
                _unitOfWork.iNsQuyetDinhRepository.Update(quyetdinh);
                _unitOfWork.iNsQuyetDinhRepository.SaveChange();
            }
        }

        public void TuChoiQuyetDinh(int idQuyetDinh, string reason)
        {
            _logger.LogInformation(
                $"Method {nameof(TuChoiQuyetDinh)} called. IdQuyetDinh: {idQuyetDinh}, Reason: {reason}"
            );

            var currentId = CommonUntil.GetCurrentUserId(_contextAccessor);
            var nhansu = _unitOfWork.iNsNhanSuRepository.FindById(currentId);
            var hotenNhanSuHienTai = nhansu.HoDem + " " + nhansu.Ten;

            var quyetdinh = _unitOfWork.iNsQuyetDinhRepository.Table.FirstOrDefault(x =>
                x.Id == idQuyetDinh
            );

            if (quyetdinh == null)
            {
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tồn tại quyết định có Id: {idQuyetDinh}"
                );
            }
            else if (quyetdinh.Status == NsQuyetDinhStatus.PheDuyet)
            {
                throw new UserFriendlyException(
                    500012,
                    "Không thể từ chối quyết định đã được phê duyệt"
                );
            }
            else
            {
                _unitOfWork.iNsQuyetDinhLogRepository.Add(
                    new NsQuyetDinhLog
                    {
                        IdQuyetDinh = idQuyetDinh,
                        OldStatus = quyetdinh.Status,
                        NewStatus = NsQuyetDinhStatus.TuChoi,
                        Description =
                            $"{hotenNhanSuHienTai} đã {NsQuyetDinhStatus.Names[NsQuyetDinhStatus.TuChoi]} quyết định với lý do: {reason}",
                    }
                );
                _unitOfWork.iNsQuyetDinhLogRepository.SaveChange();

                quyetdinh.Status = NsQuyetDinhStatus.TuChoi;
                _unitOfWork.iNsQuyetDinhRepository.Update(quyetdinh);
                _unitOfWork.iNsQuyetDinhRepository.SaveChange();
            }
        }

        public PageResultDto<NsQuyetDinhResponseDto> FindPagingNsQuyetDinh(
            NsQuyetDinhRequestDto dto
        )
        {
            _logger.LogInformation(
                $"Method {nameof(FindPagingNsQuyetDinh)} called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var query =
                from qd in _unitOfWork.iNsQuyetDinhRepository.TableNoTracking
                join ns in _unitOfWork.iNsNhanSuRepository.TableNoTracking
                    on qd.IdNhanSu equals ns.Id
                select new { QuyetDinh = qd, NhanSu = ns };

            // Filter
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(x =>
                    x.QuyetDinh.NoiDungTomTat!.ToLower().Contains(dto.Keyword.ToLower())
                );
            }
            if (dto.TrangThai.HasValue)
            {
                query = query.Where(x => x.QuyetDinh.Status == dto.TrangThai);
            }
            if (dto.LoaiQuyetDinh.HasValue)
            {
                query = query.Where(x => x.QuyetDinh.LoaiQuyetDinh == dto.LoaiQuyetDinh);
            }

            var totalCount = query.Count();

            var items = query
                .OrderByDescending(x => x.QuyetDinh.CreatedDate) // cố định thứ tự
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            var result = items
                .Select(x => new NsQuyetDinhResponseDto
                {
                    Id = x.QuyetDinh.Id,
                    IdNhanSu = x.QuyetDinh.IdNhanSu,
                    MaNhanSu = x.QuyetDinh.MaNhanSu,
                    HoTen = x.NhanSu.HoDem + " " + x.NhanSu.Ten,
                    Status = x.QuyetDinh.Status,
                    LoaiQuyetDinh = x.QuyetDinh.LoaiQuyetDinh,
                    NgayHieuLuc = x.QuyetDinh.NgayHieuLuc,
                    NoiDungTomTat = x.QuyetDinh.NoiDungTomTat,
                })
                .ToList();

            return new PageResultDto<NsQuyetDinhResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }

        public NsQuyetDinhFindByIdResponseDto FindById(int idQuyetDinh)
        {
            _logger.LogInformation($"Method {nameof(FindById)} called. Id: {idQuyetDinh}");

            var result = (
                from qd in _unitOfWork.iNsQuyetDinhRepository.TableNoTracking
                join ns in _unitOfWork.iNsNhanSuRepository.TableNoTracking
                    on qd.IdNhanSu equals ns.Id
                where qd.Id == idQuyetDinh
                select new NsQuyetDinhFindByIdResponseDto
                {
                    Id = idQuyetDinh,
                    IdNhanSu = qd.IdNhanSu,
                    MaNhanSu = ns.MaNhanSu,
                    HoTen = ns.HoDem + " " + ns.Ten,
                    Status = qd.Status,
                    LoaiQuyetDinh = qd.LoaiQuyetDinh,
                    NgayHieuLuc = qd.NgayHieuLuc,
                    NoiDungTomTat = qd.NoiDungTomTat,
                }
            ).FirstOrDefault();

            if (result == null)
                throw new UserFriendlyException(
                    ErrorCodeConstant.CodeNotFound,
                    $"Không tìm thấy quyết định có Id {idQuyetDinh}"
                );

            var historyQuery = _unitOfWork
                .iNsQuyetDinhLogRepository.TableNoTracking.Where(log =>
                    log.IdQuyetDinh == idQuyetDinh
                )
                .Select(x => new NsQuyetDinhLogResponseDto
                {
                    Id = x.Id,
                    IdQuyetDinh = idQuyetDinh,
                    OldStatus = x.OldStatus,
                    NewStatus = x.NewStatus,
                    Description = x.Description,
                    CreatedDate = x.CreatedDate
                })
                .ToList();

            result.History = historyQuery;

            return result;
        }
    }
}
