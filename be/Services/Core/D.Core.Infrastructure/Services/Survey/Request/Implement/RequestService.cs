using AutoMapper;
using D.Core.Domain.Dtos.Survey.Request;
using D.Core.Infrastructure.Services.Survey.Request.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Survey.Request.Implement
{
    public class RequestService : ServiceBase, IRequestService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public RequestService(
            ILogger<RequestService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public PageResultDto<RequestSurveyRequestDto> Paging(FilterSurveyRequestDto dto)
        {
            // 1. Khởi tạo query
            var query = _unitOfWork.KsSurveyRequests.GetQueryable().AsNoTracking();

            // 2. Filter Keyword
            if (!string.IsNullOrEmpty(dto.Keyword))
            {
                var k = dto.Keyword.Trim().ToLower();
                query = query.Where(x => x.TenKhaoSatYeuCau.ToLower().Contains(k) ||
                                         x.MaYeuCau.ToLower().Contains(k));
            }

            // 3. Filter Trạng thái
            if (dto.TrangThai.HasValue)
            {
                query = query.Where(x => x.TrangThai == dto.TrangThai.Value);
            }

            // 4. Tính tổng số bản ghi
            var totalCount = query.Count();

            // 5. Query lấy dữ liệu & Map sang DTO
            var items = query
                .OrderByDescending(x => x.Id) // Mới nhất lên đầu
                .Skip((dto.PageIndex - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .Select(x => new RequestSurveyRequestDto
                {
                    Id = x.Id,
                    MaYeuCau = x.MaYeuCau,
                    TenKhaoSatYeuCau = x.TenKhaoSatYeuCau,
                    MoTa = x.MoTa,
                    ThoiGianBatDau = x.ThoiGianBatDau,
                    ThoiGianKetThuc = x.ThoiGianKetThuc,
                    IdPhongBan = x.IdPhongBan,
                    TrangThai = x.TrangThai,
                    LyDoTuChoi = x.LyDoTuChoi
                    // Không map Targets/Questions ở đây cho nhẹ query
                })
                .ToList();

            // 6. Trả về kết quả
            return new PageResultDto<RequestSurveyRequestDto>
            {
                Items = items,
                TotalCount = totalCount,
            };
        }
    }
}
