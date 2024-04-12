namespace Doczy.Business.DTOs.UserDtos
{
    public record ChangePasswordDto(string CurrentPassword, string NewPassword);
}
