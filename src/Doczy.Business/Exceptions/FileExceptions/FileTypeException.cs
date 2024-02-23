using System.Net;

namespace Doczy.Business.Exceptions.FileExceptions
{
    public class FileTypeException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.UnsupportedMediaType;

        public string ErrorMessage { get; }
        public FileTypeException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
