using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.UserRole
{
    public class GetUserRolesByUserIdResponseDto
    {
        public int NhanSuId { get; set; }
        public List<int>? RoleIds { get; set; }
    }
}
