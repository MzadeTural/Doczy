using System.Net;

namespace Doczy.Business.Exceptions.FileExceptions
{
   
    public class UnsupportedMediaTypeException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.UnsupportedMediaType;

        public string ErrorMessage { get; }
        public UnsupportedMediaTypeException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
