using AutoMapper;
using D.Core.Domain.Dtos.Delegation.Incoming.DelegationIncoming;
using D.Core.Domain.Entities.Delegation.Incoming;
using D.Core.Infrastructure.Services.Delegation.Incoming.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using D.InfrastructureBase.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Delegation.Incoming.Implements
{
    public class LogReceptionTimeService : ServiceBase, ILogReceptionTimeService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public LogReceptionTimeService(
            ILogger<LogReceptionTimeService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public void InsertLogReceptionTime(InsertReceptionTimeLogDto dto)
        {
            _logger.LogInformation($"{nameof(InsertLogReceptionTime)} dto = {JsonSerializer.Serialize(dto)}");

            try
            {
                var log = new LogReceptionTime
                {
                    ReceptionTimeId = dto.ReceptionTimeId,
                    Type = dto.Type,
                    Description = dto.Description,
                    Reason = dto.Reason,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CommonUntil.GetCurrentUserId(_contextAccessor).ToString()
                };

                _unitOfWork.iLogReceptionTimeRepository.Add(log);
                _unitOfWork.iLogReceptionTimeRepository.SaveChange();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InsertLogReceptionTime)} failed");
                throw;
            }
        }

        public PageResultDto<ViewReceptionTimeLogDto> FindLogReceptionTime(FindReceptionTimeLogDto dto)
        {
            _logger.LogInformation($"{nameof(FindLogReceptionTime)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var query = _unitOfWork.iLogReceptionTimeRepository.TableNoTracking.OrderByDescending(log => log.Id);

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            return new PageResultDto<ViewReceptionTimeLogDto>
            {
                Items = _mapper.Map<List<ViewReceptionTimeLogDto>>(items),
                TotalItem = totalCount
            };
        }
    }
}
