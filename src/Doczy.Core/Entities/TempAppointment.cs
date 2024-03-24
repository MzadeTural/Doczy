using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class TempAppointment:BaseAuditableEntity
    {

        public string? PainDescription { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid ServiceId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentUrl { get; set; }
        public int OrderId { get; set; } // Payment gateway's order ID
        public string SessionId { get; set; } // Payment gateway's session ID
    }
}
