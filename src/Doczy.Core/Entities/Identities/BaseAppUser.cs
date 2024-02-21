using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Doczy.Core.Entities.Identities
{
    public class BaseAppUser: IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public override string Email { get; set; } = null!;
        [Display(Name = "Fincode")]
        public override string UserName { get; set; } = null!;
        public override string PhoneNumber { get; set; } = null!;
        public Guid GenderId { get; set; }
        public Gender? Gender { get; set; }
        public string? Fullname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
