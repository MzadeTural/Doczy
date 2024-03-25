using System.Net;

namespace Doczy.Business.Exceptions.GenderExceptions
{

    public class GenderAlreadyExistException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public GenderAlreadyExistException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
