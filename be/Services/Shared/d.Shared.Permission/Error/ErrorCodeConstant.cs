using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d.Shared.Permission.Error
{
    public static class ErrorCodeConstant
    {
        public const int UserExists = 1005; // Người dùng đã tồn tại quyền này
        public const int RefreshTokenNotFound = 1006; // refresh token không đúng hoặc đã hết hạn
        public const int PasswordWrong = 1007; // Mật khẩu không đúng
        public const int PasswordOrCodeWrong = 1008; // Không đúng mật khẩu hoặc tài khoản.
        public const int CodeExits = 1009; // Mã nhân sự đã tồn tại
        public const int CodeNotFound = 1010; // Không tìm thấy mã nhân sự
        public const int AccountDisabled = 1011; // Tài khoản bị vô hiệu hóa
    }
}
