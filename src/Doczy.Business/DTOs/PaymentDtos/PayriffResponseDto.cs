using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.PaymentDtos
{
    public class PayriffResponseDto
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Route { get; set; }
        public string RnternalMessage { get; set; }
        public string ResponseId { get; set; }
        public PayloadDto Payload { get; set; }
    }
}
