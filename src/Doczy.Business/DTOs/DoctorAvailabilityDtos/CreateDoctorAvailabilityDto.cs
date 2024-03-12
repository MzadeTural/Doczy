using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.DoctorAvailabilityDtos
{
    public class CreateDoctorAvailabilityDto
    {
        public DayOfWeek DayOfWeek { get; set; }
        public List<TimeSpan> AvailableHours { get; set; }
    }
}
