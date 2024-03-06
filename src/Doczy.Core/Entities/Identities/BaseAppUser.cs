using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Doczy.Core.Entities.Identities
{
    public class BaseAppUser: IdentityUser<Guid>
    {
        public DateTime CreatedAt { get; set; }
        public string ProfileImageUrl { get; set; }
        public override string Email { get; set; } 
        [Display(Name = "Fincode")]
        public override string? UserName { get; set; } 
        public override string? PhoneNumber { get; set; } 
        public Guid? GenderId { get; set; }
        public Gender? Gender { get; set; }
        public string? FirstName  { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPExpiryDate { get; set; }
        public bool IsVerified { get; set; }
    }
}
