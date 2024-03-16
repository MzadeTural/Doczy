using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Speciality : BaseEntity
    {

        public DoctorAppUser Doctor { get; set; }
        public Guid DoctorId { get; set; }
        public string Title { get; set; }
    }
}
