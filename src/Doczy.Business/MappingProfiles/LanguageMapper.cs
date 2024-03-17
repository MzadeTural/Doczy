using AutoMapper;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.MappingProfiles
{
    public class LanguageMapper:Profile
    {
        public LanguageMapper()
        {
            CreateMap<CreateLanguageDto, Language>().ReverseMap();
            CreateMap<Language, GetLanguageDto>()
                
                .ReverseMap();

           
        }
    
    }
}
