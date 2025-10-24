namespace D.Auth.Domain.Dtos.Permission
{
    public class PermissionTreeResponseDto
    {
        public string? Key { get; set; }
        public string? Label { get; set; }
        public string? ParentKey { get; set; }
        public List<PermissionTreeResponseDto> Children { get; set; } = new();
    }
}
