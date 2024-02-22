namespace Doczy.Core.Entities.Identities
{
    public class DoctorAppUser : BaseAppUser
    {
        public List<Blog>? Blogs { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public string? IdCardImageUrl { get; set; }
        public string? DiplomaImageUrl { get; set; }
        public WorkPlace? WorkPlace { get; set; }
        public Guid WorkPlaceId { get; set; }
        public DoctorCategory? DoctorCategory { get; set; }
        public Guid DoctorCategoryId { get; set; }


    }
}
