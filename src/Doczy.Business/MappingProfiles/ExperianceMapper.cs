using AutoMapper;
using Doczy.Business.DTOs.Experiance;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class ExperianceMapper : Profile
    {
        public ExperianceMapper()
        {
            CreateMap<CreateExperianceDto, Experiance>().ReverseMap();
            CreateMap<Experiance, GetExperianceDto>()
           .ForMember(dest => dest.WorkPlace, opt => opt.MapFrom(src => src.WorkPlace.Name));
         
        }
    }
}
