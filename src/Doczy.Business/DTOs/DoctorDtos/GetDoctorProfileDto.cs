namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetDoctorProfileDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CategoryName { get; set; }
        public string? Gender { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
