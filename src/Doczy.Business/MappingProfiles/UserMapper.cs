using AutoMapper;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.MappingProfiles
{
    public class UserMapper:Profile
    {
        public UserMapper()
        {
            CreateMap<BaseAppUser, GetUserDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.Name))
                   .ReverseMap();
        }
    }
}
