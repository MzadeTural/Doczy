namespace Doczy.Business.DTOs.AuthDtos
{
    public record LoginDto(string Email, string Password ,bool RememberMe);
}
