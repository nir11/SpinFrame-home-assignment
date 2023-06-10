namespace server.Enums
{
    public static class Errors
    {
        public static readonly ErrorInfo CarNotFound = new ErrorInfo { Code = 1, Message = "Car Not Found" };
        public static readonly ErrorInfo CarAlreadyExists = new ErrorInfo { Code = 2, Message = "Car Already Exists" };
    }

    public class ErrorInfo
    {
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
