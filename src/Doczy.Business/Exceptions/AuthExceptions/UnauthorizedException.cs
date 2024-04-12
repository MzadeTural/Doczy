using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.AuthExceptions
{
  
    public sealed class UnauthorizedException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

        public string ErrorMessage { get; }

        public UnauthorizedException()
        {
            ErrorMessage = "Unauthorize";
        }

        public UnauthorizedException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public UnauthorizedException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
