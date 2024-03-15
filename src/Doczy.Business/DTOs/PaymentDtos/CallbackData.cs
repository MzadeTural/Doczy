using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.PaymentDtos
{
    public class CallbackData
    {
        public Payload Payload { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Route { get; set; }
    }

    
}
