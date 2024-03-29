using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class CategorySlider:BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
    }
}
