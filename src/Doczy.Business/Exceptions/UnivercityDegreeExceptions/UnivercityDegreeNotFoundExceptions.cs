using System;
using System.Net;

namespace Doczy.Business.Exceptions.UnivercityDegreeExceptions
{
	public class UnivercityDegreeNotFoundExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public UnivercityDegreeNotFoundExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}