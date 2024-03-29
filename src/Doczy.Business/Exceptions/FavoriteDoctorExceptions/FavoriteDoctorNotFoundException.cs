using System.Net;

namespace Doczy.Business.Exceptions.FavoriteDoctorExceptions
{
  
    public class FavoriteDoctorNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }
        public FavoriteDoctorNotFoundException()
        {
            ErrorMessage = "There is no any favorite doctor   items";
        }

        public FavoriteDoctorNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
