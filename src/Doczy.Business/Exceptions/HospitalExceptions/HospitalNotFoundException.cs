using System.Net;

namespace Doczy.Business.Exceptions.HospitalExceptions
{
  
    public class HospitalNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }
        public HospitalNotFoundException()
        {
            ErrorMessage = "There is no any hospital  items";
        }

        public HospitalNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
