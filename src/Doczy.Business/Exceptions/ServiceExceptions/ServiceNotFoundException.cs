using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.ServiceExceptions
{
    public class ServiceNotFoundException : Exception, IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public ServiceNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }


    }
}
