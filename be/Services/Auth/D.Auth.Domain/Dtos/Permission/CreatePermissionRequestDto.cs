using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.Auth.Domain.Dtos.Permission
{
    public class CreatePermissionRequestDto
    {
        public string PermissonKey { get; set; }
        public string PermissionName { get; set; }
    }
}
