using D.DomainBase.Common;

namespace D.Auth.Domain.Dtos.Role
{
    public class RoleFindByIdRequestDto : IQuery<RoleFindByIdResponseDto>
    {
        public int Id { get; set; }
    }
}
