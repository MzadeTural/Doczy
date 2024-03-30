using AutoMapper;
using Doczy.Business.DTOs.CategorySliderDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class CategorySliderMapper:Profile
    {
        public CategorySliderMapper()
        {
            CreateMap<CreateCategorySliderDto, CategorySlider>()
                .ForMember(s => s.IconUrl, e => e.Ignore());
                 
            CreateMap<CategorySlider, GetCategorySliderDto>();
        }
    }
}
