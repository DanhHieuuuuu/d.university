using AutoMapper;
using D.Auth.Domain.Dtos;
using D.Auth.Infrastructure.Services.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Implements
{
    public class NsNhanSuService : ServiceBase, INsNhanSuService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public NsNhanSuService(
            ILogger<NsNhanSuService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto)
        {
            _logger.LogInformation($"{nameof(FindPagingNsNhanSu)} method called. Dto: {dto}");

            var query = _unitOfWork.iNsNhanSuRepository.TableNoTracking.Where(x => string.IsNullOrEmpty(dto.Keyword) || dto.Keyword == x.MaSoThue);
            var result = _mapper.Map<List<NsNhanSuResponseDto>>(query);

            return new PageResultDto<NsNhanSuResponseDto>
            {
                Items = result.Skip(dto.SkipCount()).Take(dto.PageSize).ToList(),
                TotalItem = result.Count(),
            };
        }
    }
}
