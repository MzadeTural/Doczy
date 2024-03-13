using AutoMapper;
using Doczy.Business.DTOs.AvailableHoursDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class AvailableHourMapper:Profile
    {
        public AvailableHourMapper()
        {
            CreateMap<AvailableHour, GetAvailableHourDto>();
        }
    }
}
