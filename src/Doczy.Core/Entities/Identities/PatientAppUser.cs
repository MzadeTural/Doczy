namespace Doczy.Core.Entities.Identities
{
    public class PatientAppUser : BaseAppUser
    {
        public List<Appointment>? Appointments { get; set; }
    }
}
