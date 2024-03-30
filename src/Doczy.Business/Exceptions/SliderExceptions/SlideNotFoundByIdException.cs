using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.SliderExceptions
{
   

    public sealed class SlideNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public SlideNotFoundByIdException(Guid id)
        {
            ErrorMessage = $"Slide not found with id: {id}";
        }

        public SlideNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public SlideNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
