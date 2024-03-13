using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doczy.Business.DTOs.AvailableHoursDtos
{
    public class GetAvailableHourDto
    {
        public Guid Id { get; set; }
        public TimeSpan Time { get; set; }
    }
}
