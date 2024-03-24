using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Award:BaseAuditableEntity
    {
        public string? AwardImageUrl { get; set; }
        public DoctorAppUser Doctor{ get; set; }
        public Guid DoctorId { get; set; }
    }
}
