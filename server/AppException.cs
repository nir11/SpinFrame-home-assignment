using server.Enums;

namespace server
{
    public class AppException : Exception
    {
        public int Code { get; set; }

        public AppException(ErrorInfo error) : base(error.Message)
        {
            Code = error.Code;
        }
    }
}
