using System.Net;

namespace Doczy.Business.Exceptions.LanguageExceptions
{
    public class LanguageAlreadyAddedExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; }

        public LanguageAlreadyAddedExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    
}
}
