using server.Enums;

namespace server
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Payload { get; set; }
        public Error? Error { get; set; }

        public ApiResponse()
        {
            Success = true;
        }

        public ApiResponse(ErrorInfo error)
        {
            Success = false;
            Payload = null;
            Error = new Error
            {
                Code = error.Code,
                Messgae = error.Message
            };
        }

        public ApiResponse(Exception ex)
        {
            Success = false;
            Payload = null;
            Error = new Error
            {
                Code = ex is AppException ? ((AppException)ex).Code : -1,
                Messgae = ex.Message
            };      
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Payload { get; set; }
        public Error? Error { get; set; }

        public ApiResponse()
        {
            Success = true;
            Error = null;
        }

        public ApiResponse(T data)
        {
            Success = true;
            Payload = data;
            Error = null;
        }

        public ApiResponse(ErrorInfo error)
        {
            Success = false;
            Payload = default(T);
            Error = new Error
            {
                Code = error.Code,
                Messgae = error.Message
            };
        }

        public ApiResponse(Exception ex)
        {
            Success = false;
            Payload = default(T);
            Error = new Error
            {
                Code = ex is AppException ? ((AppException)ex).Code : -1,
                Messgae = ex.Message
            };
        }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Messgae { get; set; } = string.Empty;
    }
}
