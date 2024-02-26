using System.Net;

namespace Doczy.Business.Exceptions.WorkPlaceExceptions
{
    public class WokrPlaceNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public WokrPlaceNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }


    }
}
