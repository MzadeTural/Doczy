using System;
using System.Net;

namespace Doczy.Business.Exceptions.BlogExceptions
{
	public class BlogNotFoundExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public BlogNotFoundExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}

