using AutoMapper;
using D.Core.Domain.Dtos.Kpi.KpiCaNhan;
using D.Core.Domain.Entities.Kpi;
using D.Core.Infrastructure.Services.Kpi.Abstracts;
using D.DomainBase.Dto;
using D.InfrastructureBase.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace D.Core.Infrastructure.Services.Kpi.Implements
{
    public class KpiCaNhanService : ServiceBase, IKpiCaNhanService
    {
        private readonly ServiceUnitOfWork _unitOfWork;

        public KpiCaNhanService(
            ILogger<KpiRoleService> logger,
            IHttpContextAccessor contextAccessor,
            IMapper mapper,
            ServiceUnitOfWork unitOfWork
        )
            : base(logger, contextAccessor, mapper)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateKpiCaNhan(CreateKpiCaNhanDto dto)
        {
            _logger.LogInformation(
                $"{nameof(CreateKpiCaNhan)} method called. Dto: {JsonSerializer.Serialize(dto)}"
            );

            var nhanSu = _unitOfWork.iNsNhanSuRepository.TableNoTracking.FirstOrDefault(x => x.Id == dto.IdNhanSu);
            if (nhanSu == null)
                throw new Exception("Không tìm thấy nhân sự");

            var maxSTT = _unitOfWork.iKpiCaNhanRepository.TableNoTracking
                .Where(k => k.IdNhanSu == nhanSu.Id && !k.Deleted)
                .Max(k => (int?)k.STT) ?? 0;

            var entity = _mapper.Map<KpiCaNhan>(dto);
            entity.STT = maxSTT + 1;
            _unitOfWork.iKpiCaNhanRepository.Add(entity);
            _unitOfWork.iKpiCaNhanRepository.SaveChange();
        }

        public void DeleteKpiCaNhan(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PageResultDto<KpiCaNhanDto>> FindPagingKpiCaNhanAsync(FilterKpiCaNhanDto dto)
        {
            throw new NotImplementedException();
        }

        public PageResultDto<KpiCaNhanDto> GetAllKpiCaNhan(FilterKpiCaNhanDto dto)
        {
            throw new NotImplementedException();
        }

        public List<int> GetListTrangThai()
        {
            throw new NotImplementedException();
        }

        public void UpdateKetQuaThucTe(UpdateKpiThucTeKpiCaNhanListDto dto)
        {
            throw new NotImplementedException();
        }

        public void UpdateKpiCaNhan(UpdateKpiCaNhanDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<List<KpiCaNhanDto>> UpdateTrangThaiKpiCaNhan(UpdateTrangThaiKpiDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
