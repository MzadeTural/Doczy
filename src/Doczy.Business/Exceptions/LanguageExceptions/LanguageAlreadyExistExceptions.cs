using System.Net;

namespace Doczy.Business.Exceptions.LanguageExceptions
{
    public class LanguageAlreadyExistExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public LanguageAlreadyExistExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
