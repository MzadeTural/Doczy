using System.ComponentModel.DataAnnotations;

namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetWillVerifiedDoctorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CategoryName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? IdCardImageUrl { get; set; }
        public string? DiplomaImageUrl { get; set; }
        [Display(Name = "Fincode")]
        public  string? UserName { get; set; }
    }
}
