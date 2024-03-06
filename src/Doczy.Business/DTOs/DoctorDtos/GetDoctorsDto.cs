namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetDoctorsDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CategoryName { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public int Raiting { get; set; }
        public byte Favourite{ get; set; }
    }
}
