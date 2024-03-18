using System.Net;

namespace Doczy.Business.Exceptions.ServiceTypeExceptions
{
 
    public class ServiceTypeNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }
        public ServiceTypeNotFoundException()
        {
            ErrorMessage = "There is no any service type  items";
        }

        public ServiceTypeNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
