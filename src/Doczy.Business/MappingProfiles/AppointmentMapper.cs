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
            CreateMap<Appointment, GetDoctorAppointmentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Service.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Patient.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Patient.FirstName))
            .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.Service.ServiceType.Name))
            .ForMember(dest => dest.ServiceTypeIconUrl, opt => opt.MapFrom(src => src.Service.ServiceType.IconUrl)).ReverseMap();

             CreateMap<Appointment, GetPatientAppointmentDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Service.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Doctor.LastName))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Doctor.FirstName))
            .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.Service.ServiceType.Name))
            .ForMember(dest => dest.ServiceTypeIconUrl, opt => opt.MapFrom(src => src.Service.ServiceType.IconUrl)).ReverseMap();

            CreateMap<Appointment, GetAppoinmnetDetailDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Service.Name))
           .ForMember(dest => dest.ServiceTypeName, opt => opt.MapFrom(src => src.Service.ServiceType.Name))
           .ForMember(dest => dest.ServiceTypeIconUrl, opt => opt.MapFrom(src => src.Service.ServiceType.IconUrl))
           .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Service.Duration))
           .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price)).ReverseMap();

        }
    }
}
