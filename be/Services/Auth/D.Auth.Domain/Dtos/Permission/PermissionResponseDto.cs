namespace D.Auth.Domain.Dtos.Permission
{
    public class PermissionResponseDto
    {
        /// <summary>
        /// Key permission
        /// </summary>
        public string? Key { get; set; }

        /// <summary>
        /// Key of parent permission (can null)
        /// </summary>
        public string? ParentKey { get; set; }

        /// <summary>
        /// Label of key permsision (permission name)
        /// </summary>
        public string? Label { get; set; }
    }
}
