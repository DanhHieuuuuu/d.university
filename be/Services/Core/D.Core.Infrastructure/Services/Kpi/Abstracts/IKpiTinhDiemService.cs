using D.Core.Domain.Dtos.Kpi.KpiTinhDiem;
using D.Core.Domain.Dtos.Kpi.KpiTinhDiem.D.Core.Domain.Dtos.Kpi.KpiTinhDiem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Kpi.Abstracts
{
    public interface IKpiTinhDiemService
    {
        Task<PersonalScoreDto> CalculatePersonalScore(PersonalScoreRequestDto dto);
        Task<UnitScoreDto> CalculateUnitScore(UnitScoreRequestDto dto);
        Task<SchoolScoreDto> CalculateSchoolScore(SchoolScoreRequestDto dto);
        Task<List<int>> GetManagedUnitIds(int userId);
        Task<List<PersonalScoreDto>> GetStaffScoresInUnit(StaffScoreRequestDto dto);
        Task<KpiDashboardResponse> GetDashboardData(GetKpiScoreBoardDto dto);
    }
}
