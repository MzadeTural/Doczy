namespace Doczy.Core.Entities.Identities
{
    public class DoctorAppUser : BaseAppUser
    {
        public List<Blog> Blogs { get; set; }
        public List<Appointment>? Appointments { get; set; }
       
        
    }
}
