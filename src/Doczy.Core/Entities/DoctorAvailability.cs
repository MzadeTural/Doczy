using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;
using System.ComponentModel.DataAnnotations;

namespace Doczy.Core.Entities
{
    public class DoctorAvailability:BaseAuditableEntity
    {
        public DoctorAppUser Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [DataType("TimeOnly")]
        public DateTime MyProperty { get; set; }
        public ICollection<TimeSpan> AvailableHours { get; set; }
       


    }
}
