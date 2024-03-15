using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.PaymentDtos
{
    public class CardRegistration
    {
        public string MaskedPAN { get; set; }
        public string CardUID { get; set; }
        public string Brand { get; set; }
    }
}
