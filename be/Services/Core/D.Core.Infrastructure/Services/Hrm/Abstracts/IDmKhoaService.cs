using D.Core.Domain.Dtos.Hrm.DanhMuc.DmKhoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface IDmKhoaService
    {
        public Task<DmKhoaResponseDto> GetByIdAsync(int id);
    }
}
