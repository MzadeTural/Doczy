using Doczy.Core.Entities.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Core.Entities
{
    public class UserSocialMedia:BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public DoctorAppUser User { get; set; }

        public Guid SocialMediaId { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public string PlatformUrl { get; set; }
    }
}
