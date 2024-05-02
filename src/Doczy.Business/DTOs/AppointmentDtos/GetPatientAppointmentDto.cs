namespace Doczy.Business.DTOs.AppointmentDtos
{
    public class GetPatientAppointmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeIconUrl { get; set; }
        public byte Duration { get; set; }
    }
}
