namespace Doczy.Core.Entities.Identities
{
    public class DoctorAppUser : BaseAppUser
    {
        public List<Blog>? Blogs { get; set; }
        public IList<DoctorLanguage>? Languages { get; set; }
        public IList<Education> Educations { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<Service>? Services { get; set; }
        public string? IdCardImageUrl { get; set; }
        public string? DiplomaImageUrl { get; set; }
        public IEnumerable<Experiance>? Experiances { get; set; }
        public IEnumerable<Award>? Awards { get; set; }
        public IEnumerable<Speciality>? Specialities { get; set; }
        public string? AboutDoctor { get; set; }
        public DoctorCategory? DoctorCategory { get; set; }
        public Guid? DoctorCategoryId { get; set; }
        public IEnumerable<DoctorRating> Ratings { get; set; }
        public IEnumerable<FavoriteDoctor> FavoriteDoctors { get; set; }



    }
}
