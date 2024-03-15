using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.PaymentDtos
{
    public class Payload
    {
        public string Version { get; set; }
        public int OrderId { get; set; }
        public string SessionId { get; set; }
        public string TransactionType { get; set; }
        public string RRN { get; set; }
        public string PAN { get; set; }
        public decimal PurchaseAmount { get; set; }
        public string Currency { get; set; }
        public DateTime TranDateTime { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string Brand { get; set; }
        public string OrderStatus { get; set; }
        public string ApprovalCode { get; set; }
        public string OrderDescription { get; set; }
        public string ApprovalCodeScr { get; set; }
        public decimal PurchaseAmountScr { get; set; }
        public string CurrencyScr { get; set; }
        public CardRegistration CardRegistration { get; set; }
        public string InvoiceUuid { get; set; }
    }
}
