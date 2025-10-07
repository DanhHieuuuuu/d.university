using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface ICoreApiClient
    {
        Task<DmChucVuResponseDto?> GetChucVuNameAsync(int? id);
    }
}
