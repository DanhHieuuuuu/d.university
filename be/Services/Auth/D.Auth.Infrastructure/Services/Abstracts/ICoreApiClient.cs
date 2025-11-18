using D.Core.Domain.Dtos.Hrm.DanhMuc.DmChucVu;

namespace D.Auth.Infrastructure.Services.Abstracts
{
    public interface ICoreApiClient
    {
        Task<DmChucVuResponseDto?> GetChucVuNameAsync(int? id);
    }
}
