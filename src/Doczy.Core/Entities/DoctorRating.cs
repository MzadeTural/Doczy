using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class DoctorRating:BaseEntity
    {
        
        public int Rating { get; set; } // Rating out of 5
        public string Review { get; set; }
        public DateTime Date { get; set; }
        public Guid DoctorId { get; set; }
        public DoctorAppUser Doctor { get; set; }
        public Guid PatientId { get; set; }
        public PatientAppUser Patient { get; set; }
    }
}
