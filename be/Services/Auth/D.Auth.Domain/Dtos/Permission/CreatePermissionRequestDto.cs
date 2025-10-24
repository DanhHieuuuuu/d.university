namespace D.Auth.Domain.Dtos.Permission
{
    public class CreatePermissionRequestDto
    {
        public string PermissonKey { get; set; }
        public string PermissionName { get; set; }
        public string? ParentKey { get; set; }
    }
}
