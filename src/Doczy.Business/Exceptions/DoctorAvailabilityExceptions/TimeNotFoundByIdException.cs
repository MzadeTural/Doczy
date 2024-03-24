using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.DoctorAvailabilityExceptions
{
   
    public sealed class TimeNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public TimeNotFoundByIdException(Guid id)
        {
            ErrorMessage = $"Comment not found with id: {id}";
        }

        public TimeNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public TimeNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
