using Doczy.Business.DTOs.HospitalDtos;
namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetDoctorDetailDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CategoryName { get; set; }
        public string? ProfileImageUrl { get; set; }
        public double? Rating { get; set; }
        public int? Favourite { get; set; }
        public GetHospitalDto Hospital { get; set; }
        public DateTime EarliestAvailable { get; set; }
        public int? Reviews { get; set; }
        public bool IsFavourite { get; set; }
    }
}
