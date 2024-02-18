using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.Common
{
    public record ResponseDto(HttpStatusCode StatusCode, string Message);
}
