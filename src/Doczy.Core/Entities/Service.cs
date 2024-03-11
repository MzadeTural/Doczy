using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Service:BaseAuditableEntity
    {
        public string? Name { get; set; }
        public byte Duration { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public Guid ServiceTypeId { get; set; }
        public DoctorAppUser? Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public ServiceType? ServiceType { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
