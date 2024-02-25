using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class UnivercityDegree:BaseEntity
    {
        public string? Name { get; set; }
        public IList<Education>? Educations { get; set; }

    }
}
