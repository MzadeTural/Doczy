using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class FavoriteDoctor:BaseEntity
    {
        public Guid PatientId { get; set; }
        public PatientAppUser Patient { get; set; }
        public Guid DoctorId { get; set; }
        public DoctorAppUser Doctor { get; set; }
    }
}
