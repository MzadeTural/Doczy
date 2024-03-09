using System;
using System.Net;

namespace Doczy.Business.Exceptions.EducationExceptions
{
    public class EducationNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public EducationNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}

