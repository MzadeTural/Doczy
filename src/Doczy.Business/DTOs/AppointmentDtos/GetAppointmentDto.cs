namespace Doczy.Business.DTOs.AppointmentDtos
{
    public class GetAppointmentDto
    {
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string PatientName { get; set; }
        public string PatientLastName { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeIconUrl { get; set; }
    }
}
