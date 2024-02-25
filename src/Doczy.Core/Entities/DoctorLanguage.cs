using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class DoctorLanguage:BaseEntity
    {
        public DoctorAppUser? Doctor{ get; set; }
        public Guid DoctorId { get; set; }
        public Language? Language { get; set; }
        public Guid LanguageId { get; set; }
    }
}
