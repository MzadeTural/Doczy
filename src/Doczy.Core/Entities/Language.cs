using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class Language:BaseEntity
    {
        public string? Name { get; set; }
        public IList<DoctorLanguage>? Doctors { get; set; }
    }
}
