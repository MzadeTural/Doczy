using System.Net;

namespace Doczy.Business.Exceptions.HospitalExceptions
{
  
    public class HospitalAlreadyExistExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public HospitalAlreadyExistExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
