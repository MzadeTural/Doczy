using System;
namespace Doczy.Business.DTOs.BlogDtos
{
    public record GetBlogDto(
        Guid Id,
        string Title,
     string Subtitle,
     string Content,
     string ImageUrl,
     Guid DoctorId
        );
}

