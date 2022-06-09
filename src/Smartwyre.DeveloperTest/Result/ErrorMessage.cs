namespace Smartwyre.DeveloperTest.Result
{
    public class ErrorMessage
    {
        public ErrorMessage(int code, string message)
        {
            ErrorCode = code;
            Message = message;
        }

        public int ErrorCode { get; }

        public string Message { get; }
    }
}