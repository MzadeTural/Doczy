using System.Net;

namespace Doczy.Business.Exceptions.DoctorAvailabilityExceptions
{
    public class DoctorAvailabilityNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }
        public DoctorAvailabilityNotFoundException()
        {
            ErrorMessage = "There is no any DoctorAvailability  items";
        }

        public DoctorAvailabilityNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
