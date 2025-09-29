using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Core.Infrastructure.Services.Hrm.Abstracts
{
    public interface IDmChucVuService
    {
        public Task<DmChucVuResponseDto> GetByIdAsync(int id);
    }
}
