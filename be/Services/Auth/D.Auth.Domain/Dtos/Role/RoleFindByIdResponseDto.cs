namespace D.Auth.Domain.Dtos.Role
{
    public class RoleFindByIdResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<int>? PermissionIds { get; set; } = new List<int>();
    }
}
