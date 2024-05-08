using Doczy.Core.Entities;

namespace Doczy.Business.DTOs.UserDtos
{
    public class GetUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Gender { get; set; }
    }
}
