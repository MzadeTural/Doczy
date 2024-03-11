using AutoMapper;
using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class AppointmentMapper:Profile
    {
        public AppointmentMapper()
        {
            CreateMap<CreateAppointmentDto, Appointment>()
          .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.ChosenDate))
          .ForMember(dest => dest.AppointmentTime, opt => opt.MapFrom(src => src.ChosenHour))
                .ReverseMap();
        }
    }
}
