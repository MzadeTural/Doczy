using System.Net;

namespace Doczy.Business.Exceptions.FileExceptions
{
  
    public class FileCreationFailureException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }
        public FileCreationFailureException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
