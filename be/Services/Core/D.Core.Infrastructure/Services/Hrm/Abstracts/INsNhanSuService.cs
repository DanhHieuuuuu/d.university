using D.Core.Domain.Dtos.Hrm;
using D.Core.Domain.Dtos.Hrm.NhanSu;
using D.Core.Domain.Entities.Hrm.NhanSu;
using D.DomainBase.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface INsNhanSuService
    {
        PageResultDto<NsNhanSuResponseDto> FindPagingNsNhanSu(NsNhanSuRequestDto dto);
        void CreateGiaDinhNhanSu(int idNhanSu, CreateNsQuanHeGiaDinhDto dto);
        NsNhanSuResponseDto CreateNhanSu(CreateNhanSuDto dto);
        void CreateHopDong(CreateHopDongDto dto);
    }
}
