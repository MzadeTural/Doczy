using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.ExperianceExceptions
{
   
    public class ExperianceNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }
        public ExperianceNotFoundException()
        {
            ErrorMessage = "There is no any experiance items";
        }

        public ExperianceNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
