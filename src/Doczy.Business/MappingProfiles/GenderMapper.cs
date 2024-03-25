using AutoMapper;
using Doczy.Business.DTOs.GenderDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.MappingProfiles
{
    public class GenderMapper:Profile
    {
        public GenderMapper()
        {
            CreateMap<CreateGenderDto, Gender>().ReverseMap();  
            CreateMap<Gender, GetGenderDto>().ReverseMap();  

        }
    }
}
