using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class Univercity:BaseEntity
    {
        public string? Name { get; set; }
        public string? IconUrl { get; set; }
        public IList<Education>? Educations { get; set; }
    }
}
