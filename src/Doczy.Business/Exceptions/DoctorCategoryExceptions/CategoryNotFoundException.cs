using System.Net;

namespace Doczy.Business.Exceptions.DoctorCategoryExceptions
{
 
    public class CategoryNotFoundException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public CategoryNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
