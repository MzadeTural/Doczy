using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.AuthExceptions
{
    public class AlreadyAuthenticationException : Exception, IBaseException
    {


        public HttpStatusCode StatusCode { get; }

        public string ErrorMessage { get; }



        public AlreadyAuthenticationException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }

        public AlreadyAuthenticationException(string message, HttpStatusCode statusCode, Exception? innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorMessage = message;
        }
    }
}
