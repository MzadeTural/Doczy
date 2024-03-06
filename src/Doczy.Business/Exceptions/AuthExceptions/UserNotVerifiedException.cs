using System.Net;

namespace Doczy.Business.Exceptions.AuthExceptions
{

    public class UserNotVerifiedException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }
        public string? ErrorDetail { get; }

        public UserNotVerifiedException()
        {
            StatusCode = HttpStatusCode.Forbidden;
            ErrorMessage = "Your account not verified or blocked.";
            ErrorDetail = "Please  contact our staff.";
        }

        public UserNotVerifiedException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}


