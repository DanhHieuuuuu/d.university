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
    public class LogStatusService : ServiceBase, ILogStatusService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public LogStatusService(
            ILogger<LogStatusService> logger,
            IHttpContextAccessor httpContext,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, httpContext, mapper)
        {
            _unitOfWork = unitOfWork;
        }
        public void InsertLog(InsertDelegationIncomingLogDto dto)
        {
            _logger.LogInformation(
                $"{nameof(InsertLog)} dto = {JsonSerializer.Serialize(dto)}"
            );

            try
            {
                var log = new LogStatus
                {
                    DelegationIncomingCode = dto.DelegationIncomingCode,
                    OldStatus = dto.OldStatus,
                    NewStatus = dto.NewStatus,
                    Description = dto.Description,
                    Reason = dto.Reason,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CommonUntil.GetCurrentUserId(_contextAccessor).ToString()
                };

                _unitOfWork.iLogStatusRepository.Add(log);
                _unitOfWork.iLogStatusRepository.SaveChange();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(InsertLog)} failed");
                throw;
            }
        }

        public PageResultDto<ViewDelegationIncomingLogDto> FindLogDelegationIncoming(FindDelegationIncomingLogDto dto)
        {
            _logger.LogInformation($"{nameof(FindLogDelegationIncoming)} method called, dto: {JsonSerializer.Serialize(dto)}.");

            var query = _unitOfWork.iLogStatusRepository.TableNoTracking.OrderByDescending(log => log.Id);

            var totalCount = query.Count();

            var items = query.Skip(dto.SkipCount()).Take(dto.PageSize).ToList();

            return new PageResultDto<ViewDelegationIncomingLogDto>
            {
                Items = _mapper.Map<List<ViewDelegationIncomingLogDto>>(items),
                TotalItem = totalCount
            };
        }


    }
}
