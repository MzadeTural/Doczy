using System;
using AutoMapper;
using Doczy.Business.DTOs.BlogDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.MappingProfiles
{
	public class BlogMapper:Profile
	{
		public BlogMapper()
		{
            CreateMap<CreateBlogDto, Blog>();
            //CreateMap<UpdateFieldOfStudyDto, FieldOfStudy>();
            CreateMap<Blog, GetBlogDto>();
        }
	}
}

