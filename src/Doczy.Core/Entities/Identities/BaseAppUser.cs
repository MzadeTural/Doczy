using Microsoft.AspNetCore.Identity;


namespace Doczy.Core.Entities.Identities
{
    public class BaseAppUser: IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public Guid GenderId { get; set; }
        public Gender? Gender { get; set; }
        public string? Fullname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
