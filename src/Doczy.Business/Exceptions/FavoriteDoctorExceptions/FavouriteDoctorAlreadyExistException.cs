using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions.FavoriteDoctorExceptions
{
  
    public class FavouriteDoctorAlreadyExistException : Exception, IBaseException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string ErrorMessage { get; }

        public FavouriteDoctorAlreadyExistException(string Message) : base(Message) { ErrorMessage = Message; }
    }
}
