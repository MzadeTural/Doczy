using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetUnVerifiedDoctorsDto
    {
        public string? IdCardImageUrl { get; set; }
        public string? DiplomaImageUrl { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
