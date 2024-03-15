using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.PaymentExceptions
{
    public class PaymentFailedException : Exception,IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;

       public string ErrorMessage { get; }

        public PaymentFailedException(string Message) : base(Message) { ErrorMessage = Message; }


    }
}
