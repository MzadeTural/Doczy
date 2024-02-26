using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class Experiance:BaseSectionEntity
    {
        public string Title { get; set; }
        public WorkPlace? WorkPlace { get; set; }
        public Guid WorkPlaceId { get; set; }
        public string Location { get; set; }
        public bool currentlyWorking { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate{ get; set; }
    }
}
