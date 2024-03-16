using AutoMapper;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Experiance;
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
                .ForMember(dest => dest.Favourite, opt => opt.MapFrom(src => src.FavoriteDoctors.Count()))
                .ForMember(dest => dest.Raiting, opt => opt.MapFrom(src => src.Ratings.Average(r => r.Rating)))

                   .ReverseMap();
            CreateMap<DoctorAppUser, GetDoctorResumeDto>().ReverseMap();

        }
    }
}
