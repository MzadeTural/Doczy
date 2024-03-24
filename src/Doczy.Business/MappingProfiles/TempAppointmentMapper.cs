using AutoMapper;
using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class TempAppointmentMapper:Profile
    {
        public TempAppointmentMapper()
        {
            CreateMap<CreateAppointmentDto, TempAppointment>()
            .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.ChosenDate))
                  .ReverseMap();
        }
    }
}
