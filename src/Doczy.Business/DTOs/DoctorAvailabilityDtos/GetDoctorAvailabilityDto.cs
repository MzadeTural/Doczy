using Doczy.Core.Entities.Identities;
using Doczy.Core.Entities;
using Doczy.Business.DTOs.AvailableHoursDtos;

namespace Doczy.Business.DTOs.DoctorAvailabilityDtos
{
    public class GetDoctorAvailabilityDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<GetAvailableHourDto> AvailableHours { get; set; }
    }
}
