using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D.ControllerBases
{
    public class ResponseAPI
    {
        public StatusCode Status { get; set; }
        public object? Data { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }

        public ResponseAPI(StatusCode status, object? data, int code, string message)
        {
            Status = status;
            Data = data;
            Code = code;
            Message = message;
        }

        public ResponseAPI(object? data)
        {
            Status = StatusCode.Success;
            Data = data;
            Code = 200;
            Message = "Ok";
        }

        public ResponseAPI()
        {
            Status = StatusCode.Success;
            Data = null;
            Code = 200;
            Message = "Ok";
        }
    }
    public class ResponseAPI<T> : ResponseAPI
    {
        public new T Data { get; set; }

        public ResponseAPI(StatusCode status, T data, int code, string message) : base(status, data, code, message)
        {
            Status = status;
            Data = data;
            Code = code;
            Message = message;
        }

        public ResponseAPI(T data) : base(data)
        {
            Data = data;
        }

        public ResponseAPI() : base() // ✅ thêm constructor rỗng
        {
        }
    }

    public enum StatusCode
    {
        Success = 1,
        Error = 0
    }
}
