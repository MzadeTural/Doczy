using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Experiance:BaseSectionEntity
    {
        public string Title { get; set; }
        public DoctorAppUser? Doctor{ get; set; }
        public Guid? DoctorId { get; set; }
        public WorkPlace? WorkPlace { get; set; }
        public Guid WorkPlaceId { get; set; }
        public string? Location { get; set; }
        public bool currentlyWorking { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate{ get; set; }
    }
}
