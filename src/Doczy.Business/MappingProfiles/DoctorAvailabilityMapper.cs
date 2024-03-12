using AutoMapper;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Business.DTOs.Experiance;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class DoctorAvailabilityMapper:Profile
    {
        public DoctorAvailabilityMapper()
        {
            CreateMap<DoctorAvailability, CreateDoctorAvailabilityDto>()
                .ReverseMap();  
        }
    }
}
