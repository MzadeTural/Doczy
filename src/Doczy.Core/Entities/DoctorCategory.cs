using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class DoctorCategory : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<DoctorAppUser>? Doctors{ get; set; }
    }
}
