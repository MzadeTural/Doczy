using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Appointment : BaseSectionEntity
    {
        public string? PainDescription { get; set; }
        public TimeOnly Time { get; set; }
        public DateOnly Date { get; set; }
        public DoctorAppUser? Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public PatientAppUser? Patient { get; set; }
        public Guid PatientId { get; set; }
        public Service? Service { get; set; }
        public Guid ServiceId { get; set; }
    }
}
