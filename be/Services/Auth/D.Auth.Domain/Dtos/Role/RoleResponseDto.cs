namespace D.Auth.Domain.Dtos.Role
{
    public class RoleResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public int TotalUser { get; set; }
    }
}
