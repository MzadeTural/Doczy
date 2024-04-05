using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorCategoryDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
    public class DoctorCategoryMapper:Profile
    {
        public DoctorCategoryMapper()
        {
            CreateMap<CreateDoctorCategoryDto,DoctorCategory>().ReverseMap();
            CreateMap<DoctorCategory,GetDoctorCategoryDto>().ReverseMap();
            CreateMap<DoctorCategory,GetEntityIdDto>().ReverseMap();
        }
    }
}
