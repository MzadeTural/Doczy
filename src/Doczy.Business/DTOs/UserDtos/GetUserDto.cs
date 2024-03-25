using Doczy.Core.Entities;

namespace Doczy.Business.DTOs.UserDtos
{
    public record GetUserDto
    (
        string FirstName  ,
         string LastName ,
         string ProfileImageUrl,
         string? Gender
        );
}
