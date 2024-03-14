using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Experiance:BaseAuditableEntity
    {
        public string Title { get; set; }
        public DoctorAppUser? Doctor{ get; set; }
        public Guid? DoctorId { get; set; }
        public Hospital? Hospital { get; set; }
        public Guid HospitalId { get; set; }
        public string? Location { get; set; }
        public bool currentlyWorking { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate{ get; set; }
    }
}
