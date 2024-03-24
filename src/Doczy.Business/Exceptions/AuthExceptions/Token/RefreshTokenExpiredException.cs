using System.Net;

namespace Doczy.Business.Exceptions.AuthExceptions.Token
{
    public class RefreshTokenExpiredException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage { get; }

        public RefreshTokenExpiredException()
        {
            ErrorMessage = "Refresh Token Expired";
        }

        public RefreshTokenExpiredException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public RefreshTokenExpiredException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
