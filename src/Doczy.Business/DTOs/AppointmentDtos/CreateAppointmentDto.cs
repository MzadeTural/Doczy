namespace Doczy.Business.DTOs.AppointmentDto
{
    public class CreateAppointmentDto
    {
        public Guid DoctorId { get; set; }
        public DateTime ChosenDate { get; set; }
        public Guid ChosenHourId { get; set; }
        public Guid ServiceId { get; set; }
        public string? PainDescription { get; set; }
    }
}
