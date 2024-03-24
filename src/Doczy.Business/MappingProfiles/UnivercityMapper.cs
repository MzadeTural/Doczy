using System;
using AutoMapper;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.DTOs.UnivercityDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
	public class UnivercityMapper:Profile
	{
		public UnivercityMapper()
		{
            CreateMap<CreateUnivercityDto, Univercity>().ReverseMap();
            CreateMap<UpdateUnivercityDto, Univercity>().ReverseMap();
            CreateMap<Univercity, GetUnivercityDto>().ReverseMap();
        }
	}
}

