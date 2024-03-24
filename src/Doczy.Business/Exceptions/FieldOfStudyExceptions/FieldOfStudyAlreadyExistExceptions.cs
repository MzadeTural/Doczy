using System;
using System.Net;

namespace Doczy.Business.Exceptions.FieldOfStudyExceptions
{
	public class FieldOfStudyAlreadyExistExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public FieldOfStudyAlreadyExistExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}

