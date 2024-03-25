using AutoMapper;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities;
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
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Ratings.Count()))
                   .ReverseMap();

            CreateMap<DoctorAppUser, GetDoctorResumeDto>().ReverseMap();
            CreateMap<DoctorAppUser, GetAboutDoctorDto>()
                    .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages.Select(language => new GetLanguageDto(language.Id, language.Language.Name))))
                    .ReverseMap();

            CreateMap<DoctorAppUser, GetDoctorDetailDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.DoctorCategory.Name))
               .ForMember(dest => dest.Hospital, opt => opt.MapFrom(src => GetHospitalDtoFromExperiences(src.Experiances)))
                .ForMember(dest => dest.Favourite, opt => opt.MapFrom(src => src.FavoriteDoctors.Count()))
                .ForMember(dest => dest.Raiting, opt => opt.MapFrom(src => src.Ratings.Average(r => r.Rating)))
                .ReverseMap();
            CreateMap<DoctorAppUser, GetWillVerifiedDoctorDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.DoctorCategory.Name))
               .ReverseMap();


        }
        private GetHospitalDto GetHospitalDtoFromExperiences(IEnumerable<Experiance> experiences)
        {
            var workingExperience = experiences.FirstOrDefault(exp => exp.currentlyWorking);
            if (workingExperience != null)
            {
                return new GetHospitalDto
                (   id:workingExperience.Hospital.Id,
                    Name: workingExperience.Hospital?.Name,
                    IconUrl: workingExperience.Hospital?.IconUrl
                );
            }
            return null;
        }
    }
}
