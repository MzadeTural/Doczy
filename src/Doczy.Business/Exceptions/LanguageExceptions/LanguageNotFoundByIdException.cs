using System.Net;

namespace Doczy.Business.Exceptions.LanguageExceptions
{

    public sealed class LanguageNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public LanguageNotFoundByIdException(Guid id)
        {
            ErrorMessage = $"Language not found with id: {id}";
        }

        public LanguageNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public LanguageNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
