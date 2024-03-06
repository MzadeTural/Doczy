using System.Net;

namespace Doczy.Business.Exceptions.UnivercityExceptions
{
    public class UnivercityNotFoundExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public UnivercityNotFoundExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
