using System;
namespace Doczy.Business.DTOs.BlogDtos
{
    public record UpdateBlogDto(
        string Title,
     string Subtitle,
     string Content,
     string ImageUrl,
     Guid DoctorId
        );
}

