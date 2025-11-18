using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.UserRole
{
    public class GetUserRolesByUserIdRequestDto : IQuery<GetUserRolesByUserIdResponseDto>
    {
        public int NhanSuId { get; set; }
    }
}
