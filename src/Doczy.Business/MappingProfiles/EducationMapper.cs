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
		}
	}
}