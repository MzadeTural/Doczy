using System;
using System.Net;

namespace Doczy.Business.Exceptions.FieldOfStudyExceptions
{
	public class FieldOfStudyNotFoundExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public FieldOfStudyNotFoundExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}