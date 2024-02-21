using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class WorkPlace:BaseEntity
    {
        public string? Name { get; set; }
        public string? IconUrl { get; set; }
        public IEnumerable<DoctorAppUser>? Doctors{ get; set; }
    }
}
