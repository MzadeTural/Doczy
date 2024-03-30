using AutoMapper;
using Doczy.Business.DTOs.SliderDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class SliderMapper : Profile
    {
        public SliderMapper()
        {
            CreateMap<CreateSliderDto, Slider>();
            CreateMap<Slider, GetSliderDto>();
        }
    }
}
