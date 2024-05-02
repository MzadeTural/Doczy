using System;
using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
	public class PayriffPayments : BaseAuditableEntity
    {
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
        public string RRN { get; set; }
        public int OrderId { get; set; }
        public string SessionId { get; set; }
        public string TranDateTime { get; set; }
        public string OrderStatus { get; set; }
        public string ApprovalCodeScr { get; set; }
        public decimal PurchaseAmount { get; set; }
    }
}

