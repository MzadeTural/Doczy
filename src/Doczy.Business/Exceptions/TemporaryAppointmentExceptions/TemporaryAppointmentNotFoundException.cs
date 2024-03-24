using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.TemporaryAppointmentExceptions
{
    public class TemporaryAppointmentNotFoundException:Exception,IBaseException
    {

        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }
        public TemporaryAppointmentNotFoundException()
        {
            ErrorMessage = "There is no any Temporary appointment  items";
        }

        public TemporaryAppointmentNotFoundException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
