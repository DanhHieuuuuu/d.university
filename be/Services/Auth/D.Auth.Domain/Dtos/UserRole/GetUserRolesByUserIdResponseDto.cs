namespace D.Auth.Domain.Dtos.UserRole
{
    public class GetUserRolesByUserIdResponseDto
    {
        public int NhanSuId { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}
