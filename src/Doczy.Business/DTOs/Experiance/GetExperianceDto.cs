namespace Doczy.Business.DTOs.Experiance
{
   

    public class GetExperianceDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Hospital { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}
