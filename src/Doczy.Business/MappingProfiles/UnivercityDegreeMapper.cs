using System;
using AutoMapper;
using Doczy.Business.DTOs.UnivercityDegreeDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
	public class UnivercityDegreeMapper:Profile
	{
		public UnivercityDegreeMapper()
		{
            CreateMap<CreateUnivercityDegreeDto, UnivercityDegree>();
            //CreateMap<UpdateUnivercityDegreeDto, UnivercityDegree>();
            CreateMap<UnivercityDegree, GetUnivercityDegreeDto>();
        }
	}
}

