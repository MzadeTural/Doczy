using AutoMapper;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class DoctorAvailabilityMapper : Profile
    {
        public DoctorAvailabilityMapper()
        {
            CreateMap< CreateDoctorAvailabilityDto, DoctorAvailability>()
                  .ForMember(usc => usc.AvailableHours, e => e.Ignore())
                .ReverseMap();

            CreateMap<DoctorAvailability, GetDoctorAvailabilityDto>()
                    .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
            .ForMember(dest => dest.AvailableHours, opt => opt.MapFrom(src => src.AvailableHours.Select(ah => new AvailableHour { Time = ah.Time }).ToList()));

        }
    }
}
