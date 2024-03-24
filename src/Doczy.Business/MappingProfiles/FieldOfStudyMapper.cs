using System;
using AutoMapper;
using Doczy.Business.DTOs.FieldOfStudyDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
	public class FieldOfStudyMapper : Profile
	{
		public FieldOfStudyMapper()
		{
			CreateMap<CreateFieldOfStudyDto, FieldOfStudy>();
			//CreateMap<UpdateFieldOfStudyDto, FieldOfStudy>();
			CreateMap<FieldOfStudy, GetFieldOfStudyDto>();
        }
	}
}

