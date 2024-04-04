using Doczy.Core.Entities.Common;

namespace Doczy.Core.Entities
{
    public class SocialMedia:BaseAuditableEntity
    {
        public string Platform { get; set; }
        public string PlatformIconUrl { get; set; }
    }
}
