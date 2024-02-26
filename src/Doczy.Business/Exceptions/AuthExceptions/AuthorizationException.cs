using System.Net;

namespace Doczy.Business.Exceptions.AuthExceptions
{
    public class AuthorizationException : Exception, IBaseException
    {


        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }

        public AuthorizationException(string action)
        {
            StatusCode = HttpStatusCode.Forbidden;
            ErrorMessage = $"You are not authorized to perform this action: {action}";
        }

        public AuthorizationException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }

        public AuthorizationException(string message, HttpStatusCode statusCode, Exception? innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
