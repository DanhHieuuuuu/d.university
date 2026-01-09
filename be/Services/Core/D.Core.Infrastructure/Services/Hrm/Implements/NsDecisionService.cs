
using AutoMapper;
using D.Constants.Core.Hrm;
using D.ControllerBase.Exceptions;
using D.Core.Domain.Dtos.Hrm.QuyetDinh;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.Core.Infrastructure.Services.Hrm.Abstracts;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using d.Shared.Permission.Error;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using D.DomainBase.Dto;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Hrm.Implements
{
    public class NsDecisionService : ServiceBase, INsDecisionService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public NsDecisionService(
            ILogger<NsDecisionService> logger,
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
            _logger.LogInformation($"Method {nameof(TaoQuyetDinh)} called. Dto: {JsonSerializer.Serialize(dto)}");

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
            _logger.LogInformation($"Method {nameof(PheDuyetQuyetDinh)} called. IdQuyetDinh: {idQuyetDinh}");

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
                    "Không tồn tại quyết định có Id"
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
            }
        }

        public void TuChoiQuyetDinh(int idQuyetDinh, string reason)
        {
            _logger.LogInformation($"Method {nameof(TuChoiQuyetDinh)} called. IdQuyetDinh: {idQuyetDinh}, Reason: {reason}");

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
                    "Không tồn tại quyết định có Id"
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
            }
        }

        public PageResultDto<NsQuyetDinhResponseDto> FindPagingNsQuyetDinh(NsQuyetDinhRequestDto dto)
        {
            _logger.LogInformation($"Method {nameof(FindPagingNsQuyetDinh)} called. Dto: {JsonSerializer.Serialize(dto)}");


            var query = _unitOfWork.iNsQuyetDinhRepository.TableNoTracking.AsQueryable();

            // Filter
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                query = query.Where(x => x.NoiDungTomTat!.ToLower().Contains(dto.Keyword.ToLower()));
            }
            if (dto.TrangThai != null)
            {
                query = query.Where(x => x.Status == dto.TrangThai);
            }

            var totalCount = query.Count();

            var items = query
                .OrderByDescending(x => x.CreatedDate) // cố định thứ tự
                .Skip(dto.SkipCount())
                .Take(dto.PageSize)
                .ToList();

            var result = items
                .Select(x => new NsQuyetDinhResponseDto
                {
                    Id = x.Id,
                    IdNhanSu = x.IdNhanSu,
                    MaNhanSu = x.MaNhanSu,
                    Status = x.Status,
                    LoaiQuyetDinh = x.LoaiQuyetDinh,
                    NgayHieuLuc = x.NgayHieuLuc,
                    NoiDungTomTat = x.NoiDungTomTat
                })
                .ToList();

            return new PageResultDto<NsQuyetDinhResponseDto>
            {
                Items = result,
                TotalItem = totalCount,
            };
        }
    }
}
