using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class Service:BaseSectionEntity
    {
        public string? Name { get; set; }
        public byte Duration { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Guid ServiceTypeId { get; set; }
        public ServiceType? ServiceType { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
