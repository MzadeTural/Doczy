using System.Net;

namespace Doczy.Business.Exceptions.HospitalExceptions
{


    public sealed class HospitalNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public HospitalNotFoundByIdException(Guid id)
        {
            ErrorMessage = $"Hospital not found with id: {id}";
        }

        public HospitalNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public HospitalNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
