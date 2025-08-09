using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.ControllerBases.Exceptions
{
    public class BaseException : Exception
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public readonly int ErrorCode;
        /// <summary>
        /// Chuỗi cần localize sẽ tra trong từ điển, nếu có truyền chuỗi này thì 
        /// sẽ không lấy message của error nữa mà lấy theo chuỗi này
        /// </summary>
        public readonly string? Message;
        /// <summary>
        /// Mảng chuỗi cần chả
        /// </summary>
        public string[]? ListParam;

        public BaseException(int errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public BaseException(int errorCode, string? message) : base()
        {
            ErrorCode = errorCode;
            Message = message;
        }
        public BaseException(int errorCode, params string[] listParam) : base()
        {
            ErrorCode = errorCode;
            ListParam = listParam;
        }
    }
}
