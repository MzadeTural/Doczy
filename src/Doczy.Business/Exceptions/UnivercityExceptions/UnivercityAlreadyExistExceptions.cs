using System.Net;

namespace Doczy.Business.Exceptions.UnivercityExceptions
{
    public class UnivercityAlreadyExistExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public UnivercityAlreadyExistExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
