using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.DoctorCategoryExceptions
{
    
    
    public sealed class CategoryNotFoundByIdException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

        public string ErrorMessage { get; }

        public CategoryNotFoundByIdException(Guid id)
        {
            ErrorMessage = $"Hospital not found with id: {id}";
        }

        public CategoryNotFoundByIdException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        public CategoryNotFoundByIdException(string message, Exception? innerException) : base(message, innerException)
        {
            ErrorMessage = message;
        }
    }
}
