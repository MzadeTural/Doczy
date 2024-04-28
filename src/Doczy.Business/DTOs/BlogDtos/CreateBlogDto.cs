using System;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.DTOs.BlogDtos
{
	 public record CreateBlogDto(
        string Title,
     string Subtitle,
     string Content,
     string ImageUrl,
     Guid DoctorId
		);
}

