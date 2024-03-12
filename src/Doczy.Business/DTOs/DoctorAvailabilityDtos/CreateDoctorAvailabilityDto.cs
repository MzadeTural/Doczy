using Doczy.Business.DTOs.AvailableHoursDtos;
using Doczy.Core.Entities;
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
        public List<CreateAvailableHoursDto> AvailableHours { get; set; }
    }
}
