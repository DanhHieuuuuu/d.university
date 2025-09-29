using D.Core.Domain.Dtos.Hrm.DanhMuc.DmPhongBan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface IDmPhongBanService
    {
        public Task<DmPhongBanResponseDto> GetByIdAsync(int id);
    }
}
