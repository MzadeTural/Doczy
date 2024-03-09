using AutoMapper;
using Doczy.Business.DTOs.EducationDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
	public class EducationMapper:Profile
	{
		public EducationMapper()
		{
			CreateMap<CreateEducationDto, Education>().ReverseMap();
			CreateMap<UpdateEducationDto, Education>()
				.ForMember(x=>x.Id,e=>e.Ignore())
				.ReverseMap();
            CreateMap<Education, EducationDto>()
				.ForMember(x => x.Univercity, c => c.MapFrom(k => k.Univercity.Name))
				.ForMember(x => x.FieldOfStudy, c => c.MapFrom(k => k.FieldOfStudy.Name))
				.ForMember(x => x.UnivercityDegree, c => c.MapFrom(k => k.UnivercityDegree.Name))
				.ReverseMap();
        }
	}
}