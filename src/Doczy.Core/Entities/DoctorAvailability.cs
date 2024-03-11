using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class DoctorAvailability:BaseAuditableEntity
    {
        public DoctorAppUser Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public ICollection<TimeSpan> AvailableHours { get; set; }
     

    }
}
