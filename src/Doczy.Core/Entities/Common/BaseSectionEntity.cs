namespace Doczy.Core.Entities.Common
{
    public abstract class BaseSectionEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UptadetAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
