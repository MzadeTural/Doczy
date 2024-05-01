using System;
using System.Net;

namespace Doczy.Business.Exceptions.UnivercityDegreeExceptions
{
	public class UnivercityDegreeAlreadyExistExceptions : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public UnivercityDegreeAlreadyExistExceptions(string Message) : base(Message) { ErrorMessage = Message; }
    }
}

