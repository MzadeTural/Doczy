using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Doczy.Business.Exceptions.AuthExceptions
{
    public class EmailConfirmationException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }
        public string? errorDetail { get; }

        public EmailConfirmationException()
        {
            ErrorMessage = "Unexpected error occurred while activate user";
        }

        public EmailConfirmationException(IEnumerable<IdentityError> errors)
        {
            ErrorMessage = "Unexpected error occurred while activate user";
            errorDetail = String.Join(',', errors.Select(e => e.Description));
        }

        public EmailConfirmationException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public EmailConfirmationException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }


    }
}
