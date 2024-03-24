using System.Net;

namespace Doczy.Business.Exceptions.ServiceTypeExceptions
{
    public class ServiceTypeAlreadyExistExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public ServiceTypeAlreadyExistExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
