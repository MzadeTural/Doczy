using Doczy.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Doczy.Business.DTOs.UserDtos
{
    public class CreateDoctorDto
    {
       
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public  string Email { get; set; } = null!;
        [Display(Name = "Fincode")]
        public  string UserName { get; set; } = null!;
        public IFormFile IdCardImageUrl { get; set; }
        public IFormFile DiplomaImageUrl { get; set; }
        public string? Password { get; set; }
        public Guid DoctorCategoryId { get; set; }
    }
}
