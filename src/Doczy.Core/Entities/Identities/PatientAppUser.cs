namespace Doczy.Core.Entities.Identities
{
    public class PatientAppUser : BaseAppUser
    {
        public List<Appointment>? Appointments { get; set; }
         public IEnumerable<DoctorRating> Ratings { get; set; }
        public IEnumerable<FavoriteDoctor> FavoriteDoctors { get; set; }
    }
}
