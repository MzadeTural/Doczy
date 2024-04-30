namespace Doczy.Business.DTOs.AppointmentDtos
{
    public class GetAppoinmnetDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string ServiceTypeName { get; set; }
        public string ServiceTypeIconUrl { get; set; }
        public decimal Price { get; set; }
        public byte Duration { get; set; }
    }
}
