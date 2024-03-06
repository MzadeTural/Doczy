using AutoMapper;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.MappingProfiles
{
    public class DoctorMapper : Profile
    {
        public DoctorMapper()
        {

            CreateMap<CreateDoctorDto, DoctorAppUser>()
                    .ForMember(usc => usc.DiplomaImageUrl, e => e.Ignore())
                    .ForMember(usc => usc.IdCardImageUrl, e => e.Ignore())
                    .ReverseMap();
            CreateMap<DoctorAppUser, GetDoctorsDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.DoctorCategory.Name))
                   .ReverseMap();

        }
    }
}
