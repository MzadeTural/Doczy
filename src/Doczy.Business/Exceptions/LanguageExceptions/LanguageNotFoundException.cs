using System.Net;

namespace Doczy.Business.Exceptions.LanguageExceptions
{
    public class LanguageNotFoundException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public LanguageNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
