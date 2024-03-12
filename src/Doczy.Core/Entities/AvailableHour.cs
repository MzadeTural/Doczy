using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class AvailableHour:BaseAuditableEntity
    {
        public DoctorAvailability DoctorAvailability { get; set; }
        public Guid DoctorAvailabilityId { get; set; }
        public TimeSpan Time { get; set; }
    }
}
