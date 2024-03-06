using Doczy.Business.Exceptions;
using System.Net;

namespace Doczy.Business.DTOs.AuthDtos
{

    public class DoctorNotVerifiedException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }
        public string? ErrorDetail { get; }

        public DoctorNotVerifiedException()
        {
            StatusCode = HttpStatusCode.Forbidden;
            ErrorMessage = "Your account not verified.";
            ErrorDetail = "Please  contact our staff.";
        }

        public DoctorNotVerifiedException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
