using System.Net;

namespace Doczy.Business.Exceptions.FileExceptions
{
    public class FileSizeException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }
        public FileSizeException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
