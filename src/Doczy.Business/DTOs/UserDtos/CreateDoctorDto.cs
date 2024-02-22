using Doczy.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Doczy.Business.DTOs.UserDtos
{
    public class CreateDoctorDto
    {
        public DateTime CreatedAt { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public  string Email { get; set; } = null!;
        [Display(Name = "Fincode")]
        public  string UserName { get; set; } = null!;
        public string? IdCardImageUrl { get; set; }
        public string? DiplomaImageUrl { get; set; }
        public string? Password { get; set; }
    }
}
