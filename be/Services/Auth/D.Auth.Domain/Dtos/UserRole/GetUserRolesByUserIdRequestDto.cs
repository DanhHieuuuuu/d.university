using D.Auth.Domain.Dtos.Role;
using D.DomainBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.UserRole
{
    public class GetUserRolesByUserIdRequestDto : IQuery<GetUserRolesByUserIdResponseDto>
    {
        public int NhanSuId { get; set; }
    }
}
