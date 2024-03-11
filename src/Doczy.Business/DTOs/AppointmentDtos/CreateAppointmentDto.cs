namespace Doczy.Business.DTOs.AppointmentDto
{
    public class CreateAppointmentDto
    {
        public Guid DoctorId { get; set; }
        public DateTime ChosenDate { get; set; }
        public string ChosenHour { get; set; }
        public Guid ServiceId { get; set; }
        public string? PainDescription { get; set; }
    }
}
