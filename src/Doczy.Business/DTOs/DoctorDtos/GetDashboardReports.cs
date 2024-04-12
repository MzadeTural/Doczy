namespace Doczy.Business.DTOs.DoctorDtos
{
    public class GetDashboardReports
    {
       
            public int Patients { get; set; }
            public int Questions { get; set; }
            public double? Raiting { get; set; }
            public int InPerson { get; set; }
            public int Online { get; set; }
            public int Favourite { get; set; }
            public int? Reviews { get; set; }

        
    }
}
