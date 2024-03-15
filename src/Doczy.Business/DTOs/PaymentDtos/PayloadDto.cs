using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.PaymentDtos
{
    public class PayloadDto
    {
        public int OrderId { get; set; }
        public string SessionId { get; set; }
        public string PaymentUrl { get; set; }
        public int TransactionId { get; set; }
    }
}
