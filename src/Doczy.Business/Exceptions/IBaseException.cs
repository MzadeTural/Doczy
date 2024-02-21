using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.Exceptions
{
    public interface IBaseException
    {
      
        HttpStatusCode StatusCode { get; }
        string ErrorMessage { get; }
        virtual string? ErrorDetail { get => "Not any error detail"; }

    }
}
