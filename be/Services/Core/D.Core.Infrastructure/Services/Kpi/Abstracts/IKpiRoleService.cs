using D.Core.Domain.Dtos.Kpi.KpiDonVi;
using D.Core.Domain.Dtos.Kpi.KpiRole;
using D.DomainBase.Dto;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiRoleService
    {
        public PageResultDto<KpiRoleResponseDto> FindAllKpiRole(KpiRoleRequestDto dto);
        public void CreateKpiRole(CreateKpiRoleDto dto);
        public void UpdateKpiRole(UpdateKpiRoleDto dto);
        public void Delete(DeleteKpiRoleDto dto);
        List<GetKpiRoleByUserResponseDto> GetRoleByUserId();
    }
}
