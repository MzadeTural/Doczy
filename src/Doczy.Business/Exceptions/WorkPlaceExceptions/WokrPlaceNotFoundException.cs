using System.Net;

namespace Doczy.Business.Exceptions.WorkPlaceExceptions
{
    public class WokrPlaceNotFoundException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }


        public WokrPlaceNotFoundException(string key, string value)
        {
            StatusCode = HttpStatusCode.Unauthorized;
            ErrorMessage = $"User not found with {key}: {value}";
        }

        public WokrPlaceNotFoundException(string message, HttpStatusCode? statusCode = null) : base(message)
        {
            StatusCode = statusCode ?? HttpStatusCode.Unauthorized;
            ErrorMessage = message;
        }

        public WokrPlaceNotFoundException(string message, HttpStatusCode statusCode, Exception? innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
