using System.Net;

namespace Doczy.Business.Exceptions.UnivercityExceptions
{
   
    public sealed class UnivercityNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public UnivercityNotFoundByIdException(Guid id)
        {
            ErrorMessage = $"Univercity not found with id: {id}";
        }

        public UnivercityNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public UnivercityNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
