using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class FieldOfStudy : BaseEntity
    {
        public string? Name { get; set; }
        public IList<Education>? Educations { get; set; }
    }
}
