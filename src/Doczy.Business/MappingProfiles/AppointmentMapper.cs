using AutoMapper;
using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.AppointmentDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class AppointmentMapper:Profile
    {
        public AppointmentMapper()
        {
            CreateMap<CreateAppointmentDto, Appointment>()
          .ForMember(dest => dest.AppointmentDate, opt => opt.MapFrom(src => src.ChosenDate))
                .ReverseMap();
            CreateMap<TempAppointment, Appointment>().ReverseMap();
            CreateMap<Appointment, GetAppointmentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Service.Name))
            .ForMember(dest => dest.PatientLastName, opt => opt.MapFrom(src => src.Patient.LastName))
            .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FirstName))
            .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.Service.ServiceType.Name))
            .ForMember(dest => dest.ServiceTypeIconUrl, opt => opt.MapFrom(src => src.Service.ServiceType.IconUrl)).ReverseMap();

        }
    }
}
