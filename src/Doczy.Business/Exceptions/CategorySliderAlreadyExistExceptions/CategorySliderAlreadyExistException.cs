using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.CategorySliderAlreadyExistExceptions
{

    public class CategorySliderAlreadyExistException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.SeeOther;

        public string ErrorMessage { get; }

        public CategorySliderAlreadyExistException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
