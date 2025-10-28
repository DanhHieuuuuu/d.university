namespace D.Auth.Domain.Dtos.Permission
{
    public class PermissionTreeResponseDto
    {
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Label { get; set; }
        public List<PermissionTreeResponseDto> Children { get; set; } = new();
    }
}
